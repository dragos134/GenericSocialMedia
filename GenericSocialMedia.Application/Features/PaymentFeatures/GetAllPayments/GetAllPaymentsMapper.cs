using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetAllPayments
{
    public sealed class GetAllPaymentsMapper : Profile
    {
        public GetAllPaymentsMapper()
        {
            CreateMap<PaymentDetails, GetAllPaymentsResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Payment.UserId))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Payment.User.Email))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.Payment.Subscription.Name))
                .ForMember(dest => dest.PaymentMessage, opt => opt.MapFrom(src => src.StatusMessage))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime));
        }
    }
}
