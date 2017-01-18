using LegnicaIT.BusinessLogic.Properties;
using LegnicaIT.BusinessLogic.Providers;
using LegnicaIT.BusinessLogic.Providers.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using LegnicaIT.BusinessLogic.Enums;
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

        public string GetClaim(JwtSecurityToken token, string claimType)
        {
            return token.Claims.Where(c => c.Type == claimType).Select(c => c.Value).SingleOrDefault();
        }

        public TokenValidationParameters GetParameters(bool skipLifetimeValidation = false)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = !skipLifetimeValidation,
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
            return Verify(token, false);
        }

        internal VerifyResultModel Verify(string token, bool skipLifetimeValidation)
        {
            var handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = GetParameters(skipLifetimeValidation);

            var result = new VerifyResultModel()
            {
                IsValid = false
            };

            try
            {
                SecurityToken validatedToken;
                handler.ValidateToken(token, parameters, out validatedToken);
                var jwt = handler.ReadToken(token) as JwtSecurityToken;

                result.ExpiryDate = jwt.ValidTo;
                result.IsValid = true;
                result.Email = GetClaim(jwt, "email");
                result.AppId = Convert.ToInt32(GetClaim(jwt, "appId"));
                result.Role = GetClaim(jwt, "role");
            }
            catch (Exception)
            {
                // FIXME: why empty? Return something
            }

            return result;
        }

        public AcquireTokenModel AcquireToken(string formEmail, int formAppId, UserRole userRole)
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
                    new Claim("appId", formAppId.ToString()),
                    new Claim(ClaimTypes.Role, userRole.ToString())
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
