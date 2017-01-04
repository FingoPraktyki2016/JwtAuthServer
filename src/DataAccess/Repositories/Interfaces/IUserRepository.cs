using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository
    {
        User Get(string email, string password);

        bool IsSet(string email, string password);
    }
}