﻿using LessonTime.Services.Order.Application.Dtos;
using LessonTime.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTime.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
    }
}
