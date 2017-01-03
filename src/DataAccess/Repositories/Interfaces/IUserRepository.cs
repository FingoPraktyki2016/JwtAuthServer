using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>, IRepository
    {
        void Add();

        User GetLast();

        User Get(string email, string password);
    }
}