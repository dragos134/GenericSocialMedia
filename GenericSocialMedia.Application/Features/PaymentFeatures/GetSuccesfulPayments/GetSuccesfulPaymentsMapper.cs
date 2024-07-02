using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetSuccesfulPayments
{
    public sealed class GetSuccesfulPaymentsMapper : Profile
    {
        public GetSuccesfulPaymentsMapper()
        {
            CreateMap<PaymentDetails, GetSuccesfulPaymentsResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Payment.User.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt.DateTime))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Payment.User.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Payment.User.Username))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.Payment.Subscription.Name));
        }
    }
}
