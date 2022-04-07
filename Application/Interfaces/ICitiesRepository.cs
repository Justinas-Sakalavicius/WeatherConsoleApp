using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICitiesRepository
    {
        Task<int> AddCities(List<City> cities, CancellationToken cancellationToken);
        Task<bool> ContainsCity(List<City> cities, CancellationToken cancellationToken);
        Task<IEnumerable<City>> GetCities(CancellationToken cancellationToken);
        Task<City> GetCityByName(string name, CancellationToken cancellationToken);
    }
}