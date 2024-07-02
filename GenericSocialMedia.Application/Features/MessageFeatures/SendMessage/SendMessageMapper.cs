using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.MessageFeatures.SendMessage
{
    public sealed class SendMessageMapper : Profile
    {
        public SendMessageMapper()
        {
            CreateMap<Message, SendMessageResponse>();
        }
    }
}
