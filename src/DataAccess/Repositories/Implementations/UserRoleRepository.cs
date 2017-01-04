using System.Collections.Generic;
using System.Linq;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class UserRoleRepository : GenericRepository<UserAppRole>, IUserRoleRepository
    {
        public UserRoleRepository(IJwtDbContext _context) : base(_context)
        {
        }

        public override IEnumerable<UserAppRole> GetAll()
        {
            return this.dbSet.Include(m => m.User).ThenInclude(m => m.User).Include(m => m.App).Include(m => m.Role);
        }

        public int GetRoleId(int user_id)
        {
            return dbSet.FirstOrDefault(e => e.User.Id == user_id).Role.Id;
        }
    }
}
