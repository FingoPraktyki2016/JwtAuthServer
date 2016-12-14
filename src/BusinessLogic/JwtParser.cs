using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace LegnicaIT.BusinessLogic
{
    public class JwtParser
    {
        private static string SecretKey = "LegnicaIT-Fingo-JWT-KEY";

        public JwtParser()
        {   
        }

        public bool Verify(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            TokenValidationParameters parameters = new TokenValidationParameters()
            { 
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true
            };
            bool result = false;

            try
            {
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


    }
}
