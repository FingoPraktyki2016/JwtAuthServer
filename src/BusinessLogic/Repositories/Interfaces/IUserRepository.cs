using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.BusinessLogic.Repositories
{
    public interface IUserRepository
    {
        void AddUser();

        User GetLastUser();
    }
}