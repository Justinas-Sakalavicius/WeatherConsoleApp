using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Repositories.CitiesWeather
{
    public class CitiesWeatherRepository : ICitiesWeatherRepository
    {
        private readonly ICityWeatherDbContext _context;
        public CitiesWeatherRepository(ICityWeatherDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CityWeather>> GetCities(CancellationToken cancellationToken)
        {
            return await _context.CityWeathers
                .Include(x => x.City)
                .AsNoTracking()
                .OrderByDescending(d => d.City.Name)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<CityWeather> GetCityCurrentWeatherByName(string cityName, CancellationToken cancellationToken)
        {
            return await _context.CityWeathers
                .Include(c => c.City)
                .AsNoTracking()
                .Where(d => d.City.Name == cityName)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<int> AddCitiesWeather(List<CityWeather> citiesWeather, CancellationToken cancellationToken)
        {
            await _context.CityWeathers.AddRangeAsync(citiesWeather, cancellationToken).ConfigureAwait(false);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
