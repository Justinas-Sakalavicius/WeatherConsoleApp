using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Application;
using Infrastructure.Persistence;

namespace WeatherConsoleApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            BuildLogger();
            services.AddInfrastructure(Configuration);
            services.AddApplication(Configuration);
        }

        private void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        private void BuildLogger()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            BuildConfiguration(builder);
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Build())
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .CreateLogger();
        }
    }
}
