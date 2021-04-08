using Newtonsoft.Json;
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
            var bodyBuffer = Encoding.UTF8.GetBytes(signupFormModelJSON);
            var byteContent = new ByteArrayContent(bodyBuffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var client = new HttpClient();
            client.BaseAddress = new System.Uri($"{Constants.kBankApiPath}UserController/RegisterUser");

            var response = await client.PostAsync(client.BaseAddress, byteContent);
            return response;
        }
    }
}
