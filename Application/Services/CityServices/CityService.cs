using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services.CityServices
{
    public class CityService : ICityService
    {
        private readonly ILogger<CityService> _logger;
        private readonly ICitiesRepository _cityRepository;

        public CityService(ILogger<CityService> logger, ICitiesRepository cityRepository)
        {
            _logger = logger;
            _cityRepository = cityRepository;
        }

        public async Task<int> SaveCities(List<string> cities, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{SaveCities} invoked");
            var mapped = MapToCityObjects(cities);
            return await _cityRepository.AddCities(mapped, cancellationToken);
        }

        public async Task<City> GetCityName(string name, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{GetCityName} invoked");
            return await _cityRepository.GetCityByName(name, cancellationToken);
        }

        public async Task<bool> AreCityInDatabase(List<string> cities, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{GetCityName} invoked");
            var mapped = MapToCityObjects(cities);
            return await _cityRepository.ContainsCity(mapped, cancellationToken);
        }

        private List<City> MapToCityObjects(List<string> temp)
        {
            var cities = new List<City>();
            foreach (var element in temp)
            {
                cities.Add(new City
                {
                    Name = element
                });
            }
            return cities;
        }
    }
}
