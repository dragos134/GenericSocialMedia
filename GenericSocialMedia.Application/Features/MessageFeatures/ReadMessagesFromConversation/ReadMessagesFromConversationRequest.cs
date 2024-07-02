using MediatR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.ReadMessagesFromConversation
{
    public sealed record ReadMessagesFromConversationRequest(string SenderUsername) : IRequest<ReadMessagesFromConversationResponse>;
}
