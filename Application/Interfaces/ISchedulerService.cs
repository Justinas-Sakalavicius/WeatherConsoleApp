namespace Application.Interfaces
{
    public interface ISchedulerService
    {
        void Dispose();
        Task PeriodicallyCheckWeather(CancellationToken cancellationToken);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}