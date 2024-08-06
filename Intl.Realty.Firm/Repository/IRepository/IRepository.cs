using Intl.Realty.Firm.Models.Models;
using System.Linq.Expressions;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
                    string? includeProperties = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
                    bool tracked = false,
                    int pageSize = 0,
                    int pageNumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
        Task SaveChangesAsync();
        Task<IQueryable<T>> AsQueryableAsync(string? includeProperties = null);
        IQueryable<T> AsQueryable(string? includeProperties = null);
        Task<int> CountAsync();
        int Count();
    }
}
