namespace JwtAuthServer.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int AppId { get; set; }
    }
}
