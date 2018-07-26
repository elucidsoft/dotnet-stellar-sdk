/*
 *  Copyright 2014 Jonathan Bradshaw. All rights reserved.
 *  Redistribution and use in source and binary forms, with or without modification, is permitted.
 */

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk
{
    /// <inheritdoc />
    /// <summary>
    ///     An EventSource represents a long-lived HTTP connection through which a Web server can “push” textual messages.
    ///     To use these “Server Sent Events”, pass the server URL to the EventSource() constructor and then register
    ///     a message event handler on the resulting Event Source object. The EventSource attempts to be resilient to
    ///     transitory network errors and interruptions by automatically retrying connections to maintain persistence.
    /// </summary>
    public sealed class EventSource : IDisposable
    {
        #region Public Enums

        /// <summary>
        ///     The possible values of the readyState property.
        /// </summary>
        public enum EventSourceState
        {
            Connecting = 0,
            Open = 1,
            Closed = 2,
            Shutdown = 3
        }

        #endregion Public Enums

        #region Protected Fields

        private static readonly TraceSource Trace = new TraceSource("EventSource");
        private const int DefaultRetryInterval = 3000;

        #endregion Protected Fields

        #region Private Fields

        private readonly byte[] _buffer = new byte[8192];
        private StringBuilder _eventStream;
        private string _eventType;
        private Stream _httpStream;
        private HttpWebRequest _httpWebRequest;
        private HttpWebResponse _httpWebResponse;
        private EventSourceState _readyState;
        private int _retryInterval = DefaultRetryInterval;
        private Timer _retryTimer;
        private bool _shutdownToken;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventSource" /> class.
        /// </summary>
        /// <param name="requestUriString">The URL.</param>
        public EventSource(string requestUriString)
            : this(new Uri(requestUriString))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventSource" /> class.
        /// </summary>
        /// <param name="requestUriString">The URL.</param>
        public EventSource(Uri requestUriString)
        {
            Url = requestUriString;
            Headers = new NameValueCollection();
            MessageTypes = new string[] { };
            Timeout = 100000; // 100 seconds
            _readyState = EventSourceState.Closed;
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        ///     Occurs when an error occurs.
        /// </summary>
        public event EventHandler<ServerSentErrorEventArgs> Error;

        /// <summary>
        ///     Occurs when a message is available.
        /// </summary>
        public event EventHandler<ServerSentEventArgs> Message;

        /// <summary>
        ///     Occurs when the ready state changes.
        /// </summary>
        public event EventHandler<StateChangeEventArgs> StateChange;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        ///     Gets or sets the headers to be sent in the request. For more
        ///     customization override the ConfigureWebRequest method.
        /// </summary>
        public NameValueCollection Headers { get; }

        /// <summary>
        ///     Gets or sets an optional message type filter. If set,
        ///     this filter specifies which event types to pass through.
        /// </summary>
        public string[] MessageTypes { get; }

        /// <summary>
        ///     Gets the last event identifier.
        /// </summary>
        public string LastEventId { get; private set; }

        /// <summary>
        ///     Gets the state of the connection.
        /// </summary>
        public EventSourceState ReadyState
        {
            get => _readyState;
            private set
            {
                _readyState = value;
                OnStateChangeEvent(new StateChangeEventArgs { NewState = value });
            }
        }

        /// <summary>
        ///     Gets or sets the initial connection timeout.
        ///     There is no timeout on the connection once established.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        ///     The absolute URL to which the EventSource is connected.
        /// </summary>
        public Uri Url { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Begin the process to connect to the the EventSource. The EventSource attempts to be resilient to
        ///     transitory network errors and interruptions by automatically retrying connections to maintain persistence.
        /// </summary>
        public async Task Connect()
        {
            if (ReadyState == EventSourceState.Connecting || ReadyState == EventSourceState.Open)
                throw new InvalidOperationException("Cannot call connect while connection is " + ReadyState);

            _shutdownToken = false;

            await ConnectAsync();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Shutdown();
            _httpWebRequest = null;
            _httpWebResponse = null;
            _retryTimer = null;
        }

        /// <summary>
        ///     This method shutsdown the connection.
        /// </summary>
        public void Shutdown()
        {
            if (_shutdownToken) return;

            _shutdownToken = true;
            CloseConnection();
            ReadyState = EventSourceState.Shutdown;
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        ///     Configures the web request object. Override this method to add custom
        ///     headers and settings to the request object as required.
        /// </summary>
        /// <param name="request">The HttpWebRequest request.</param>
        private void ConfigureWebRequest(HttpWebRequest request)
        {
            request.Accept = "text/event-stream";
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            if (Headers != null) request.Headers.Add(Headers);
            if (!string.IsNullOrEmpty(LastEventId)) request.Headers.Add("Last-Event-Id", LastEventId);
        }

        /// <summary>
        ///     Called when a complete message is received (indicates by two newlines)
        ///     to process the line(s) and dispatch the event. Override this method to
        ///     customize the parsing.
        /// </summary>
        /// <param name="content">The lines received.</param>
        private void DispatchEvent(string[] content)
        {
            if (_shutdownToken) return;
            StringBuilder sb = null;

            foreach (var line in content)
            {
                var pos = line.IndexOf(':');
                if (pos <= 0 || pos + 2 >= line.Length) continue;

                var type = line.Substring(0, pos);
                var value = line.Substring(pos + 2);

                Trace.TraceInformation("DispatchEvent (Type={0})", type);
                Trace.TraceData(TraceEventType.Verbose, 0, value);

                switch (type)
                {
                    case "id":
                        LastEventId = value;
                        break;

                    case "event":
                        _eventType = value;
                        break;

                    case "data":
                        if (IsWanted(_eventType))
                        {
                            if (sb == null) sb = new StringBuilder();
                            sb.AppendLine(value);
                        }
                        break;

                    case "retry":
                        int.TryParse(value, out _retryInterval);
                        break;
                }
            }

            if (sb == null || _shutdownToken) return;

            OnMessageEvent(new ServerSentEventArgs
            {
                Data = sb.ToString()
            });
        }

        /// <summary>
        ///     Determines whether the specified event type is filtered. By default this checks against
        ///     the list of MessageTypes (if specified) but can be overriden for additional tests.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <returns>[True] if the message should be processed.</returns>
        private bool IsWanted(string eventType)
        {
            return MessageTypes == null || MessageTypes.Count() == 0|| MessageTypes.Contains(eventType);
        }

        /// <summary>
        ///     Raises the <see cref="E:ErrorEvent" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ServerSentErrorEventArgs" /> instance containing the event data.</param>
        private void OnErrorEvent(ServerSentErrorEventArgs e)
        {
            Trace.TraceInformation("Raising OnErrorEvent ({0})", e.Exception.Message);
            var handler = Error;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="E:MessageEvent" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ServerSentEventArgs" /> instance containing the event data.</param>
        private void OnMessageEvent(ServerSentEventArgs e)
        {
            Trace.TraceInformation("Raising OnMessageEvent ({0})", _eventType);
            var handler = Message;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="E:StateChange" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnStateChangeEvent(StateChangeEventArgs e)
        {
            Trace.TraceInformation("Raising OnStateChangeEvent ({0})", e.NewState);
            var handler = StateChange;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Retries the connection after delay using a simple backoff mechanism.
        /// </summary>
        private void RetryAfterDelay(bool backoff = true)
        {
            if (_retryInterval <= 0 || _shutdownToken) return;

            // Attempt reconnection after retry interval
            Trace.TraceInformation("RetryAfterDelay ({0}ms)", _retryInterval);
            _retryTimer = new Timer(
                async delegate
                {
                    if (!_shutdownToken)
                        await ConnectAsync();
                },
                null,
                Math.Max(DefaultRetryInterval, _retryInterval),
                System.Threading.Timeout.Infinite); // Single shot timer

            // Increase backoff timer up to a minute each retry
            if (backoff) _retryInterval = (int)Math.Min(_retryInterval * 1.5, 60000);
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        ///     Closes the connection.
        /// </summary>
        private void CloseConnection()
        {
            if (ReadyState != EventSourceState.Connecting && ReadyState != EventSourceState.Open) return;
            Trace.TraceInformation("CloseConnection");

            if (_httpWebRequest != null)
                _httpWebRequest.Abort();

            if (_httpWebResponse != null)
                _httpWebResponse.Close();

            if (_retryTimer != null)
                _retryTimer.Dispose();

            _eventStream = null;
            LastEventId = null;
            _eventType = null;

            ReadyState = EventSourceState.Closed;
        }

        /// <summary>
        ///     Connects to the event source.
        /// </summary>
        private async Task<bool> ConnectAsync()
        {
            Trace.TraceInformation("ConnectAsync ({0})", Url);
            ReadyState = EventSourceState.Connecting;

            _httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            ConfigureWebRequest(_httpWebRequest);
            
            try
            {
                var handle = _httpWebRequest.BeginGetResponse(EndGetResponse, null);
                var tcs = new TaskCompletionSource<bool>();

                ThreadPool.RegisterWaitForSingleObject(
                    handle.AsyncWaitHandle,
                    (state, timedOut) =>
                    {
                        if (!timedOut || _httpWebRequest == null || _shutdownToken) return;
                        Trace.TraceInformation("ConnectAsync (Timed Out)");
                        OnErrorEvent(new ServerSentErrorEventArgs { Exception = new TimeoutException() });
                        CloseConnection();
                        RetryAfterDelay();
                    },
                    _httpWebRequest,
                    Timeout,
                    true);

                return await tcs.Task;
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is IOException)
                {
                    OnErrorEvent(new ServerSentErrorEventArgs { Exception = ex });
                    CloseConnection();
                    RetryAfterDelay();
                    return true;
                }

                throw;
            }
        }

        /// <summary>
        ///     Ends the async get response.
        /// </summary>
        /// <param name="result">The IAsyncResult.</param>
        /// <exception cref="System.NullReferenceException">GetResponseStream() returned null</exception>
        private void EndGetResponse(IAsyncResult result)
        {
            if (_shutdownToken) return;

            try
            {
                _httpWebResponse = (HttpWebResponse)_httpWebRequest.EndGetResponse(result);
                _httpStream = _httpWebResponse.GetResponseStream();
                if (_shutdownToken) return;
            }
            catch (WebException ex)
            {
                OnErrorEvent(new ServerSentErrorEventArgs { Exception = ex });
                CloseConnection();
                RetryAfterDelay();

                if (_httpWebResponse == null)
                    return;
            }

            var contentType = new ContentType(_httpWebResponse.ContentType);
            Trace.TraceInformation("EndGetResponse (StatusCode={0}, MediaType={1})", _httpWebResponse.StatusCode, contentType.MediaType);

            if (_httpWebResponse.StatusCode != HttpStatusCode.OK || contentType.MediaType != "text/event-stream")
            {
                // If we get the wrong content type or status code, as per spec, do not attempt to reconnect.
                OnErrorEvent(new ServerSentErrorEventArgs
                {
                    Exception = new Exception("Unexpected response from server. Status " +
                                              _httpWebResponse.StatusCode + ". Media Type " + contentType.MediaType)
                });
                CloseConnection();
                return;
            }

            ReadyState = EventSourceState.Open;
            _retryInterval = DefaultRetryInterval;

            _eventStream = new StringBuilder();
            LastEventId = null;
            _eventType = null;

            if (_shutdownToken) return;
            if (_httpStream == null) throw new NullReferenceException("GetResponseStream");
            _httpStream.BeginRead(_buffer, 0, _buffer.Length, EndReadFromStream, null);
        }

        /// <summary>
        ///     Recursive method to read the network stream and process the data.
        /// </summary>
        /// <param name="result">The IAsyncResult.</param>
        private void EndReadFromStream(IAsyncResult result)
        {
            if (_shutdownToken) return;

            try
            {
                var bytesRead = _httpStream.EndRead(result);
                Trace.TraceInformation("EndReadFromStream (Bytes={0})", bytesRead);
                if (_shutdownToken) return;

                if (bytesRead == 0)
                {
                    CloseConnection();
                    RetryAfterDelay();
                    return;
                }

                for (var i = 0; i < bytesRead; i++)
                    if (i > 0 && _buffer[i] == '\n' && _buffer[i - 1] == '\n')
                    {
                        DispatchEvent(_eventStream.ToString().Split('\n'));
                        _eventStream.Length = 0;
                    }
                    else
                    {
                        _eventStream.Append((char)_buffer[i]);
                    }
            }
            catch 
            {
                //OnErrorEvent(new ServerSentErrorEventArgs { Exception = ex });
                CloseConnection();
                RetryAfterDelay();
                return;
            }

            // Recursively call until we run out of data
            if (!_shutdownToken && ReadyState == EventSourceState.Open)
                _httpStream.BeginRead(_buffer, 0, _buffer.Length, EndReadFromStream, null);
        }

        #endregion Private Methods

        #region Public Classes

        /// <summary>
        ///     Server Sent Error Event Object
        /// </summary>
        public sealed class ServerSentErrorEventArgs : EventArgs
        {
            #region Public Properties

            /// <summary>
            /// Internal Exception
            /// </summary>
            public Exception Exception { get; internal set; }

            #endregion Public Properties
        }

        /// <summary>
        ///     Server Sent Event Message Object
        /// </summary>
        public sealed class ServerSentEventArgs : EventArgs
        {
            #region Public Properties

            /// <summary>
            ///     Gets the data.
            /// </summary>
            public string Data { get; internal set; }

            #endregion Public Properties
        }

        /// <summary>
        ///     Server Sent Error Event Object
        /// </summary>
        public sealed class StateChangeEventArgs : EventArgs
        {
            #region Public Properties

            /// <summary>
            /// New State changed to
            /// </summary>
            public EventSourceState NewState { get; internal set; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}