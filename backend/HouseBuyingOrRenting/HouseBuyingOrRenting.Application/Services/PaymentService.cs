using AutoMapper;
using HouseBuyingOrRenting.Application.Dto;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class PaymentService : BaseService<Payment, Payment, PaymentCreateDto, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IRealEstateRepository realEstateRepository, IMapper mapper) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _realEstateRepository = realEstateRepository;
            _mapper = mapper;
        }

        public override async Task<Payment> MapEntityCreateDtoToEntity(PaymentCreateDto entityCreateDto)
        {
            var payment = _mapper.Map<Payment>(entityCreateDto);

            return payment;
        }

        public override Task<Payment> MapEntityToEntityDto(Payment entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Payment> MapEntityUpdateDtoToEntity(Guid id, Payment entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async override Task<Guid> InsertAsync(PaymentCreateDto entityCreateDto)
        {
            await _realEstateRepository.ChangePaymentStatus(entityCreateDto.RealEstateId);

            var realEstate = await _paymentRepository.GetByRealEstateId(entityCreateDto.RealEstateId);
            if (realEstate != null)
            {
                return default(Guid);
            }

            return await base.InsertAsync(entityCreateDto);
        }
    }
}
