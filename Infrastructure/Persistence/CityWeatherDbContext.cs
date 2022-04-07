using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class CityWeatherDbContext : DbContext, ICityWeatherDbContext
    {
        public CityWeatherDbContext(DbContextOptions<CityWeatherDbContext> options) : base(options)
        { }

        public DbSet<CityWeather> CityWeathers { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
