using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.User
{
    public static class UserMapping
    {
        public static UserDto Map(this UserModel userModel)
        {
            return new UserDto()
            {
                Id = userModel.Id,
                IsEnabled = userModel.IsEnabled,
                CreationDate = userModel.CreationDate,
                UpdateDate = userModel.UpdateDate,

                AccessCount = userModel.AccessCount,
                Password = userModel.Password,
                Premium = userDto.Premium
            };
        }

        public static UserModel Map(this UserDto userDto)
        {
            return new UserModel()
            {
                Id = string.IsNullOrEmpty(userDto.Id) ? ObjectId.GenerateNewId().ToString() : ObjectId.Parse(userDto.Id) == ObjectId.Empty ? ObjectId.GenerateNewId().ToString() : userDto.Id,
                IsEnabled = userDto.IsEnabled,
                CreationDate = userDto.CreationDate,
                UpdateDate = userDto.UpdateDate,

                AccessCount = userDto.AccessCount,
                Password = userDto.Password,
                Premium = userDto.Premium
            };
        }

        public static List<UserModel> Map(this List<UserDto> memes)
        {
            List<UserModel> userModelList = new List<UserModel>();
            foreach (UserDto meme in memes)
            {
                userModelList.Add(meme.Map());
            }
            return userModelList;
        }

        public static List<UserDto> Map(this List<UserModel> memes)
        {
            List<UserDto> userDtoList = new List<UserDto>();
            foreach (UserModel meme in memes)
            {
                userDtoList.Add(meme.Map());
            }
            return userDtoList;
        }
    }
}
