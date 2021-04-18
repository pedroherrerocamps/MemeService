using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Base
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> Get();
        Task<T> Get(ObjectId id);
        Task<T> Create(T item);
        Task<T> Update(ObjectId id, T newItem);
        Task<T> Remove(T deleteItem);
        Task<string> Remove(string id);
    }
}
