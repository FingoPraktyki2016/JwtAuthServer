using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class UserAppRepository : GenericRepository<UserApps>, IUserAppRepository
    {
        public UserAppRepository(IJwtDbContext _context) : base(_context)
        {
        }
    }
}