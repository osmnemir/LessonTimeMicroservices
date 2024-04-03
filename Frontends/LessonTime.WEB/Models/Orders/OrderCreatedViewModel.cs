namespace LessonTime.WEB.Models.Orders
{
    public class OrderCreatedViewModel
    {
        public int OrderId { get; set; }
        public  bool IsSuccessful { get; set; }
        public  string Error { get; set; }
    }
}
