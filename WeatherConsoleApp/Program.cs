using Microsoft.Extensions.Hosting;
using Serilog;

namespace WeatherConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Log.Logger.Information("Application Starting");

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostBuilderContext, serviceCollection) => new Startup(hostBuilderContext.Configuration).ConfigureServices(serviceCollection)).UseSerilog();
    }
}
