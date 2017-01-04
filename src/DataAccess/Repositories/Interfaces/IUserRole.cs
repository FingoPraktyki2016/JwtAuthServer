using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    internal interface IUserRole : IGenericRepository<UserAppRole>, IRepository
    {
        int GetRoleId(int user);
    }
}
