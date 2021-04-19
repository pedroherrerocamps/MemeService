using MemeService.Services.Setting;
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
        private readonly ISettingRepository _settingService;
        private readonly ILogger _logger;

        public MemeController(IMemeRepository memeService, ISettingRepository settingService, ILogger logger)
        {
            _memeService = memeService;
            _settingService = settingService;
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
            return await _memeService.GetItem(id);
        }

        [HttpGet, Route("{shortUrl}/shortUrl")]
        public async Task<MemeDto> GetByShortUrl(string shortUrl)
        {
            MemeDto meme = await _memeService.GetItemByCondition(item => item.ShortUrl.Equals(shortUrl));
            if (meme != null)
            {
                meme.AccessCount++;
                return await _memeService.UpdateItem(meme.Id, meme);
            }

            return null;
        }


        [HttpPost, Route("byCondition")]
        public async Task<List<MemeDto>> GetByCondition([FromBody] MemeSearch meme)
        {
            return await _memeService.GetItemsByCondition(meme);
        }

        [HttpPost]
        public async Task<MemeDto> Insert([FromBody] MemeDto meme)
        {
            SettingDto filePath = await _settingService.GetItemByCondition(item => item.Name.Equals("filePath"));
            if (filePath == null) return null;
            meme.Original = filePath.Value + "\\" + meme.Original;
            meme.Thumbnail = filePath.Value + "\\" + meme.Thumbnail;
            meme.ShortUrl = Guid.NewGuid().ToString();
            meme.CreationDate = DateTime.Now;
            MemeDto auxMeme = new MemeDto();
            do
            {
                auxMeme = await _memeService.GetItemByCondition(item => item.ShortUrl.Equals(meme.ShortUrl));
            } while (auxMeme != null && auxMeme.ShortUrl == meme.ShortUrl);

            return await _memeService.CreateItem(meme);
        }

        [HttpPut]
        public async Task<MemeDto> Update([FromBody] MemeDto meme)
        {
            return await _memeService.UpdateItem(meme.Id, meme);
        }

        [HttpDelete]
        public async Task<MemeDto> DeleteItem([FromBody] MemeDto meme)
        {
            return await _memeService.RemoveItem( meme);
        }

        [HttpDelete, Route("{id}")]
        public async Task<MemeDto> DeleteItem(string id)
        {
            return await _memeService.RemoveItem(id);
        }

    }
}
