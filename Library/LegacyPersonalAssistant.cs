using dotnet_unit_test.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace dotnet_unit_test
{
    public class LegacyPersonalAssistant
    {
        public async Task<WeatherReport> GetWeatherAsync(string city)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=be299e03491ccf88fa21a27aa5da7b99&units=imperial");
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherReport>(content);
        }

        public async Task<bool> PerformLongRunningTaskAsync(int delayInSeconds)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://slowwly.robertomurray.co.uk/delay/{delayInSeconds}/url/http://www.google.co.uk");
            return response.IsSuccessStatusCode;
        }

        public bool IsPrime(int candidate)
        {
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
