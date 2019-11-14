using SharedServicesModule.Models;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

namespace BusinessLogicModule.Services
{
    public static class HttpClientAccessor
    {
        public static HttpClient CreateHttpClient()
        {
            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);
            HttpClient httpClient = new HttpClient(filter);
            httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Resources.Token = Resources.Token == null ? "" : Resources.Token);
            return httpClient;
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
