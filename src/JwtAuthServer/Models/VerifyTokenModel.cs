using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthServer.Models
{
    public class VerifyTokenModel
    {
        public string Token { get; set; }

    }
}
