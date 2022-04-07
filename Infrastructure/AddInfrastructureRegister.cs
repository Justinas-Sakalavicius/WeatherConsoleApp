using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

namespace Infrastructure.Persistence
{
    public static class AddInfrastructureRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<CityWeatherDbContext>(options =>
                    options.UseInMemoryDatabase("CityWeatherDB"));
            }
            else
            {
                services.AddDbContext<CityWeatherDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(CityWeatherDbContext).Assembly.FullName)));
            }

            services.AddScoped<ICityWeatherDbContext>(provider => provider.GetRequiredService<CityWeatherDbContext>());

            return services;
        }
    }
}
