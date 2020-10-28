using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Movies.Api.Tests;
using NUnit.Framework;

namespace Movies.Api.Integration.Tests.Controllers
{
    internal class MovieControllerTests
    {
        private CustomWebApplicationFactory<Startup> factory;
        private HttpClient client;
        private const string RootRoute = "/movies";
        private const string MovieTitle = "The Shawshank Redemption";

        [OneTimeSetUp]
        public void SetUp()
        {
            factory = new CustomWebApplicationFactory<Startup>();
            client = factory.CreateClient();
        }

        [Test, Order(1)]
        public async Task Get()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{RootRoute}?title={MovieTitle}");

            var response = await client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponseAll = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<object>(stringResponseAll);
        }
    }
}
