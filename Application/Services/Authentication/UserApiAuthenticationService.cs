using Application.Configuration;
using Application.Responses;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Xml;
using Newtonsoft.Json;

namespace Application.Services.Authentication
{
    public class UserApiAuthenticationService : IUserApiAuthenticationService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;

        public UserApiAuthenticationService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<string> RetrieveToken()
        {
            DateTime expirationDate;
            if (!_memoryCache.TryGetValue("Bearer", out string token))
            {
                var body = AddApplicationRegister.GetCredentials();
                //var body = new { username = "meta", password = "site" };
                var data = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiUrlConstants.PostAuthorize, data);
                
                var authstring = await response.Content.ReadAsStringAsync();
                var auth = JsonConvert.DeserializeObject<AuthResponse>(authstring);
                _memoryCache.Set("Bearer", auth.Bearer, new DateTimeOffset(DateTime.UtcNow.AddHours(1)));
                token = auth.Bearer;
            }
            return token;
        }
    }
}
