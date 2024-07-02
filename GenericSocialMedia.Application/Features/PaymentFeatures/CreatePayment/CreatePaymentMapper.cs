using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.CreatePayment
{
    public sealed class CreatePaymentMapper : Profile
    {
        public CreatePaymentMapper()
        {
            CreateMap<Payment, CreatePaymentResponse>();
        }
    }
}
