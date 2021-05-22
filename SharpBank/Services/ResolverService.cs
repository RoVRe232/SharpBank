﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharpBank.Models.Accounts;
using SharpBank.Models.Transactions;
using SharpBank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class ResolverService : IResolverService
    {
        LoginService _loginService;
        public ResolverService(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IEnumerable<BankAccountModel> GetLoggedInUserAccounts(HttpContext httpContext)
        {
            //TODO add resolver for resources to avoid duplicated code (like getCurrentUserAccounts, getCurrentUserCards...)
            // get logged in user's username
            var username = _loginService.GetLoggedInUsername(httpContext);
            if (username == null)
                return null;

            // Get bank accounts from API
            //TODO consider adding new endpoint called accounts/names to avoid retrieving all data
            var response = HttpService.Instance.SendRequestToApiAsync(username, "/api/user/accounts").Result;
            if (!response.IsSuccessStatusCode)
                return null;

            // Get json serialized bank accounts from response content
            var bankAccounts = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(bankAccounts))
                return null;

            // Deserialize the bank accounts json into collection
            ICollection<BankAccountModel> bankAccountsArray =
                ((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(bankAccounts)).ToObject<List<BankAccountModel>>();

            return bankAccountsArray;
        }

        public IEnumerable<T> GetLoggedInUserData<T>(HttpContext httpContext, String endpoint)
        {
            //TODO add resolver for resources to avoid duplicated code (like getCurrentUserAccounts, getCurrentUserCards...)
            // get logged in user's username
            var username = _loginService.GetLoggedInUsername(httpContext);
            if (username == null)
                return null;

            // Get bank accounts from API
            var response = HttpService.Instance.SendRequestToApiAsync(username, endpoint).Result;
            if (!response.IsSuccessStatusCode)
                return null;

            // Get json serialized bank accounts from response content
            var content = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(content))
                return null;

            // Deserialize the bank accounts json into collection
            ICollection<T> contentArray =
                ((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(content)).ToObject<List<T>>();

            return contentArray;
        }

    }
}
