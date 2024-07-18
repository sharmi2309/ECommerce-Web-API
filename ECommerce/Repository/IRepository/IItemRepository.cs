using System.Linq.Expressions;

namespace ECommerce.Repository.IRepository
{
    public interface IItemRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task saveAsync();


    }
}
