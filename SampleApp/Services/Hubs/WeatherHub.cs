using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Vue2SpaSignalR.Services.Hubs
{
    public class WeatherHub : Hub
    {
       
    }

    public class Weather : HostedService
    {
        public Weather(IHubContext<WeatherHub> context)
        {
            Clients = context.Clients;
        }

        private IHubClients Clients { get; }

        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task UpdateWeatherForecasts()
        {
            var rng = new Random();
            var randomWeatherForescast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            await Clients.All.InvokeAsync("weather", randomWeatherForescast);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                await UpdateWeatherForecasts();

                var task = Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                try
                {
                    await task;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
