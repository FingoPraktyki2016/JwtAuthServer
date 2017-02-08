using LegnicaIT.BusinessLogic.Properties;
using LegnicaIT.BusinessLogic.Providers;
using LegnicaIT.BusinessLogic.Providers.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using LegnicaIT.BusinessLogic.Models.Token;

namespace LegnicaIT.BusinessLogic
{
    public class JwtParser
    {
        private readonly SymmetricSecurityKey encodedSecretKey;
        private readonly IDateTimeProvider dateTimeProvider;

        private const string ClaimEmail = "email";
        private const string ClaimAppId = "appId";
        private const string ClaimIssuer = "iss";

        /// <summary>
        /// JWT token parser
        /// </summary>
        public JwtParser()
        {
            encodedSecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetSecretKey()));
            dateTimeProvider = new DateTimeProvider();
        }

        /// <summary>
        /// JWT token parser - for tests
        /// </summary>
        /// <param name="dateTimeProvider">Mocked DateTimeProvider</param>
        internal JwtParser(IDateTimeProvider dateTimeProvider)
        {
            encodedSecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetSecretKey()));
            this.dateTimeProvider = dateTimeProvider;
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

            // Catch invalid token
            try
            {
                SecurityToken validatedToken;
                handler.ValidateToken(token, parameters, out validatedToken);
            }
            catch (Exception)
            {
                return result;
            }

            var jwt = handler.ReadToken(token) as JwtSecurityToken;

            if (jwt != null)
            {
                result.ExpiryDate = jwt.ValidTo;
                result.Email = GetClaim(jwt, ClaimEmail);
                result.AppId = Convert.ToInt32(GetClaim(jwt, ClaimAppId));
                result.IsValid = true;
            }

            return result;
        }

        public AcquireTokenModel AcquireToken(string formEmail, int formAppId)
        {
            var handler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(encodedSecretKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity;

            try
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, formEmail),
                    new Claim(ClaimIssuer, GetIssuerName()),
                    new Claim(ClaimAppId, formAppId.ToString()),
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
