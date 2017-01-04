using System.Linq;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IJwtDbContext _context) : base(_context)
        {
        }
    }
}
