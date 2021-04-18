using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeService.Services.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private const string DATABASE = "MemeService";
        private readonly IMongoClient _mongoClient;
        private readonly string _collectionName;
        private readonly IMongoCollection<T> _collection;

        protected virtual IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collectionName);

        public BaseRepository(IMongoClient mongoClient, string collectionName = "")
        {
            (_mongoClient, _collectionName) = (mongoClient, collectionName);

            var database = _mongoClient.GetDatabase(DATABASE);
            _collection = database.GetCollection<T>(_collectionName);
        }


        public async Task<List<T>> Get()
        {
            return await _collection.Find(item => true).ToListAsync();
        }

        public async Task<T> Get(ObjectId id)
        {
            return await _collection.Find<T>(item => ObjectId.Parse(item.Id) == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await _collection.Find<T>(expression).ToListAsync();
        }

        public async Task<T> Create(T item)
        {
            _collection.InsertOne(item);
            return item;
        }

        public async Task<T> Update(ObjectId id, T newItem)
        {
            _collection.ReplaceOne(item => ObjectId.Parse(item.Id) == id, newItem);
            return newItem;
        }

        public async Task<T> Remove(T deleteItem)
        {
            _collection.DeleteOne(item => item.Id == deleteItem.Id);
            return deleteItem;
        }

        public async Task<string> Remove(string id)
        {
            _collection.DeleteOne(item => item.Id.Equals(id));
            return id;
        }

    }
}
