using LegnicaIT.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    public interface IUserAppRepository : IGenericRepository<UserApps>, IRepository
    {
    }
}