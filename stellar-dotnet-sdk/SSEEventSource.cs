using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using LaunchDarkly.EventSource;

namespace stellar_dotnet_sdk
{
    public class SSEEventSource : IEventSource, IDisposable
    {
        private readonly LaunchDarkly.EventSource.EventSource _eventSource;

        public SSEEventSource(Uri uri, Action<ConfigurationBuilder> configureEventSource = null)
        {
            Url = uri;
            var config = new ConfigurationBuilder(uri);
            configureEventSource?.Invoke(config);
            _eventSource = new LaunchDarkly.EventSource.EventSource(config.Build());

            _eventSource.Opened += StateChangedEventHandler;
            _eventSource.Closed += StateChangedEventHandler;
            _eventSource.Error += (sender, args) =>
                Error?.Invoke(this, ConvertExceptionEventArgs(args));
            _eventSource.MessageReceived += MessageReceivedEventHandler;
        }

        [Obsolete]
        public NameValueCollection Headers { get; }

        public string LastEventId { get; private set; }

        [Obsolete]
        public string[] MessageTypes { get; }

        public EventSource.EventSourceState ReadyState { get; private set; }

        public int Timeout { get; set; }

        public Uri Url { get; set; }

        public event EventHandler<EventSource.ServerSentErrorEventArgs> Error;

        public event EventHandler<EventSource.ServerSentEventArgs> Message;

        public event EventHandler<EventSource.StateChangeEventArgs> StateChange;

        public async Task Connect()
        {
            await _eventSource.StartAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            _eventSource?.Dispose();
        }

        public void Shutdown()
        {
            _eventSource.Close();
        }

        private void StateChangedEventHandler(object sender, StateChangedEventArgs args)
        {
            var newState = ConvertStateChangeEventArgs(args);
            ReadyState = newState.NewState;
            StateChange?.Invoke(this, newState);
        }

        private void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs args)
        {
            if (args.EventName != "message")
            {
                return;
            }

            LastEventId = args.Message.LastEventId;

            Message?.Invoke(this, new EventSource.ServerSentEventArgs { Data = args.Message.Data} );
        }

        private static EventSource.StateChangeEventArgs ConvertStateChangeEventArgs(StateChangedEventArgs args)
        {
            var newState = ConvertEventSourceState(args.ReadyState);
            return new EventSource.StateChangeEventArgs {NewState = newState};
        }

        private static EventSource.EventSourceState ConvertEventSourceState(ReadyState state)
        {
            switch (state)
            {
                case LaunchDarkly.EventSource.ReadyState.Closed:
                    return EventSource.EventSourceState.Closed;
                case LaunchDarkly.EventSource.ReadyState.Connecting:
                    return EventSource.EventSourceState.Connecting;
                case LaunchDarkly.EventSource.ReadyState.Open:
                    return EventSource.EventSourceState.Open;
                case LaunchDarkly.EventSource.ReadyState.Shutdown:
                    return EventSource.EventSourceState.Shutdown;
                case LaunchDarkly.EventSource.ReadyState.Raw:
                default:
                    return EventSource.EventSourceState.Raw;
            }
        }

        private static EventSource.ServerSentErrorEventArgs ConvertExceptionEventArgs(ExceptionEventArgs args)
        {
            return new EventSource.ServerSentErrorEventArgs {Exception = args.Exception};
        }
    }
}