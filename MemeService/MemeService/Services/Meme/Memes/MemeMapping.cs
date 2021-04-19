using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Meme.Memes
{
    public static class MemeMapping
    {
        public static MemeDto Map(this MemeModel memeModel)
        {
            if (memeModel == null) return null;
            return new MemeDto()
            {
                Id = memeModel.Id,
                IsEnabled = memeModel.IsEnabled,
                CreationDate = memeModel.CreationDate,
                UpdateDate = memeModel.UpdateDate,
                Name = memeModel.Name,

                Description = memeModel.Description,
                Width = memeModel.Width,
                Height = memeModel.Height,

                Original = memeModel.Original,
                Thumbnail = memeModel.Thumbnail,


                ShortUrl = memeModel.ShortUrl,
                AccessCount = memeModel.AccessCount,
            };
        }

        public static MemeModel Map(this MemeDto memeDto)
        {
            if (memeDto == null) return null;
            return new MemeModel()
            {
                Id = string.IsNullOrEmpty(memeDto.Id) ? ObjectId.GenerateNewId().ToString() : ObjectId.Parse(memeDto.Id) == ObjectId.Empty ? ObjectId.GenerateNewId().ToString() : memeDto.Id,
                IsEnabled = memeDto.IsEnabled,
                CreationDate = memeDto.CreationDate,
                UpdateDate = DateTime.Now,
                Name = memeDto.Name,

                Description = memeDto.Description,
                Width = memeDto.Width,
                Height = memeDto.Height,

                Original = memeDto.Original,
                Thumbnail = memeDto.Thumbnail,


                ShortUrl = memeDto.ShortUrl,
                AccessCount = memeDto.AccessCount,
            };
        }

        public static List<MemeModel> Map(this List<MemeDto> memes)
        {
            List<MemeModel> memeModelList = new List<MemeModel>();
            if (memes == null || memes.Count == 0) return new List<MemeModel>();
            foreach (MemeDto meme in memes)
            {
                memeModelList.Add(meme.Map());
            }
            return memeModelList;
        }

        public static List<MemeDto> Map(this List<MemeModel> memes)
        {
            List<MemeDto> memeDtoList = new List<MemeDto>();
            if (memes == null || memes.Count == 0) return new List<MemeDto>();
            foreach (MemeModel meme in memes)
            {
                memeDtoList.Add(meme.Map());
            }
            return memeDtoList;
        }
    }
}
