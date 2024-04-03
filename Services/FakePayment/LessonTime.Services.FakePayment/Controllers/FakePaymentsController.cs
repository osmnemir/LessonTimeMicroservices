using LessonTime.Services.FakePayment.Models;
using LessonTime.Shared.ControllerBases;
using LessonTime.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonTime.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto) 
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
