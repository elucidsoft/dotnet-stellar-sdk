using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Vue2SpaSignalR.Services.Hubs
{
    public class CounterHub : Hub
    {
        
    }

    public class Counter : HostedService
    {
        public Counter(IHubContext<CounterHub> context)
        {
            Clients = context.Clients;
        }

        private IHubClients Clients { get; }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            int counter = 0;

            while (true)
            {
                await Clients.All.InvokeAsync("increment", counter);

                var task = Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                try
                {
                    await task;
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                counter++;
            }
        }
    }
}
