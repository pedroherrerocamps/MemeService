using MemeService.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Meme.Memes
{
    public class MemeDto: BaseDto
    {
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Original { get; set; }
        public string Thumbnail { get; set; }
        public string ShortUrl { get; set; }
        public long AccessCount { get; set; }
    }

    public class MemeSearch
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
