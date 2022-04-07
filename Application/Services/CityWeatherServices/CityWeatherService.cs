using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.CityWeatherServices
{
    public class CityWeatherService : ICityWeatherService
    {
        private readonly ILogger<CityWeatherService> _logger;
        private readonly ICitiesWeatherRepository _cityWeatherRepository;

        public CityWeatherService(ILogger<CityWeatherService> logger, ICitiesWeatherRepository cityWeatherRepository)
        {
            _logger = logger;
            _cityWeatherRepository = cityWeatherRepository;
        }

        public async Task<int> SaveCitiesWeather(List<CityWeather> cityWeathers, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{SaveCitiesWeather} invoked");
            return await _cityWeatherRepository.AddCitiesWeather(cityWeathers, cancellationToken);
        }

        public async Task<CityWeather> GetCityWeather(string name, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{GetCityWeather} invoked");
            return await _cityWeatherRepository.GetCityCurrentWeatherByName(name, cancellationToken);
        }
    }
}
