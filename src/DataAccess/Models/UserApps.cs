namespace LegnicaIT.DataAccess.Models
{
    public class UserApps : BaseEntity
    {
        public User User { set; get; }
        public App App { set; get; }
    }
}