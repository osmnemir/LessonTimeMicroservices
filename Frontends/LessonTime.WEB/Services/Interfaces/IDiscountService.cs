using LessonTime.WEB.Models.Discount;
using System.Threading.Tasks;

namespace LessonTime.WEB.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);

    }
}
