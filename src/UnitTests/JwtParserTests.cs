using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Models;
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
    }
}
