﻿using Intl.Realty.Firm.Models.Models;
using System.Linq.Expressions;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<IEnumerable<T>> GetAllByIdsAsync(IEnumerable<int> ids, string? includeProperties = null, bool tracked = false);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
    }
}
