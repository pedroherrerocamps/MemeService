using MemeService.Services.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeService.Services.Meme.Memes
{
    public class MemeRepository: BaseRepository<MemeModel>, IMemeRepository
    {
        public MemeRepository(IMongoClient mongoClient)
        : base(mongoClient, "meme") { }

        public async Task<List<MemeDto>> GetItems()
        {
            List<MemeModel> items = await Get();
            return items.Map();
        }

        public async Task<List<MemeDto>> GetItemsByCondition(MemeDto meme)
        {
            Expression<Func<MemeModel, bool>> expression;
            if (!string.IsNullOrEmpty(meme.Name))
            {
                if (!string.IsNullOrEmpty(meme.Description))
                {
                    expression = item => item.IsEnabled && item.Name.Equals(meme.Name) && item.Description.Contains(meme.Description);
                    List<MemeModel> fullItems = await GetByCondition(expression);
                    return fullItems.Map();
                }
                expression = item => item.IsEnabled && item.Name.Equals(meme.Name);

                List<MemeModel> items = await GetByCondition(expression);
                return items.Map();
            }
            if (!string.IsNullOrEmpty(meme.Description))
            {
                expression = item => item.IsEnabled && item.Description.Contains(meme.Description);

                List<MemeModel> items = await GetByCondition(expression);
                return items.Map();
            }
            return null;

        }

        public async Task<MemeDto> GetItem(ObjectId id)
        {
            MemeModel item = await Get(id);

            return item.Map();
        }

        public async Task<MemeDto> CreateItem(MemeDto item)
        {
            MemeModel newItem = await Create(item.Map());

            return newItem.Map();
        }

        public async Task<MemeDto> UpdateItem(ObjectId id, MemeDto item)
        {
            MemeModel updatedItem = await Update(id, item.Map());
            return updatedItem.Map();
        }

        public async Task<MemeDto> RemoveItem(MemeDto item)
        {
            MemeModel deletedItem = await Remove(item.Map());
            return deletedItem.Map();
        }

        public async Task<string> RemoveItem(string id)
        {
            return await Remove(id);
        }
    }
}
