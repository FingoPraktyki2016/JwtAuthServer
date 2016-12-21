using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class AppRepository : GenericRepository<App>, IAppRepository
    {
        public AppRepository(IJwtDbContext _context) : base(_context)
        {
        }
    }
}
