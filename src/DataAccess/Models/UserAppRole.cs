namespace LegnicaIT.DataAccess.Models
{
    public class UserAppRole : BaseEntity
    {
        public User User { set; get; }
        public App App { set; get; }
        public Role Role { set; get; }
    }
}
