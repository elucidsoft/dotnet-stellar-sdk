//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Reflection.Metadata;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace stellar_dotnetcore_sdk
//{
//    public enum EventSourceState { Connecting = 0, Open = 1, Closed = 2 }

//    /// <summary>
//    /// Custom light-weight EventSource Implementation that follows the https://www.w3.org/TR/eventsource/ spec.
//    /// Author: Eric Malamisura
//    /// </summary>
//    public class EventSource : IDisposable
//    {
//        private Uri _uri;
//        public EventSourceState State { get; set; }

//        public EventSource(Uri uri)
//        {
//            _uri = uri;
//        }

//        public async Task<EventSource> Connect()
//        {
//            var httpClient = new HttpClient();
//            httpClient.Timeout = Timeout.InfiniteTimeSpan;
//            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
//            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };


//            await Task.Run(async () =>
//            {
//                int retryTime = 0;
//                StringBuilder sb = new StringBuilder();

//                //while (true)
//                //{
//                    string stream = null;
//                    try
//                    {
//                        stream = await httpClient.GetStringAsync(_uri);

//                    }
//                    catch (IOException e)
//                    {
//                        await Task.Delay(retryTime);
//                       // continue;
//                    }
                    

//                    if (String.IsNullOrWhiteSpace(stream))
//                    {
//                        await Task.Delay(TimeSpan.FromSeconds(1));
//                        //continue;
//                    }

//                    var result = stream.Split('\n');
                    
//                    foreach (var line in result)
//                    {
//                        if (String.IsNullOrEmpty(line))
//                        {
//                            if (sb.Length > 0)
//                            {
//                                Message?.Invoke(this, sb.ToString());
//                                sb.Clear();
//                            }
//                            continue;
//                        }

//                        int splitIndex = line.IndexOf(':', 0);
//                        if (splitIndex > 0)
//                        {
//                            var eventType = line.Substring(0, splitIndex);
//                            var message = line.Substring(splitIndex + 2);

//                            switch (eventType)
//                            {
//                                case "event":
//                                    break;
//                                case "data":
//                                    sb.Append(message);
//                                    break;
//                                case "id":
//                                    httpClient.DefaultRequestHeaders.Remove("Last-Event-ID");
//                                    httpClient.DefaultRequestHeaders.Add("Last-Event-ID", new[] { message });
//                                    break;
//                                case "retry":
//                                    retryTime = Convert.ToInt32(message);
//                                    break;
//                            }
//                        }
//                    }
//                //}

//            });




//            return this;
//        }

//        public event EventHandler<string> Message;

//        public void Dispose()
//        {
//        }
//    }
//}