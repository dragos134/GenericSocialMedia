using MediatR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.SendSpamMessage
{
    public sealed record SendSpamMessageRequest(string IdsList, string Content) : IRequest<SendSpamMessageResponse>;
}
