using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using LegnicaIT.BusinessLogic.Models.Common;
using Moq;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Helpers
{
    public class ApiHelperTests
    {
        [Fact]
        public void CallPost_CallParameters_AreCorrect()
        {
            // prepare
            var apiResponse = new ApiResponseModel()
            {
                ResponseMessage = "api-post-result",
                StatusCode = HttpStatusCode.OK
            };

            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost("route-post")).Returns(apiResponse);
            var client = new ApiHelper(mockedApiClient.Object);
            var callParams = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } };

            // action
            var result = client.CallPost("route-post", callParams, "token-12340-abcd");

            // verify
            mockedApiClient.Verify(c => c.AddParameter("key1", "value1"), Times.Once);
            mockedApiClient.Verify(c => c.AddParameter("key2", "value2"), Times.Once);
            mockedApiClient.Verify(c => c.AddHeader("Authorization", "Bearer token-12340-abcd"), Times.Once);
            Assert.Equal("api-post-result", result.ResponseMessage);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void CallGet_CallParameters_AreCorrect()
        {
            // prepare
            var apiResponse = new ApiResponseModel()
            {
                ResponseMessage = "api-get-result",
                StatusCode = HttpStatusCode.OK
            };

            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallGet(It.IsAny<string>())).Returns(apiResponse);
            var client = new ApiHelper(mockedApiClient.Object);
            var callParams = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } };

            // action
            var result = client.CallGet("route-get", callParams, "token-12340-abcd");

            // verify
            mockedApiClient.Verify(c => c.AddParameter("key1", "value1"), Times.Once);
            mockedApiClient.Verify(c => c.AddParameter("key2", "value2"), Times.Once);
            mockedApiClient.Verify(c => c.AddHeader("Authorization", "Bearer token-12340-abcd"), Times.Once);
            mockedApiClient.Verify(c => c.GetCallRouteWithParameters("route-get"), Times.Once);
            Assert.Equal("api-get-result", result.ResponseMessage);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void CallGet_MakeCallGet_CalledWithResultOfGetCallRouteWithParameters()
        {
            // prepare
            var apiResponse = new ApiResponseModel()
            {
                ResponseMessage = "api-get-result",
                StatusCode = HttpStatusCode.OK
            };

            string apiGetUrl = "http://api/route-get?key1=value1";
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallGet(apiGetUrl)).Returns(apiResponse);
            mockedApiClient.Setup(c => c.GetCallRouteWithParameters("route-get"))
                .Returns(apiGetUrl);

            var client = new ApiHelper(mockedApiClient.Object);
            var callParams = new Dictionary<string, string>() { { "key1", "value1" } };

            // action
            var result = client.CallGet("route-get", callParams, "token-12340-abcd");

            mockedApiClient.Verify(c => c.GetCallRouteWithParameters("route-get"), Times.Once);
            mockedApiClient.Verify(c => c.MakeCallGet(apiGetUrl), Times.Once);
            Assert.Equal("api-get-result", result.ResponseMessage);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Verify_CallParameters_AreCorrect()
        {
            // prepare
            var apiResponse = new ApiResponseModel()
            {
                ResponseMessage = "api-verify-result",
                StatusCode = HttpStatusCode.OK
            };

            string executedApiRoute = null;
            var executedApiParams = new Dictionary<string, string>();
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost(It.IsAny<string>()))
                .Callback<string>(route => executedApiRoute = route)
                .Returns(apiResponse);
            mockedApiClient.Setup(c => c.AddParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => executedApiParams.Add(k, v));
            var client = new ApiHelper(mockedApiClient.Object);

            // action
            var executedApiResult = client.Verify("token-12340-abcd");

            // verify
            Assert.Equal("api-verify-result", executedApiResult.ResponseMessage);
            Assert.Equal(HttpStatusCode.OK, executedApiResult.StatusCode);
            Assert.Equal("api/auth/verify", executedApiRoute);
            Assert.Equal(1, executedApiParams.Count);
            Assert.Equal("token-12340-abcd", executedApiParams["Token"]);
        }

        [Fact]
        public void AcquireToken_CallParameters_AreCorrect()
        {
            // prepare
            var apiResponse = new ApiResponseModel()
            {
                ResponseMessage = "api-acquiretoken-result",
                StatusCode = HttpStatusCode.OK
            };

            string executedApiRoute = null;
            var executedApiParams = new Dictionary<string, string>();
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost(It.IsAny<string>()))
                .Callback<string>(route => executedApiRoute = route)
                .Returns(apiResponse);
            mockedApiClient.Setup(c => c.AddParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => executedApiParams.Add(k, v));
            var client = new ApiHelper(mockedApiClient.Object);

            // action
            var executedApiResult = client.AcquireToken("email", "pass", "appId");

            // verify
            Assert.Equal("api-acquiretoken-result", executedApiResult.ResponseMessage);
            Assert.Equal(HttpStatusCode.OK, executedApiResult.StatusCode);
            Assert.Equal("api/auth/acquiretoken", executedApiRoute);
            Assert.Equal(3, executedApiParams.Count);
            Assert.Equal("email", executedApiParams["Email"]);
            Assert.Equal("pass", executedApiParams["Password"]);
            Assert.Equal("appId", executedApiParams["AppId"]);
        }

        [Fact]
        public void SwitchApp_CallParameters_AreCorrect()
        {
            // prepare
            var apiResponse = new ApiResponseModel()
            {
                ResponseMessage = "api-acquiretoken-result",
                StatusCode = HttpStatusCode.OK
            };

            string executedApiRoute = null;
            var executedApiParams = new Dictionary<string, string>();
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost(It.IsAny<string>()))
                .Callback<string>(route => executedApiRoute = route)
                .Returns(apiResponse);
            mockedApiClient.Setup(c => c.AddParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => executedApiParams.Add(k, v));
            var client = new ApiHelper(mockedApiClient.Object);

            // action
            var executedApiResult = client.SwitchApp("token", "1");

            // verify
            Assert.Equal("api-acquiretoken-result", executedApiResult.ResponseMessage);
            Assert.Equal(HttpStatusCode.OK, executedApiResult.StatusCode);
            Assert.Equal(1, executedApiParams.Count);
            Assert.Equal("1", executedApiParams["appId"]);
            Assert.Equal("api/auth/switchapp", executedApiRoute);
        }
    }
}