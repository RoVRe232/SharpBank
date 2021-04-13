using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

    }
}
