using LegnicaIT.BusinessLogic.Properties;
using LegnicaIT.BusinessLogic.Providers;
using LegnicaIT.BusinessLogic.Providers.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LegnicaIT.BusinessLogic.Models.Token;

namespace LegnicaIT.BusinessLogic
{
    public class JwtParser
    {
        private readonly SymmetricSecurityKey encodedSecretKey;
        private readonly IDateTimeProvider dateTimeProvider;

        public JwtParser(IDateTimeProvider dateTimeProvider = null)
        {
            this.dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
            encodedSecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetSecretKey()));
        }

        public string GetIssuerName()
        {
            return tokensettings.IssuerName;
        }

        public string GetSecretKey()
        {
            return tokensettings.SecretKey;
        }

        public int GetExpiredDays()
        {
            return Convert.ToInt32(tokensettings.ExpiredDays);
        }

        public TokenValidationParameters GetParameters()
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = GetIssuerName(),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = encodedSecretKey,
            };

            return parameters;
        }

        public VerifyResultModel Verify(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = GetParameters();

            var result = new VerifyResultModel()
            {
                IsValid = false
            };

            try
            {
                SecurityToken validatedToken;
                handler.ValidateToken(token, parameters, out validatedToken);
                var jwt = handler.ReadToken(token);
                result.ExpiryDate = jwt.ValidTo;
                result.IsValid = true;
            }
            catch (Exception)
            {
            }

            return result;
        }

        public AcquireTokenModel AcquireToken(string formEmail, int formAppId/*, string userRole*/)
        {
            var handler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(encodedSecretKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity;

            try
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, formEmail),
                    new Claim("iss", GetIssuerName()),
                    new Claim("AppId", formAppId.ToString()),
                    new Claim(ClaimTypes.Role, "gsgs")
                });
            }
            catch (Exception)
            {
                return null;
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                SigningCredentials = credentials,
                Expires = dateTimeProvider.GetNow().AddDays(GetExpiredDays()),
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
