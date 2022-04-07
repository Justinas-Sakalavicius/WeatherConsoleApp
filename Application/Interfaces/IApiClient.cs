using Application.Responses;

namespace Application.Interfaces
{
    public interface IApiClient
    {
        Task<List<string>> GetCitiesAsync(CancellationToken cancellationToken);
        Task<CityWeatherResponse> GetCitiesWeatherAsync(string city,CancellationToken cancellationToken);
    }
}
