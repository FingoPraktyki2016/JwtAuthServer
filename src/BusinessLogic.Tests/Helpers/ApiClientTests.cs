using LegnicaIT.BusinessLogic.Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Helpers
{
    public class ApiClientTests : IDisposable
    {
        private string apiUrl = "api-path/";
        private ApiClient client;

        public ApiClientTests()
        {
            // re-create client before each test
            client = new ApiClient(apiUrl);
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Contructor_SetsApiUrl()
        {
            Assert.Equal("api-path/", client.apiUrl);
        }

        [Fact]
        public void Constructor_SetsCallParametersEmpty()
        {
            Assert.NotNull(client.callParameters);
            Assert.Equal(0, client.callParameters.Count);
        }

        [Fact]
        public void GetCallRoute_Returns_CorrectValue()
        {
            string route = client.GetCallRoute("route");

            Assert.Equal("api-path/route", route);
        }

        [Fact]
        public void AddParameter_WithValue_AddsValue()
        {
            client.AddParameter("key", "value");

            Assert.Equal(1, client.callParameters.Count);
            Assert.Equal(true, client.callParameters.ContainsKey("key"));
            Assert.Equal("value", client.callParameters["key"]);
        }

        [Fact]
        public void AddParameter_CalledMutipleTimes_AddsValue()
        {
            client.AddParameter("key1", "value1");
            client.AddParameter("key2", "value2");
            client.AddParameter("key3", "value3");

            Assert.Equal(3, client.callParameters.Count);
            Assert.Equal("key1, key2, key3", client.callParameters.Keys.Join());
            Assert.Equal("value1, value2, value3", client.callParameters.Values.Join());
            Assert.Equal("value1", client.callParameters["key1"]);
            Assert.Equal("value2", client.callParameters["key2"]);
            Assert.Equal("value3", client.callParameters["key3"]);
        }
    }
}
