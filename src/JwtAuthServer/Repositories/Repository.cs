using LegnicaIT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JwtAuthServer
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);

        IQueryable<T> Table();
    }

    public interface IJwtDbContext
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }

    public interface IUserRepository
    {
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> dbSet;
        protected IJwtDbContext context;
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
}