using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.BusinessLogic.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IJwtDbContext _context) : base(_context)
        {
        }
    }
}
