using Newtonsoft.Json;
using SharpBank.Services;
using SharpBank.Utils;
using System;
using System.Net.Http;
using System.Text;

namespace SharpBank.Models.Login
{
    public class LoginFormModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendLoginRequestToApiAsync()
        {
            var signupFormModelJSON = JsonConvert.SerializeObject(this);
            var requestContent = new StringContent(signupFormModelJSON, Encoding.UTF8, "application/json");
            var requestUri = new Uri($"{Constants.kBankApiDomain}/api/user/loginrequest");

            var response = await HttpService.Instance.httpClient.PostAsync(requestUri, requestContent);
            response.EnsureSuccessStatusCode();

            string authJwt = await response.Content.ReadAsStringAsync();

            return response;
        }
    }
}
