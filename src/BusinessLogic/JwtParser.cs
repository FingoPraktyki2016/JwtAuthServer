using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.BusinessLogic.Properties;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LegnicaIT.BusinessLogic
{
    public class JwtParser
    {
        // Move away from GIT repo!!!!
        private static readonly string IssuerName = tokensettings.IssuerName;

        private static readonly string SecretKey = tokensettings.SecretKey;

        private readonly int ExpiredDays = Convert.ToInt32(tokensettings.ExpiredDays);

        private static readonly SymmetricSecurityKey encodedSecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));

        public JwtParser()
        {
        }

        public VerifyResultModel Verify(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = IssuerName,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = encodedSecretKey,
            };

            var result = new VerifyResultModel()
            {
                IsValid = false
            };

            try
            {
                SecurityToken validatedToken;
                handler.ValidateToken(token, parameters, out validatedToken);
                var jwt = handler.ReadToken(token);
                result.IsValid = true;
                result.ExpiryDate = jwt.ValidTo;
            }
            catch (Exception e)
            {
                //TODO Logger
            }

            return result;
        }

        public AcquireTokenModel AcquireToken(string formEmail, string formPassword, int formAppId)
        {
            var handler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(encodedSecretKey, SecurityAlgorithms.HmacSha256Signature);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, formEmail),
                new Claim("iss", IssuerName),
                new Claim("AppId", formAppId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(ExpiredDays),
            };

            var plainToken = handler.CreateToken(tokenDescriptor);

            // Encoded token
            var result = new AcquireTokenModel()
            {
                Token = handler.WriteToken(plainToken)
            };

            return result;
        }
    }
}
