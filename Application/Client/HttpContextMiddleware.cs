using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Application.Interfaces;

namespace Application.Client
{
    public class HttpContextMiddleware : DelegatingHandler
    {
        private readonly IUserApiAuthenticationService _authenticationService;
        private readonly ILogger<HttpContextMiddleware> _logger;
        public HttpContextMiddleware(IUserApiAuthenticationService authenticationService, ILogger<HttpContextMiddleware> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage;
            try
            {
                var bearer = await _authenticationService.RetrieveToken();
                if (string.IsNullOrEmpty(bearer))
                {
                    throw new Exception($"Access token is missing for the request {request.RequestUri}");
                }
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", bearer);

                httpResponseMessage = await base.SendAsync(request, cancellationToken);
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);
                throw;
            }
            return httpResponseMessage;
        }
    }
}
