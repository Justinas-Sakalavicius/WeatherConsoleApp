using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services.Scheduler;
using Application.Client;
using Application.Configuration;
using Application.Services.Authentication;
using System.Net.Http.Headers;
using Application.Repositories.Cities;
using Application.Repositories.CitiesWeather;
using Application.Services.Tracker;
using Application.Services.CityServices;
using Application.Services.CityWeatherServices;

namespace Application
{
    public static class AddApplicationRegister
    {
        private static WeatherAPISettings ApiSettingsContainer;
        private const string mediaType = "application/json";
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            ApiSettingsContainer = GetWeatherAPISettings(configuration);

            services.AddHttpClient<IUserApiAuthenticationService, UserApiAuthenticationService>(client =>
            {
                client.BaseAddress = new Uri(ApiSettingsContainer.Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            });

            services.AddTransient<ICitiesRepository, CitiesRepository>();
            services.AddTransient<ICitiesWeatherRepository, CitiesWeatherRepository>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityWeatherService, CityWeatherService>();

            services.AddTransient<HttpContextMiddleware>();
            services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri(ApiSettingsContainer.Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            }).AddHttpMessageHandler<HttpContextMiddleware>();

            services.AddSchedulerJob<CityWeatherTrackerService>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = configuration["IntervalInSeconds"];
            });

            return services;
        }

        public static ApiUserCredentials GetCredentials()
        {
            return new ApiUserCredentials
            {
                Username = ApiSettingsContainer.Username,
                Password = ApiSettingsContainer.Password
            };
        }

        private static WeatherAPISettings GetWeatherAPISettings(IConfiguration configuration)
        {
            return new WeatherAPISettings
            {
                Url = configuration["WeatherAPISettings:Url"],
                Username = configuration["WeatherAPISettings:Username"],
                Password = configuration["WeatherAPISettings:Password"],
            };
        }
    }
}
