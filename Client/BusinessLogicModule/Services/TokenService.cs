using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SharedServicesModule.Services
{
    public static class TokenService
    {
        public async static Task<TokenResponseModel> GetToken(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://localhost:44316/api/accounts/token", content);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(Application.Current.Resources["m_error_bad_signup"].ToString());
            }
            TokenResponseModel tokenResponseModel = JsonConvert.DeserializeObject<TokenResponseModel>(await response.Content.ReadAsStringAsync());

            Resources.Token = tokenResponseModel.AccessToken;
            return tokenResponseModel;
        }
    }
}
