using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Repositories.Cities
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly ICityWeatherDbContext _context;
        public CitiesRepository(ICityWeatherDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCities(CancellationToken cancellationToken)
        {
            return await _context.Cities
                .AsNoTracking()
                .OrderByDescending(d => d.Name)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<City> GetCityByName(string name, CancellationToken cancellationToken)
        {
            return await _context.Cities
                .AsNoTracking()
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<int> AddCities(List<City> cities, CancellationToken cancellationToken)
        {
            await _context.Cities.AddRangeAsync(cities, cancellationToken).ConfigureAwait(false);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ContainsCity(List<City> cities, CancellationToken cancellationToken)
        {
            return await _context.Cities.AnyAsync(x => cities.Contains(x) ,cancellationToken).ConfigureAwait(false);
        }
    }
}
