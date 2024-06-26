﻿using LessonTime.Services.Basket.Dtos;
using LessonTime.Shared.Dtos;
using System.Threading.Tasks;

namespace LessonTime.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<Response<bool>> Delete(string userId);
    }
}
