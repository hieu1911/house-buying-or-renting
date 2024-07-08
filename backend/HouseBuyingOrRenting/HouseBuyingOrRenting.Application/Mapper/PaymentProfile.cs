using AutoMapper;
using HouseBuyingOrRenting.Application.Dto;
using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting.Application.Mapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentCreateDto, Payment>();
        }
    }
}
