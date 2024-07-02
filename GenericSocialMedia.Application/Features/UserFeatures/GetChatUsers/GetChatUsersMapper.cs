using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetChatUsers
{
    public sealed class GetChatUsersMapper : Profile
    {
        public GetChatUsersMapper()
        {
            CreateMap<User, GetChatUsersResponse>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(source => source.PhotoId.ToString()))
                .ForMember(dest => dest.LastMessage,
                    opt => opt.MapFrom(
                            (source) => source.SentMessages
                                .Union(source.ReceivedMessages)
                                .OrderByDescending(u => u.CreatedAt)
                                .FirstOrDefault().Content))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(source => source.Subscription.Name))
                .ForMember(dest => dest.NoOfUnread, opt => opt.MapFrom(source => source.SentMessages.Where(msg => !msg.IsRead).Count()));
        }
    }
}
