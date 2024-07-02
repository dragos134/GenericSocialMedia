using MediatR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.UpdateMessage
{
    public sealed record UpdateMessageRequest(int Id, string Message) : IRequest<UpdateMessageResponse>;
}
