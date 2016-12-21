using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add();

        User GetLast();

        User Get(string email, string password);

        bool IsSet(string email, string password);
    }
}