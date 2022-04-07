using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ICityWeatherDbContext
    {
        DbSet<CityWeather> CityWeathers { get; }
        DbSet<City> Cities { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
