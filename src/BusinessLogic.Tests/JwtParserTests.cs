using LegnicaIT.BusinessLogic.Providers.Interface;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using LegnicaIT.BusinessLogic.Models.Token;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests
{
    public class JwtParserTests
    {
        [Fact]
        public void AcquireToken_ForValidInput_ReturnsCorrectToken()
        {
            var parser = new JwtParser();

            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", 1);

            Assert.NotNull(tokenModel.Token);
        }

        [Fact]
        public void AcquireToken_ForInvalidInput_ReturnsNull()
        {
            var parser = new JwtParser();

            AcquireTokenModel tokenModel = parser.AcquireToken(null, 0);

            Assert.Null(tokenModel);
        }

        [Fact]
        public void AcquireToken_VerifyForTokenParametersData()
        {
            var parser = new JwtParser();
            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", 1);

            var handler = new JwtSecurityTokenHandler();
            var param = parser.GetParameters();
            JwtSecurityToken readToken = handler.ReadJwtToken(tokenModel.Token);

            var iss = parser.GetClaim(readToken, "iss");
            var email = parser.GetClaim(readToken, "email");
            var appId = parser.GetClaim(readToken, "appId");

            Assert.Equal(param.ValidIssuer, iss);
            Assert.Equal("rafal.gradziel@fingo.pl", email);
            Assert.Equal("1", appId);
        }

        /// <summary>
        /// Acquire EmailConfirmationToken
        /// </summary>

        [Fact]
        public void AcquireEmailConfirmationToken_ForValidInput_ReturnsCorrectToken()
        {
            var parser = new JwtParser();

            AcquireTokenModel tokenModel = parser.AcquireEmailConfirmationToken("rafal.gradziel@fingo.pl", 1);

            Assert.NotNull(tokenModel.Token);
        }

        [Fact]
        public void AcquireEmailConfirmationToken_ForInvalidInput_ReturnsNull()
        {
            var parser = new JwtParser();

            AcquireTokenModel tokenModel = parser.AcquireEmailConfirmationToken(null, 0);

            Assert.Null(tokenModel);
        }

        [Fact]
        public void AcquireEmailConfirmationToken_VerifyForTokenParametersData()
        {
            var parser = new JwtParser();
            AcquireTokenModel tokenModel = parser.AcquireEmailConfirmationToken("rafal.gradziel@fingo.pl", 1);

            var handler = new JwtSecurityTokenHandler();
            var param = parser.GetParameters();
            JwtSecurityToken readToken = handler.ReadJwtToken(tokenModel.Token);

            var iss = parser.GetClaim(readToken, "iss");
            var email = parser.GetClaim(readToken, "email");
            var userId = parser.GetClaim(readToken, "userId");

            Assert.Equal(param.ValidIssuer, iss);
            Assert.Equal("rafal.gradziel@fingo.pl", email);
            Assert.Equal("1", userId);
        }

        [Fact]
        public void Verify_ForExpiredToken_ReturnsError()
        {
            var parser = new JwtParser();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InJhZmFsLmdyYWR6aWVAZmluZ28ucGwiLCJpc3MiOiJMZWduaWNhSVQiLCJBcHBJZCI6IjEiLCJuYmYiOjE0ODE3MTc0MTcsImV4cCI6MTQ4MTcyMTAxNywiaWF0IjoxNDgxNzE3NDE3fQ.ZPzu-eaoaY7CxyQmJwvfk18vd9sO5guOwbfjsKK1Qcg";

            VerifyResultModel result = parser.Verify(token);

            Assert.Equal(false, result.IsValid);
            Assert.Equal(null, result.ExpiryDate);
        }

        [Fact]
        public void Verify_ForFreshToken_ReturnsOk()
        {
            var parser = new JwtParser();
            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", 1);

            VerifyResultModel result = parser.Verify(tokenModel.Token);

            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.ExpiryDate);
        }

        [Fact]
        public void Verify_ForFreshToken_ReturnsCorrectExpiryDate()
        {
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var dateFormat = "yyyy-MM-dd HH:mm";
            var dateNow = DateTime.UtcNow;
            // we are using mocked IDateTimeProvider to make sure we are refering to same "now"
            mockedDateTimeProvider.Setup(p => p.GetNow()).Returns(dateNow);
            var parser = new JwtParser(mockedDateTimeProvider.Object);
            var dateNowFutureString = dateNow.AddDays(parser.GetExpiredDays()).ToString(dateFormat);

            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", 1);
            VerifyResultModel result = parser.Verify(tokenModel.Token);
            string expiryDateString = null;

            if (result.ExpiryDate != null)
            {
                expiryDateString = result.ExpiryDate.Value.ToString(dateFormat);
            }

            Assert.Equal(dateNowFutureString, expiryDateString);
        }

        [Fact]
        public void Verify_ForTokenParametersData()
        {
            var parser = new JwtParser();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IjEyM0B0ZXN0LnBsIiwiaXNzIjoiTGVnbmljYUlUIiwiYXBwSWQiOiIxIiwicm9sZSI6IlVzZXIiLCJuYmYiOjE0ODQxNDAwMjgsImV4cCI6MTQ4NDE0MDA4OCwiaWF0IjoxNDg0MTQwMDI4fQ.fZXSj3jZIQ8u2aoAzv6fDW0_c7BBb5oVr2oVDytnTek";

            VerifyResultModel result = parser.Verify(token, true);

            var email = result.Email;
            var appId = result.AppId;

            Assert.Equal("123@test.pl", email);
            Assert.Equal(1, appId);
        }
    }
}
