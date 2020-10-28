using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using dotnet_unit_test;
using dotnet_unit_test.Config;
using Library.xUnit.Tests.Helper;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace Library.xUnit.Tests
{
    public class PersonalAssistantTests : IDisposable
    {
        private readonly PersonalAssistant personalAssistant;
        private readonly Mock<HttpMessageHandler> httpMessageHandler;
        private readonly Mock<IHouseService> houseService;

        public PersonalAssistantTests()
        {
            httpMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new Mock<HttpClient>(httpMessageHandler.Object);
            var logger = new Mock<ILogger<PersonalAssistant>>();
            houseService = new Mock<IHouseService>();
            const string weatherApiKey = "won't matter";
            personalAssistant = new PersonalAssistant(logger.Object, httpClient.Object, new WeatherApiConfig() { ApiKey = weatherApiKey }, houseService.Object);
        }

        [Fact]
        public async Task PerformLongRunningTaskAsync_Returns_true()
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
            var response = await personalAssistant.PerformLongRunningTaskAsync(3000);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task GetWeatherAsync_Returns_correct_city()
        {
            // Arrange
            const string city = "Porto";

            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())

                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"id\": 2735943,\"name\": \"Porto\",\"cod\": 200}"),
                });

            // Act
            var weatherReport = await personalAssistant.GetWeatherAsync(city);

            // Assert
            Assert.NotNull(weatherReport);
            Assert.Equal(city, weatherReport.name);
        }

        [Theory]
        [JsonFileData("MockData/portoWeatherReport.json")]
        public async Task GetWeatherAsync_Returns_correct_city_When_response_is_successful_weather_report(string responseContent)
        {
            // Arrange
            const string city = "Porto";

            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())

                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            // Act
            var weatherReport = await personalAssistant.GetWeatherAsync(city);

            // Assert
            Assert.NotNull(weatherReport);
            Assert.Equal(city, weatherReport.name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_Returns_false_When_candidate_lower_than_2(int candidate)
        {
            // Arrange

            // Act
            var result = personalAssistant.IsPrime(candidate);

            // Assert
            Assert.False(result, $"{candidate} should not be prime");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void IsPrime_Returns_true_When_candidate_is_prime(int candidate)
        {
            // Arrange

            // Act
            var result = personalAssistant.IsPrime(candidate);

            // Assert
            Assert.True(result, $"{candidate} should be prime");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        public void IsPrime_Returns_false_When_candidate_is_not_prime(int candidate)
        {
            // Arrange

            // Act
            var result = personalAssistant.IsPrime(candidate);

            // Assert
            Assert.False(result, $"{candidate} should not be prime");
        }

        /// <summary>
        /// Example of 100% coverage of a method but a null reference exception is still hidden
        /// </summary>
        [Fact]
        public void SetOvenTemperature_Sets_temperature_When_degrees_is_positive()
        {
            // Arrange

            // Act
            personalAssistant.SetOvenTemperature(200);

            // Assert
            houseService.Verify(x => x.SetOvenTemperature(200), Times.Once);
        }

        // This would fail
        //[Fact]
        //public void SetOvenTemperature_Does_not_do_anything_When_degrees_is_negative()
        //{
        //    // Arrange

        //    // Act
        //    personalAssistant.SetOvenTemperature(-1);

        //    // Assert
        //    houseService.Verify(x => x.SetOvenTemperature(200), Times.Never);
        //}


        public void Dispose()
        {
            // cleanup tasks
        }
    }
}
