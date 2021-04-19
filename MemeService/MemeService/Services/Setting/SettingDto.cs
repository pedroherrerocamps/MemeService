using MemeService.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Setting
{
    public class SettingDto : BaseDto
    {
        public string Value { get; set; }
    }
}
