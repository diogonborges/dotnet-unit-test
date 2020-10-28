using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Movies.Api.Tests
{
    public static class TestUtils
    {
        private static StringContent CreateContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<object> SendCreatedRequest(HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            return await Deserialize(response);
        }

        public static async Task<object> SendOkRequest(HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            return await Deserialize(response);
        }

        public static HttpRequestMessage CreatePostMessage(string url, object content = null)
        {
            HttpContent messageContent = null;

            if (content != null)
            {
                messageContent = CreateContent(content);
            }

            return CreateMessage(url, HttpMethod.Post, messageContent);
        }

        public static HttpRequestMessage CreateGetRequest(string url)
        {
            return CreateMessage(url, HttpMethod.Get);
        }

        public static HttpRequestMessage CreateDeleteRequest(string url)
        {
            return CreateMessage(url, HttpMethod.Delete);
        }

        public static HttpRequestMessage CreatePutRequest(string url, object content)
        {
            if (content == null)
            {
                return CreateMessage(url, HttpMethod.Put);
            }

            return CreateMessage(url, HttpMethod.Put, CreateContent(content));
        }

        private static HttpRequestMessage CreateMessage(string url, HttpMethod method, HttpContent content = null)
        {
            if (content == null)
            {
                return new HttpRequestMessage(method, url);
            }

            return new HttpRequestMessage(method, url)
            {
                Content = content
            };
        }

        private static async Task SendRequestAssertStatus(HttpClient client, HttpRequestMessage request, HttpStatusCode status)
        {
            var response = await client.SendAsync(request);

            Assert.AreEqual(status, response.StatusCode);
        }

        public static async Task SendNoContentRequest(HttpClient client, HttpRequestMessage request)
        {
            await SendRequestAssertStatus(client, request, HttpStatusCode.NoContent);
        }

        public static async Task SendNotFoundRequest(HttpClient client, HttpRequestMessage request)
        {
            await SendRequestAssertStatus(client, request, HttpStatusCode.NotFound);
        }

        private static async Task<object> Deserialize(HttpResponseMessage response)
        {
            var stringResponseAll = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(stringResponseAll);
        }
    }
}