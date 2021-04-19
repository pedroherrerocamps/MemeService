using Serilog;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MemeService.Services.Setting
{
    [EnableCors("AllowCorsOriginFromEveryone")]
    [Route("api/setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingRepository _settingService;
        private readonly ILogger _logger;

        public SettingController(ISettingRepository settingService, ILogger logger)
        {
            _settingService = settingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<SettingDto>> Get()
        {
            return await _settingService.GetItems();
        }

        [HttpGet, Route("{id}")]
        public async Task<SettingDto> Get(string id)
        {
            return await _settingService.GetItem(id);
        }

        [HttpGet, Route("{name}/name")]
        public async Task<SettingDto> GetByName(string name)
        {
            return await _settingService.GetItemByCondition((item => item.Name.Equals(name)));
        }


        [HttpPost]
        public async Task<SettingDto> Insert([FromBody] SettingDto setting)
        {
            return await _settingService.CreateItem(setting);
        }

        [HttpPut]
        public async Task<SettingDto> Update([FromBody] SettingDto setting)
        {
            return await _settingService.UpdateItem(setting.Id, setting);
        }

        [HttpDelete]
        public async Task<SettingDto> DeleteItem([FromBody] SettingDto setting)
        {
            return await _settingService.RemoveItem(setting);
        }

        [HttpDelete, Route("{id}")]
        public async Task<string> DeleteItem(string id)
        {
            return await _settingService.RemoveItem(id);
        }
    }
}
