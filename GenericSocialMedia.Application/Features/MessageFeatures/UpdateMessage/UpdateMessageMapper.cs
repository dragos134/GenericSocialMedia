using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.MessageFeatures.UpdateMessage
{
    public sealed class UpdateMessageMapper : Profile
    {
        public UpdateMessageMapper()
        {
            CreateMap<Message, UpdateMessageResponse>();
        }
    }
}
