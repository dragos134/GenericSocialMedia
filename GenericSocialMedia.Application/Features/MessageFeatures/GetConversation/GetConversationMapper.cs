using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.MessageFeatures.GetConversation
{
    public sealed class GetConversationMapper : Profile
    {
        public GetConversationMapper()
        {
            CreateMap<Message, GetConversationResponse>()
                .ForMember(dest => dest.SenderUsername, opt => opt.MapFrom(source => source.Sender.Username))
                .ForMember(dest => dest.ReceiverUsername, opt => opt.MapFrom(source => source.Receiver.Username))
                .ForMember(dest => dest.SenderFullname, opt => opt.MapFrom(source => $"{source.Sender.FirstName} {source.Sender.LastName}"))
                .ForMember(dest => dest.ReceiverFullname, opt => opt.MapFrom(source => $"{source.Receiver.FirstName} {source.Receiver.LastName}"))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(source => source.CreatedAt.UtcDateTime));
        }
    }
}
