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
            if (userModel == null) return null;
            return new UserDto()
            {
                Id = userModel.Id,
                IsEnabled = userModel.IsEnabled,
                CreationDate = userModel.CreationDate,
                UpdateDate = userModel.UpdateDate,
                Name = userModel.Name,

                AccessCount = userModel.AccessCount,
                Password = userModel.Password,
                Premium = userModel.Premium
            };
        }

        public static UserModel Map(this UserDto userDto)
        {
            if (userDto == null ) return null;
            return new UserModel()
            {
                Id = string.IsNullOrEmpty(userDto.Id) ? ObjectId.GenerateNewId().ToString() : ObjectId.Parse(userDto.Id) == ObjectId.Empty ? ObjectId.GenerateNewId().ToString() : userDto.Id,
                IsEnabled = userDto.IsEnabled,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = userDto.Name,

                AccessCount = userDto.AccessCount,
                Password = userDto.Password,
                Premium = userDto.Premium
            };
        }

        public static List<UserModel> Map(this List<UserDto> users)
        {
            List<UserModel> userModelList = new List<UserModel>();
            if (users == null || users.Count == 0) return new List<UserModel>();
            foreach (UserDto user in users)
            {
                userModelList.Add(user.Map());
            }
            return userModelList;
        }

        public static List<UserDto> Map(this List<UserModel> users)
        {
            List<UserDto> userDtoList = new List<UserDto>();
            if (users == null || users.Count == 0) return new List<UserDto>();
            foreach (UserModel user in users)
            {
                userDtoList.Add(user.Map());
            }
            return userDtoList;
        }
    }
}
