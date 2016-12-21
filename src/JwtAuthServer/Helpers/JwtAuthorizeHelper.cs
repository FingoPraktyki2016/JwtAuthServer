using LegnicaIT.BusinessLogic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public class JwtAuthorizeHelper
    {
        public void Configure(IApplicationBuilder app)
        {
            TokenValidationParameters tokenValidationParameters = new JwtParser().GetParameters();

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
