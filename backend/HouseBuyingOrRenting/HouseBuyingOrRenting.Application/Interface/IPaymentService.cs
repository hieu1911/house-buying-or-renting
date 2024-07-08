using HouseBuyingOrRenting.Application.Dto;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IPaymentService : IBaseService<Payment, Payment, PaymentCreateDto, Payment>
    {
    }
}
