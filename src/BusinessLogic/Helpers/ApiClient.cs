using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public class ApiClient : IApiClient
    {
        private HttpClient httpClient;

        internal string apiUrl;
        internal Dictionary<string, string> callParameters;

        public ApiClient(string api)
        {
            apiUrl = api;
            callParameters = new Dictionary<string, string>();
            httpClient = new HttpClient();
        }

        public void AddHeader(string key, string value)
        {
            httpClient.DefaultRequestHeaders.Add(key, value);
        }

        public void AddParameter(string key, string value)
        {
            callParameters.Add(key, value);
        }

        public FormUrlEncodedContent GetCallContent()
        {
            return new FormUrlEncodedContent(callParameters);
        }

        public string GetCallRoute(string route)
        {
            return $"{apiUrl}{route}";
        }

        public string GetCallRouteWithParameters(string route)
        {
            var builder = new UriBuilder($"{apiUrl}{route}");
            builder.Port = -1;
            string query = QueryHelpers.AddQueryString(builder.Query, callParameters);
            builder.Query = query;
            string apiFullUrl = builder.ToString();

            return apiFullUrl;
        }

        public string MakeCallPost(string route)
        {
            httpClient.BaseAddress = new Uri(apiUrl);

            var content = GetCallContent();
            var apiRoute = GetCallRoute(route);
            var response = httpClient.PostAsync(apiRoute, content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return responseString;
        }

        public string MakeCallGet(string route)
        {
            httpClient.BaseAddress = new Uri(apiUrl);
            var response = httpClient.GetAsync(route).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return responseString;
        }
    }
}
