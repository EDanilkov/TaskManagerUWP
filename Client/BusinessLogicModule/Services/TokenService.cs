using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace SharedServicesModule.Services
{
    public static class TokenService
    {
        public async static Task<TokenResponseModel> GetToken(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            HttpStringContent content = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);
            HttpClient httpClient = new HttpClient(filter);
            
            HttpResponseMessage response = await httpClient.PostAsync(new Uri(Consts.BaseAddress + "api/accounts/token"), content);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception(Windows.UI.Xaml.Application.Current.Resources["m_error_bad_signup"].ToString());
            }
            TokenResponseModel tokenResponseModel = JsonConvert.DeserializeObject<TokenResponseModel>(await response.Content.ReadAsStringAsync());

            Resources.Token = tokenResponseModel.AccessToken;
            return tokenResponseModel;
        }
    }
}
