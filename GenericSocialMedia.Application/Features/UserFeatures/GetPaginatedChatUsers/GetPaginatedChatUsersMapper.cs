using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetPaginatedChatUsers
{
    public class GetPaginatedChatUsersMapper : Profile
    {
        public GetPaginatedChatUsersMapper()
        {
            CreateMap<User, GetPaginatedChatUsersResponse>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(source => source.PhotoId.ToString()))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(source => source.Subscription.Name))
                .ForMember(dest => dest.NoOfUnread, opt => opt.MapFrom(source => source.SentMessages.Where(msg => !msg.IsRead).Count()));
        }
    }
}
