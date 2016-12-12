using LegnicaIT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JwtAuthServer
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IJwtDbContext db;

        public UserRepository(IJwtDbContext _context) : base(_context)
        {
            this.db = _context;
        }

        public IQueryable<User> GetUsers()
        {
            return this.Table();
        }
    }

    public interface IUserRepository
    {
    }

    public interface IJwtDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected IJwtDbContext context;
        protected readonly DbSet<T> dbSet;

        protected GenericRepository(IJwtDbContext _context)
        {
            this.context = _context;
            this.dbSet = _context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public virtual IQueryable<T> Table()
        {
            return this.dbSet;
        }
    }

    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);

        IQueryable<T> Table();
    }
}