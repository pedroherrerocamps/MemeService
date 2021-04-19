using MemeService.Services.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeService.Services.Setting
{
    public class SettingRepository: BaseRepository<SettingModel>, ISettingRepository
    {
        public SettingRepository(IMongoClient mongoClient, ILogger logger)
        : base(mongoClient, "setting", logger) { }

    public async Task<List<SettingDto>> GetItems()
    {
        List<SettingModel> items = await Get();
        return items.Map();
    }

    public async Task<SettingDto> GetItem(string id)
    {
        SettingModel item = await Get(id);

        return item.Map();
    }

    public async Task<SettingDto> GetItemByCondition(Expression<Func<SettingModel, bool>> expression)
    {
        SettingModel item = await GetByCondition(expression);
        return item.Map();
    }

        public async Task<SettingDto> CreateItem(SettingDto item)
    {
        SettingModel newItem = await Create(item.Map());

        return newItem.Map();
    }

    public async Task<SettingDto> UpdateItem(string id, SettingDto item)
    {
        SettingModel updatedItem = await Update(id, item.Map());
        return updatedItem.Map();
    }

    public async Task<SettingDto> RemoveItem(SettingDto item)
    {
        SettingModel deletedItem = await Remove(item.Map());
        return deletedItem.Map();
    }

    public async Task<string> RemoveItem(string id)
    {
        return await Remove(id);
    }
}
}
