using LegnicaIT.BusinessLogic.Repositories.Interfaces;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.BusinessLogic.Repositories
{
    public class AppRepository : GenericRepository<App>, IAppRepository
    {
        public AppRepository(IJwtDbContext _context) : base(_context)
        {
        }
    }
}
