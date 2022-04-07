using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICityWeatherService
    {
        Task<CityWeather> GetCityWeather(string name, CancellationToken cancellationToken);
        Task<int> SaveCitiesWeather(List<CityWeather> cityWeathers, CancellationToken cancellationToken);
    }
}