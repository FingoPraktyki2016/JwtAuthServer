﻿using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using LegnicaIT.BusinessLogic.Models.Common;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public class ApiClient : IApiClient
    {
        private HttpClient httpClient;

        internal string apiUrl;
        internal Dictionary<string, string> callParameters;
        internal bool isInitialized;

        public ApiClient(string api)
        {
            apiUrl = api;
            isInitialized = false;
        }

        public void Initialize()
        {
            callParameters = new Dictionary<string, string>();
            httpClient = new HttpClient();
            isInitialized = true;
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
            var builder = new UriBuilder($"{apiUrl}{route}") { Port = -1 };
            string query = QueryHelpers.AddQueryString(builder.Query, callParameters);
            builder.Query = query;
            string apiFullUrl = builder.ToString();

            return apiFullUrl;
        }

        public ApiResponseModel MakeCallPost(string route)
        {
            if (!isInitialized)
            {
                throw new Exception("ApiClient needs internal Initialize() call first!");
            }

            httpClient.BaseAddress = new Uri(apiUrl);

            var content = GetCallContent();
            var apiRoute = GetCallRoute(route);
            var response = httpClient.PostAsync(apiRoute, content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            isInitialized = false;

            return new ApiResponseModel() { ResponseMessage = responseString, StatusCode = response.StatusCode };
        }

        public ApiResponseModel MakeCallGet(string route)
        {
            if (!isInitialized)
            {
                throw new Exception("ApiClient needs internal Initialize() call first!");
            }

            httpClient.BaseAddress = new Uri(apiUrl);
            var response = httpClient.GetAsync(route).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            isInitialized = false;

            return new ApiResponseModel() { ResponseMessage = responseString, StatusCode = response.StatusCode };
        }
    }
}