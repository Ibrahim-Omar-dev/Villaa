using System.Linq.Expressions;

namespace Magic_villa.Repository.IRepository
{
    public interface Repository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null,string? includeProperty=null
            ,int pageNumber = 0,int pageSize = 3);


        Task<T?> Get(Expression<Func<T, bool>> filter, bool track = true, string? includeProperty = null);

        Task Create(T entity);

        void Remove(T entity);
    }
}
