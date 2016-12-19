using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;


namespace LegnicaIT.JwtAuthServer.Helpers

{
    public class JwtAuthorizeHelper
    {

        private static string IssuerName = "LegnicaIT";

        private static string SecretKey = "LegnicaIT-Fingo-JWT-KEY";

        private static readonly SymmetricSecurityKey encodedSecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));

       
        public void Configure(IApplicationBuilder app)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = IssuerName,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = encodedSecretKey,
            };
       
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
                AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,
            });
        }

    }
}
