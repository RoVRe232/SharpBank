using Newtonsoft.Json;
using SharpBank.Services;
using SharpBank.Utils;
using System;
using System.Net.Http;
using System.Text;

namespace SharpBank.Models.Login
{
    public class SignupFormModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnp { get; set; }
        public string Ci { get; set; }
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendSignupRequestToApiAsync()
        {
            var signupFormModelJSON = JsonConvert.SerializeObject(this);
            var requestContent = new StringContent(signupFormModelJSON, Encoding.UTF8, "application/json");
            var requestUri = new Uri($"{Constants.kBankApiDomain}/api/user/signuprequest");

            var response = await HttpService.Instance.httpClient.PostAsync(requestUri, requestContent);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
