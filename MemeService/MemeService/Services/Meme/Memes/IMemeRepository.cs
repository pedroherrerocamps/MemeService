using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeService.Services.Meme.Memes
{
    public interface IMemeRepository
    {
        Task<List<MemeDto>> GetItems();
        Task<MemeDto> GetItem(string id);
        Task<MemeDto> GetItemByCondition(Expression<Func<MemeModel, bool>> expression);
        Task<List<MemeDto>> GetItemsByCondition(MemeSearch meme);
        Task<MemeDto> CreateItem(MemeDto item);
        Task<MemeDto> UpdateItem(string id, MemeDto item);
        Task<MemeDto> RemoveItem(MemeDto item);
        Task<MemeDto> RemoveItem(string id);
    }
}
