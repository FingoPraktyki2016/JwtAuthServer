﻿using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using LegnicaIT.BusinessLogic.Models.Common;
using System.Collections.Generic;
using System.Net;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public class ApiHelper
    {
        private readonly IApiClient client;

        public ApiHelper(string api, IApiClient mockedClient = null)
        {
            client = mockedClient ?? new ApiClient(api);
        }

        public ApiResponseModel AcquireToken(string email, string password, string appId)
        {
            var param = new Dictionary<string, string>
            {
                {"Email", email},
                {"Password", password},
                {"AppId", appId}
            };

            return CallPost("api/auth/acquiretoken", param);
        }

        public ApiResponseModel Verify(string token)
        {
            var param = new Dictionary<string, string> { { "Token", token } };

            return CallPost("api/auth/verify", param, token);
        }

        public ApiResponseModel GetUserRoles(string token)
        {
            return CallPost("api/user/getroles", null, token);
        }

        internal ApiResponseModel CallPost(string route, Dictionary<string, string> dict, string token = null)
        {
            client.Initialize();

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

            var apiResponse = client.MakeCallPost(route);

            return apiResponse;
        }

        internal ApiResponseModel CallGet(string route, Dictionary<string, string> dict, string token = null)
        {
            client.Initialize();

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

            string apiFullUrl = client.GetCallRouteWithParameters(route);
            var apiResponse = client.MakeCallGet(apiFullUrl);

            return apiResponse;
        }
    }
}