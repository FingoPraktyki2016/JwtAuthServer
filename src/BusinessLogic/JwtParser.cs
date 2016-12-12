using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public Boolean Verify(String token)
        {
            var handler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            //handler.ValidateToken(token, TokenValidationParameters.DefaultAuthenticationType, out validatedToken);


            return true;
        }


    }
}
