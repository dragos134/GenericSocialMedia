using MediatR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.GetConversation
{
    public sealed record GetConversationRequest(string Username) : IRequest<List<GetConversationResponse>>;
}
