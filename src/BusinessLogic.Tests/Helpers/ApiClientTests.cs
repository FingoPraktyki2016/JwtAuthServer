using LegnicaIT.BusinessLogic.Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
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
            client.Initialize();
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

        [Theory, ClassData(typeof(CallGetParametersData))]
        public void GetCallRouteWithParameters_WithParameters_ReturnsCorrectPath(
            string apiRoute, Dictionary<string, string> apiParams, string expected)
        {
            if (apiParams != null)
            {
                foreach (var p in apiParams)
                {
                    client.AddParameter(p.Key, p.Value);
                }
            }

            string actual = client.GetCallRouteWithParameters(apiRoute);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeCallGet_NotInitialized_ThrowsException()
        {
            // create client without Initalization() call
            client = new ApiClient(apiUrl);
            // client.Initialization() is skipped on purpose

            var ex = Assert.Throws<Exception>(() => client.MakeCallGet("route"));
            Assert.Equal("ApiClient needs internal Initialize() call first!", ex.Message);
        }

        [Fact]
        public void MakeCallPost_NotInitialized_ThrowsException()
        {
            // create client without Initalization() call
            client = new ApiClient(apiUrl);
            // client.Initialization() is skipped on purpose

            var ex = Assert.Throws<Exception>(() => client.MakeCallPost("route"));
            Assert.Equal("ApiClient needs internal Initialize() call first!", ex.Message);
        }
    }

    public class CallGetParametersData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"route1", null, "http://api-path/route1"},
            new object[] {"route2", new Dictionary<string, string>(), "http://api-path/route2"},
            new object[] {"route3", new Dictionary<string, string>() {{"k", "v"}}, "http://api-path/route3?k=v"},
            new object[] {"route4", new Dictionary<string, string>() {{"k", "v"}, {"a","b"}}, "http://api-path/route4?k=v&a=b"},
            new object[] {"route5", new Dictionary<string, string>() {{"k", "v /-&"}}, "http://api-path/route5?k=v%20%2F-%26"},
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
