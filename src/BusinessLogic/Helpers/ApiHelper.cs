using System;
using System.Collections.Generic;
using System.Net.Http;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public class ApiHelper
    {
        private string apiUrl;
        private IApiClient client;

        public ApiHelper(string api, IApiClient mockedClient = null)
        {
            apiUrl = api;
            client = (mockedClient != null) ? mockedClient : new ApiClient(api);
        }

        public string AcquireToken(string email, string password, string appId)
        {
            var param = new Dictionary<string, string>
            {
                {"Email", email},
                {"Password", password},
                {"AppId", appId}
            };

            return CallPost("api/auth/acquiretoken", param);
        }

        public string Verify(string token)
        {
            var param = new Dictionary<string, string>();
            param.Add("Token", token);

            return CallPost("api/auth/verify", param, token);
        }

        public string Restricted(string data, string token)
        {
            var param = new Dictionary<string, string>();
            param.Add("Data", data);

            return CallPost("api/auth/restricted", param, token);
        }

        internal string CallPost(string route, Dictionary<string, string> dict, string token = null)
        {
            if (dict != null)
            {
                foreach (var param in dict)
                {
                    client.AddParameter(param.Key, param.Value);
                }
            }

            if (token != null)
            {
                client.AddHeader("Authorization", $"Bearer {token}");
            }

            var responseString = client.MakeCallPost(route);

            return responseString;
        }

        internal string CallGet(string route, Dictionary<string, string> dict, string token = null)
        {
            var builder = new UriBuilder($"{apiUrl}{route}");
            builder.Port = -1;
            var query = QueryHelpers.ParseQuery(builder.Query);
            foreach (var item in dict)
            {
                query[item.Key] = item.Value;
            }
            builder.Query = query.ToString();

            string apiFullUrl = builder.ToString();

            HttpClient client = new HttpClient();

            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }

            client.BaseAddress = new Uri(apiUrl);
            var response = client.GetAsync(apiFullUrl).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return responseString;
        }
    }
}
