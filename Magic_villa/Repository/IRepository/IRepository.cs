using System.Linq.Expressions;

namespace Magic_villa.Repository.IRepository
{
    public interface Repository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);

        Task<T?> Get(Expression<Func<T, bool>> filter, bool track = true);

        Task Create(T entity);

        void Remove(T entity);
    }
}
