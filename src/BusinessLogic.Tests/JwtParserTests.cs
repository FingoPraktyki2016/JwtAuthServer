using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.BusinessLogic.Providers.Interface;
using Moq;
using System;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests
{
    public class JwtParserTests
    {
        [Fact]
        public void AcquireToken_ForValidInput_ReturnsCorrectToken()
        {
            var parser = new JwtParser();

            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", "test", 1);

            Assert.NotNull(tokenModel.Token);
        }

        [Fact]
        public void AcquireToken_ForInvalidInput_ReturnsNull()
        {
            var parser = new JwtParser();

            AcquireTokenModel tokenModel = parser.AcquireToken(null, null, 0);

            Assert.Null(tokenModel);
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
            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", "test", 1);

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

            AcquireTokenModel tokenModel = parser.AcquireToken("rafal.gradziel@fingo.pl", "test", 1);
            VerifyResultModel result = parser.Verify(tokenModel.Token);
            string expiryDateString = result.ExpiryDate.Value.ToString(dateFormat);

            Assert.Equal(dateNowFutureString, expiryDateString);
        }
    }
}
