using MemeService.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.User
{
    public class UserDto : BaseDto
    {
        public string Password { get; set; }
        public string AccessCount { get; set; }
        public bool Premium { get; set; }
    }
}
