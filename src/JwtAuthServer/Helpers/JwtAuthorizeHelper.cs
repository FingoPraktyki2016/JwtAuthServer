using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using LegnicaIT.BusinessLogic;

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public class JwtAuthorizeHelper
    {
        public void Configure(IApplicationBuilder app)
        {
            TokenValidationParameters tokenValidationParameters = new JwtParser().getParameters();

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
