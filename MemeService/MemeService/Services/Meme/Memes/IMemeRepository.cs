using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Meme.Memes
{
    public interface IMemeRepository
    {
        Task<List<MemeDto>> GetItems();
        Task<MemeDto> GetItem(ObjectId id);
        Task<List<MemeDto>> GetItemsByCondition(MemeDto meme);
        Task<MemeDto> CreateItem(MemeDto item);
        Task<MemeDto> UpdateItem(ObjectId id, MemeDto item);
        Task<MemeDto> RemoveItem(MemeDto item);
        Task<string> RemoveItem(string id);
    }
}
