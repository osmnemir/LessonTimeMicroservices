using LessonTime.WEB.Models.FakePayments;
using System.Threading.Tasks;

namespace LessonTime.WEB.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool>ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
