using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class UserAppModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int AppId { get; set; }

        [Required]
        public string Role { get; set; }

        public void FillFromClaims(IEnumerable<Claim> claims)
        {
            Email = GetClaimValue<string>(claims, ClaimTypes.Email);
            AppId = GetClaimValue<int>(claims, "AppId");
            Role = GetClaimValue<string>(claims, ClaimTypes.Role);
        }

        private T GetClaimValue<T>(IEnumerable<Claim> claims, string name)
        {
            var claim = claims.FirstOrDefault(c => c.Type == name);
            if (claim != null)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T) converter.ConvertFromString(claim.Value);
                }
            }

            return default(T);
        }
    }
}
