using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Configuration;

namespace Application.Services.Scheduler
{
    public static class SchedulerServiceExtensions
    {
        public static IServiceCollection AddSchedulerJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : SchedulerService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
