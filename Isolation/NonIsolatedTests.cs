using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using dotnet_unit_test;
using dotnet_unit_test.Config;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;
using Xunit.Sdk;

namespace Isolation
{
    public class NonIsolatedTests
    {
        private readonly PersonalAssistant personalAssistant;
        private readonly Mock<HttpMessageHandler> httpMessageHandler;

        public NonIsolatedTests()
        {
            httpMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new Mock<HttpClient>(httpMessageHandler.Object);
            var logger = new Mock<ILogger<PersonalAssistant>>();
            var houseService = new Mock<IHouseService>();
            var weatherApiKey = "won't matter";
            personalAssistant = new PersonalAssistant(logger.Object, httpClient.Object, new WeatherApiConfig { ApiKey = weatherApiKey }, houseService.Object);
        }

        /// <summary>
        /// Not isolated/scoped.
        /// </summary>
        [Fact]
        public async Task PerformLongRunningTaskAsync_if_year_is_2020_Returns_true()
        {
            // Arrange
            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())

                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("")
                });

            // Act
            var todaysDate = PersonalAssistant.GetTodaysDate();
            bool result;
            if (todaysDate.Contains("2020"))
            {
                result = await personalAssistant.PerformLongRunningTaskAsync(3000);
            }
            else
            {
                throw new XunitException("Can't perform task because the date is:");
            }

            // Assert
            Assert.True(result);
        }
    }
}
