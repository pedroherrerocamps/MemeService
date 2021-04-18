using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Meme.Memes
{
    [EnableCors("AllowCorsOriginFromEveryone")]
    [Route("api/meme")]
    [ApiController]
    public class MemeController : ControllerBase
    {
        private readonly IMemeRepository _memeService;
        private readonly ILogger _logger;

        public MemeController(IMemeRepository memeService, ILogger logger)
        {
            _memeService = memeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<MemeDto>> Get()
        {
            return await _memeService.GetItems();
        }

        [HttpGet, Route("{id}")]
        public async Task<MemeDto> Get(string id)
        {
            return await _memeService.GetItem(ObjectId.Parse(id));
        }


        [HttpPost, Route("byCondition")]
        public async Task<List<MemeDto>> GetByCondition([FromBody] MemeDto meme)
        {
            return await _memeService.GetItemsByCondition(meme);
        }

        [HttpPost]
        public async Task<MemeDto> Insert([FromBody] MemeDto meme)
        {
            return await _memeService.CreateItem(meme);
        }

        [HttpPut]
        public async Task<MemeDto> Update([FromBody] MemeDto meme)
        {
            return await _memeService.UpdateItem(ObjectId.Parse(meme.Id), meme);
        }

        [HttpDelete]
        public async Task<MemeDto> DeleteItem([FromBody] MemeDto meme)
        {
            return await _memeService.RemoveItem( meme);
        }

        [HttpDelete, Route("{id}")]
        public async Task<string> DeleteItem(string id)
        {
            return await _memeService.RemoveItem(id);
        }

    }
}
