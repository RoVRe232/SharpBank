using Newtonsoft.Json;
using SharpBank.Services;
using SharpBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models.Transactions
{
    public class MakeTransactionFormModel
    {
        public string SenderIban { get; set; }
        public string ReceiverIban { get; set; }
        public string ReceiverFullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double Currency { get; set; }

        public async Task<HttpResponseMessage> SendNewTransactionRequestToApiAsync()
        {
            var signupFormModelJSON = JsonConvert.SerializeObject(this);
            var requestContent = new StringContent(signupFormModelJSON, Encoding.UTF8, "application/json");
            var requestUri = new Uri($"{Constants.kBankApiDomain}/api/transactions/newtransaction");

            var response = await HttpService.Instance.httpClient.PostAsync(requestUri, requestContent);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
