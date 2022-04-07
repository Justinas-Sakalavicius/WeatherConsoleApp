using Application.Interfaces;
using Application.Services.CityWeatherServices;
using Application.Services.Scheduler;
using Microsoft.Extensions.Logging;

namespace Application.Services.Tracker
{
    public class CityWeatherTrackerService : SchedulerService
    {
        private readonly ILogger<CityWeatherTrackerService> _logger;
        private readonly IApiClient _client;
        private readonly ICityService _cityService;

        public CityWeatherTrackerService(
            IScheduleConfig<CityWeatherTrackerService> config, 
            ILogger<CityWeatherTrackerService> logger, 
            IApiClient client,
            ICityService cityService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _client = client;
            _cityService = cityService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TrackWeatherChanges starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task PeriodicallyCheckWeather(CancellationToken cancellationToken)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(5000);

            var cities = await _client.GetCitiesAsync(cancellationTokenSource.Token);

            if (!await _cityService.AreCityInDatabase(cities, cancellationTokenSource.Token))
            {
                await _cityService.SaveCities(cities, cancellationTokenSource.Token);
            }
            
            var result = await _client.GetCitiesWeatherAsync(cities[new Random().Next(0, cities.Count - 1)], cancellationToken);
            _logger.LogInformation($"City: {result.City} Temperature: {result.Temperature} Precipitation: {result.Precipitation} Weather: {result.Weather}");

            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} PeriodicallyCheckWeather is working.");
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TrackWeatherChanges is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
