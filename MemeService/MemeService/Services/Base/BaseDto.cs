using MongoDB.Bson;
using System;

namespace MemeService.Services.Base
{
    public class BaseDto
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
