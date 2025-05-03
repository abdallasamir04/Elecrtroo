using Electro_ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Electro_ECommerce.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TechXpressDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TechXpressDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}