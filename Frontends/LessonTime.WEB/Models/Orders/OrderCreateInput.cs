using System.Collections.Generic;

namespace LessonTime.WEB.Models.Orders
{
    public class OrderCreateInput
    {

        public string BuyerId { get; set; }
        public List<OrderItemCreateInput> OrderItems { get; set; }
        public AddressCreateInput Address { get; set; }


        public OrderCreateInput()
        {
            OrderItems = new List<OrderItemCreateInput>();
        }
    }
}
