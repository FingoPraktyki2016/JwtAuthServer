using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LegnicaIT.BusinessLogic
{
    public class JwtParser
    {
        // Move away from GIT repo!!!!
        private static string IssuerName = "LegnicaIT";

        private static string SecretKey = "LegnicaIT-Fingo-JWT-KEY";

        private static readonly SymmetricSecurityKey encodedSecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));

        public bool Verify(string token)
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

            bool result = false;

            try
            {
                SecurityToken validatedToken;
                handler.ValidateToken(token, parameters, out validatedToken);
                result = true;
            }
            catch (ArgumentNullException e)
            {
            }
            catch (ArgumentException e)
            {
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public string AcquireToken(string formEmail, string formPassword, int formAppId)
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
                // TODO: hardcoded. Move to ex. appsettings
                Expires = DateTime.Now.AddDays(14),
            };

            var plainToken = handler.CreateToken(tokenDescriptor);

            // Encoded token
            return handler.WriteToken(plainToken);
        }
    }
}
