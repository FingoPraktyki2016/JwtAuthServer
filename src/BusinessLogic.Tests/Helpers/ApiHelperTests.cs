using System.Collections.Generic;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using Xunit;
using Moq;

namespace LegnicaIT.BusinessLogic.Tests.Helpers
{
    public class ApiHelperTests
    {
        [Fact]
        public void CallPost_CallPostParameters_AreCorrect()
        {
            // prepare
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost("route-post")).Returns("api-post-result");
            var client = new ApiHelper("abc", mockedApiClient.Object);
            var callParams = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } };

            // action
            var result = client.CallPost("route-post", callParams, "token-12340-abcd");

            // verify
            mockedApiClient.Verify(c => c.AddParameter("key1", "value1"), Times.Once);
            mockedApiClient.Verify(c => c.AddParameter("key2", "value2"), Times.Once);
            mockedApiClient.Verify(c => c.AddHeader("Authorization", "Bearer token-12340-abcd"), Times.Once);
            Assert.Equal("api-post-result", result);
        }

        [Fact]
        public void Verify_CallParameters_AreCorrect()
        {
            // prepare
            string executedApiRoute = null;
            var executedApiParams = new Dictionary<string, string>();
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost(It.IsAny<string>()))
                .Callback<string>(route => executedApiRoute = route)
                .Returns("api-verify-result");
            mockedApiClient.Setup(c => c.AddParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => executedApiParams.Add(k, v));
            var client = new ApiHelper("abc", mockedApiClient.Object);

            // action
            string executedApiResult = client.Verify("token-12340-abcd");
            
            // verify
            Assert.Equal("api-verify-result", executedApiResult);
            Assert.Equal("api/auth/verify", executedApiRoute);
            Assert.Equal("token-12340-abcd", executedApiParams["Token"]);
        }

        [Fact]
        public void AcquireToken_CallParameters_AreCorrect()
        {
            // prepare
            string executedApiRoute = null;
            var executedApiParams = new Dictionary<string, string>();
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(c => c.MakeCallPost(It.IsAny<string>()))
                .Callback<string>(route => executedApiRoute = route)
                .Returns("api-acquiretoken-result");
            mockedApiClient.Setup(c => c.AddParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => executedApiParams.Add(k, v));
            var client = new ApiHelper("abc", mockedApiClient.Object);

            // action
            string executedApiResult = client.AcquireToken("email", "pass", "appId");

            // verify
            Assert.Equal("api-acquiretoken-result", executedApiResult);
            Assert.Equal("api/auth/acquiretoken", executedApiRoute);
            Assert.Equal("email", executedApiParams["Email"]);
            Assert.Equal("pass", executedApiParams["Password"]);
            Assert.Equal("appId", executedApiParams["AppId"]);
        }
    }
}
