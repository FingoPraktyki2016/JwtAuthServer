using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class AppUserModel
    {
        public string Email { get; set; }
        public int AppId { get; set; }

        public void FillFromClaims(IEnumerable<Claim> claims)
        {
            Email = GetClaimValue<string>(claims, ClaimTypes.Email);
            AppId = GetClaimValue<int>(claims, "AppId");
        }

        private T GetClaimValue<T>(IEnumerable<Claim> claims, string name)
        {
            var claim = claims.FirstOrDefault(c => c.Type == name);
            if (claim != null)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(claim.Value);
                }
            }
            return default(T);
        }
    }
}