using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeService.Services.Base
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> Get();
        Task<T> Get(string id);
        Task<T> Create(T item);
        Task<T> Update(string id, T newItem);
        Task<T> Remove(T deleteItem);
        Task<string> Remove(string id);
        Task<List<T>> GetListByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetByCondition(Expression<Func<T, bool>> expression);
    }
}
