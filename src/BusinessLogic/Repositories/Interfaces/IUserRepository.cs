using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.BusinessLogic.Repositories
{
    public interface IUserRepository
    {
        void AddUser();

        User GetLastUser();

        User GetUser(string email, string password);

        bool IsUserInDatabase(string email, string password);
    }
}
