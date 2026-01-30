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

        public Task<T?> Get(Expression<Func<T, bool>> filter, bool track = true, string? includeProperty = null)
        {
            IQueryable<T> query = _dbSet;
            if (!track)
            {
                query = query.AsNoTracking();
            }
            if (includeProperty != null)
            {
                foreach (var item in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefaultAsync(filter);
        }
        //ex includeProperty : Villa , VillaSpecial
        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperty=null,
            int pageNumber=1,int pageSize=0)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query= query.Where(filter);
            }
            if(pageNumber > 0)
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            if (includeProperty != null)
            {
                foreach(var item in includeProperty.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(item);
                }
            }
            return query.ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
