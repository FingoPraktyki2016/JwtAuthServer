using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<UserAppRole>, IRepository
    {
        int GetRoleId(int user);
    }
}
