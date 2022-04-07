using Application.Configuration;
using Application.Interfaces;
using Application.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Client
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<string>> GetCitiesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrlConstants.GetAllCities, cancellationToken);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                var cityCollection = JsonConvert.DeserializeObject<List<string>>(result);
                _logger.LogInformation($"{DateTime.Now:hh:mm:ss} GetCitiesAsync is working.");
                return cityCollection;
            }
            catch (HttpRequestException)
            {
                _logger.LogError("An error occurred.");
            }
            catch (NotSupportedException)
            {
                _logger.LogError("The content type is not supported.");
            }
            catch (JsonException)
            {
                _logger.LogError("Invalid JSON.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to read content");
            }
            return null;
        }


        public async Task<CityWeatherResponse> GetCitiesWeatherAsync(string city, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiUrlConstants.GetCityWeather}/{city}", cancellationToken);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                var cityWeather = JsonConvert.DeserializeObject<CityWeatherResponse>(result);
                _logger.LogInformation($"{DateTime.Now:hh:mm:ss} GetCitiesWeatherAsync is working.");
                return cityWeather;
            }
            catch (HttpRequestException)
            {
                _logger.LogError("An error occurred.");
            }
            catch (NotSupportedException)
            {
                _logger.LogError("The content type is not supported.");
            }
            catch (System.Text.Json.JsonException)
            {
                _logger.LogError("Invalid JSON.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to read content");
            }
            return null;
        }
    }
}
