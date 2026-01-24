using Magic_villa.Data;
using Magic_villa.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_villa.Repository
{
    public class Repository<T> : IRepository.Repository<T> where T : class

    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task<T?> Get(Expression<Func<T, bool>> filter, bool track = true)
        {
            IQueryable<T> query = _dbSet;
            if (!track)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefaultAsync(filter);
        }

        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query= query.Where(filter);
            }
            return query.ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
