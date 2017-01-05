using System;
using System.Collections.Generic;
using System.Net.Http;

namespace JwtManager.Helpers
{
    public class ApiHelper
    {
        private string apiUrl;

        public ApiHelper(string api)
        {
            apiUrl = api;
        }

        public string AcquireToken(string email, string password, string appId)
        {
            var param = new Dictionary<string, string>();
            param.Add("Email", email);
            param.Add("Password", password);
            param.Add("AppId", appId);

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

        private string CallPost(string route, Dictionary<string, string> dict, string token = null)
        {
            HttpClient client = new HttpClient();

            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }

            client.BaseAddress = new Uri(apiUrl);
            var content = new FormUrlEncodedContent(dict);
            var response = client.PostAsync($"{apiUrl}{route}", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return responseString;
        }

        private string CallGet(string route, Dictionary<string, string> dict, string token = null)
        {
            var builder = new UriBuilder($"{apiUrl}{route}");
            builder.Port = -1;
            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(builder.Query);
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
