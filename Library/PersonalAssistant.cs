using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using dotnet_unit_test.Config;
using dotnet_unit_test.Models;

namespace dotnet_unit_test
{
    /// <summary>
    /// My personal assistant, clearly breaking Single Responsibility Principle
    /// </summary>
    public class PersonalAssistant
    {
        private readonly ILogger<PersonalAssistant> logger;
        private readonly HttpClient httpClient;
        private readonly WeatherApiConfig weatherApiConfig;
        private readonly IHouseService houseService;

        public PersonalAssistant(ILogger<PersonalAssistant> logger, HttpClient httpClient, WeatherApiConfig weatherApiConfig, IHouseService houseService)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.weatherApiConfig = weatherApiConfig;
            this.houseService = houseService;
        }

        public static string GetTodaysDate()
        {
            return DateTime.UtcNow.Date.ToString("D");
        }

        public void SetOvenTemperature(int degreesInCelsius)
        {
            SampleModel sampleModel = null;
            if (degreesInCelsius > 0)
            {
                houseService.TurnOvenOn();
                houseService.SetOvenTemperature(degreesInCelsius);
                sampleModel = new SampleModel();
            }

            logger.LogDebug(sampleModel.Counter.ToString());
        }

        public async Task<WeatherReport> GetWeatherAsync(string city)
        {
            logger.LogDebug($"GetWeatherAsync called with:{city}");

            var response = await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={weatherApiConfig.ApiKey}&units=metric");
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherReport>(content);
        }

        public async Task<bool> PerformLongRunningTaskAsync(int delayInSeconds)
        {
            logger.LogDebug($"PerformLongRunningTaskAsync called with:{delayInSeconds}");

            var response = await httpClient.GetAsync($"http://slowwly.robertomurray.co.uk/delay/{delayInSeconds}/url/http://www.google.co.uk");
            return response.IsSuccessStatusCode;
        }

        public bool IsPrime(int candidate)
        {
            logger.LogDebug($"IsPrime called with:{candidate}");
            if (candidate < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(candidate); divisor++)
            {
                if (candidate % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
