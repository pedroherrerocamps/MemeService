using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Services.Setting
{
    public static class SettingMapping
    {
        public static SettingDto Map(this SettingModel settingModel)
        {
            if (settingModel == null) return null;
            return new SettingDto()
            {
                Id = settingModel.Id,
                IsEnabled = settingModel.IsEnabled,
                CreationDate = settingModel.CreationDate,
                UpdateDate = settingModel.UpdateDate,
                Name = settingModel.Name,

                Value = settingModel.Value
            };
        }

        public static SettingModel Map(this SettingDto settingDto)
        {
            if (settingDto == null) return null;
            return new SettingModel()
            {
                Id = string.IsNullOrEmpty(settingDto.Id) ? ObjectId.GenerateNewId().ToString() : ObjectId.Parse(settingDto.Id) == ObjectId.Empty ? ObjectId.GenerateNewId().ToString() : settingDto.Id,
                IsEnabled = settingDto.IsEnabled,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = settingDto.Name,

                Value = settingDto.Value
            };
        }

        public static List<SettingModel> Map(this List<SettingDto> settings)
        {
            List<SettingModel> settingModelList = new List<SettingModel>();
            if (settings == null || settings.Count == 0) return new List<SettingModel>();
            foreach (SettingDto setting in settings)
            {
                settingModelList.Add(setting.Map());
            }
            return settingModelList;
        }

        public static List<SettingDto> Map(this List<SettingModel> settings)
        {
            List<SettingDto> settingDtoList = new List<SettingDto>();
            if (settings == null || settings.Count == 0) return new List<SettingDto>();
            foreach (SettingModel setting in settings)
            {
                settingDtoList.Add(setting.Map());
            }
            return settingDtoList;
        }
    }
}
