using SharedServicesModule.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BusinessLogicModule.Services
{
    public static class HttpClientAccessor
    {

        public static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Resources.Token);
            return client;
        }

        private static HttpClient client = CreateHttpClient();

        public static HttpClient HttpClient
        {
            get
            {
                return CreateHttpClient();
            }
        }
    }
}
