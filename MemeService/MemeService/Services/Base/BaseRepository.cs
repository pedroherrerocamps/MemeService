using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
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
        private readonly ILogger _logger;
        private readonly IMongoCollection<T> _collection;


        protected virtual IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collectionName);

        public BaseRepository(IMongoClient mongoClient, string collectionName, ILogger logger)
        {
            _mongoClient = mongoClient;
            _collectionName = collectionName;
            _logger = logger;
            var database = _mongoClient.GetDatabase(DATABASE);
            _collection = database.GetCollection<T>(_collectionName);
        }


        public async Task<List<T>> Get()
        {
            List<T> list = await _collection.Find(item => true).ToListAsync();
            if (list != null && list.Count > 0) return list;
            _logger.Error("Empty GET List response");
            return null;
        }

        public async Task<T> Get(string id)
        {
            T item = null;
            try
            {
                item = await _collection.Find<T>(item => item.Id.Equals(id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("Get error: " + ex.Message);
            }
            return item;
        }

        public async Task<List<T>> GetListByCondition(Expression<Func<T, bool>> expression)
        {
            List<T> items = null;
            try
            {
                items = await _collection.Find<T>(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("GetListByCondition error: " + ex.Message);
            }
            return items;
        }
        public async Task<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            T item = null;
            try
            {
                item = await _collection.Find<T>(expression).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("GetByCondition error: " + ex.Message);
            }
            return item;

        }

        public async Task<T> Create(T item)
        {
            try { 
                _collection.InsertOne(item);
            }catch(Exception ex)
            {
                _logger.Error("Create error: " + ex.Message);
            }
            return item;
        }

        public async Task<T> Update(string id, T newItem)
        {
            T item = await Get(id);
            newItem._Id = item._Id;
            try
            {
                _collection.ReplaceOne(item => item.Id.Equals(id), newItem);
            } catch(Exception ex)
            {
                _logger.Error("Update error: " + ex.Message);
            }
            return newItem;
        }

        public async Task<T> Remove(T deleteItem)
        {
            try
            {
                _collection.DeleteOne(item => item.Id == deleteItem.Id);
            }
            catch (Exception ex)
            {
                _logger.Error("Delete error: " + ex.Message);
            }
            return deleteItem;
        }

        public async Task<string> Remove(string id)
        {
            _collection.DeleteOne(item => item.Id.Equals(id));
            return id;
        }

    }
}
