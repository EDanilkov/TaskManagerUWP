using Newtonsoft.Json;
using SharedServicesModule.ResponseModel;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BusinessLogicModule.Services
{
    public static class RequestService
    {
        static HttpClient _client = HttpClientAccessor.HttpClient;

        public static async Task<T> Get<T>(string path)
        {
            int attemptCount = 0;
            while(attemptCount < 5)
            {
                HttpResponseMessage response = await _client.GetAsync(new Uri(path));
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _client = HttpClientAccessor.HttpClient;
                    attemptCount++;
                    continue;
                }
                else
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
                    }
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
            throw new Exception("The user is not authorized");
        }

        public static async Task<NewResponseModel> Post(string path, string json)
        {
            HttpStringContent content = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            int attemptCount = 0;
            while (attemptCount < 5)
            {
                HttpResponseMessage response = await _client.PostAsync(new Uri(path), content);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _client = HttpClientAccessor.HttpClient;
                    attemptCount++;
                    continue;
                }
                else
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
                    }
                    return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
                }
            }
            throw new Exception("The user is not authorized");
        }

        public static async Task<NewResponseModel> Put(string path, string json)
        {
            HttpStringContent content = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            int attemptCount = 0;
            while (attemptCount < 5)
            {
                HttpResponseMessage response = await _client.PutAsync(new Uri(path), content);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _client = HttpClientAccessor.HttpClient;
                    attemptCount++;
                    continue;
                }
                else
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
                    }
                    return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
                }
            }
            throw new Exception("The user is not authorized");
        }

        public static async Task<NewResponseModel> Delete(string path)
        {
            int attemptCount = 0;
            while (attemptCount < 5)
            {
                HttpResponseMessage response = await _client.DeleteAsync(new Uri(path));
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _client = HttpClientAccessor.HttpClient;
                    attemptCount++;
                    continue;
                }
                else
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
                    }
                    return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
                }
            }
            throw new Exception("The user is not authorized");
        }

        
    }
}
