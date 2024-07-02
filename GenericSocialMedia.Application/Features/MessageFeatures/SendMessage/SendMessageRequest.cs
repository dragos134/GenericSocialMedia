using MediatR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.SendMessage
{
    public sealed record SendMessageRequest(string Username, string Content) : IRequest<SendMessageResponse>;
}
