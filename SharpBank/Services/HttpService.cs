using Newtonsoft.Json;
using SharpBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public sealed class HttpService
    {
        private static readonly HttpService instance = new HttpService();

        public HttpClient httpClient { get; }

        static HttpService() { }
        private HttpService() 
        {
            httpClient = new HttpClient();
        }
        public static HttpService Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task<HttpResponseMessage> SendRequestToApiAsync<T>(T model, string endpoint)
        {
            var signupFormModelJSON = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(signupFormModelJSON, Encoding.UTF8, "application/json");
            var requestUri = new Uri($"{Constants.kBankApiDomain}{endpoint}");

            var response = await Instance.httpClient.PostAsync(requestUri, requestContent);
            response.EnsureSuccessStatusCode();

            return response;
        }

    }
}
