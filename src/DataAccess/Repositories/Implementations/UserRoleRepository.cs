using System.Linq;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class UserRoleRepository : GenericRepository<UserAppRole>, IUserRole
    {
        public UserRoleRepository(IJwtDbContext _context) : base(_context)
        {
        }

        public int GetRoleId(int user)
        {
            //return dbSet.FirstOrDefault(e => e.User == user);
            return 5;
        }
    }
}
