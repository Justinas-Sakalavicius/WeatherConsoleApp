using Domain.Entities;

namespace Application.Interfaces
{ 
    public interface ICitiesWeatherRepository
    {
        Task<int> AddCitiesWeather(List<CityWeather> citiesWeather, CancellationToken cancellationToken);
        Task<IEnumerable<CityWeather>> GetCities(CancellationToken cancellationToken);
        Task<CityWeather> GetCityCurrentWeatherByName(string cityName, CancellationToken cancellationToken);
    }
}