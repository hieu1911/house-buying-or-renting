using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Application.Dto;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : BaseController<Payment, Payment, PaymentCreateDto, Payment>
    {
        public PaymentController(IPaymentService paymentService) : base(paymentService)
        {
        }
    }
}
