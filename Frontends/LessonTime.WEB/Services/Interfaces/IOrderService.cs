using LessonTime.WEB.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LessonTime.WEB.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);  // senkron
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);//asekron

        Task<List<OrderViewModel>> GetOrder();
    }
}
