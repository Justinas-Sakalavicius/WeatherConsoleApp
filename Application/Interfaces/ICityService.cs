using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICityService
    {
        Task<City> GetCityName(string name, CancellationToken cancellationToken);
        Task<int> SaveCities(List<string> cities, CancellationToken cancellationToken);
        Task<bool> AreCityInDatabase(List<string> cities, CancellationToken cancellationToken);
    }
}