using Application.Client;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class UnitTest
    {
        [Fact]
        public async Task GetAllCities_ExpectCityList()
		{
			//arrange
			var expectedCityList = new List<string>
			{
				"Vilnius", "something"
			};

			var json = JsonConvert.SerializeObject(expectedCityList);

			string url = "https://metasite-weather-api.herokuapp.com"; ///api/cities

			HttpResponseMessage httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            Mock<HttpMessageHandler> mockHandler = new();
			mockHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync",
				ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri.ToString().StartsWith(url)),
				ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(httpResponse);
			HttpClient httpClient = new(mockHandler.Object);
			httpClient.BaseAddress = new Uri(url);
			Mock<ILogger<ApiClient>> loggerMock = new();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.CancelAfter(5000);


			var service = new ApiClient(httpClient, loggerMock.Object);

			//act
			var actualCityList = await service.GetCitiesAsync(cancellationTokenSource.Token);

			//assert
			actualCityList.Should().ContainMatch("Vilnius");
			Assert.Equal(expectedCityList, actualCityList);
		}
    }
}