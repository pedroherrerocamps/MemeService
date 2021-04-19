using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeService.Services.Setting
{
    public interface ISettingRepository
    {
        Task<List<SettingDto>> GetItems();
        Task<SettingDto> GetItem(string id);
        Task<SettingDto> CreateItem(SettingDto item);
        Task<SettingDto> UpdateItem(string id, SettingDto item);
        Task<SettingDto> GetItemByCondition(Expression<Func<SettingModel, bool>> expression);
        Task<SettingDto> RemoveItem(SettingDto item);
        Task<string> RemoveItem(string id);
    }
}
