using AutoMapper;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.CreatePaymentDetails
{
    public sealed class CreatePaymentDetailsMapper : Profile
    {
        public CreatePaymentDetailsMapper()
        {
            CreateMap<int, CreatePaymentDetailsResponse>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src));
        }
    }
}
