﻿using LessonTime.WEB.Models;
using Microsoft.Extensions.Options;

namespace LessonTime.WEB.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string photoUrl) 
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }
    }
}
