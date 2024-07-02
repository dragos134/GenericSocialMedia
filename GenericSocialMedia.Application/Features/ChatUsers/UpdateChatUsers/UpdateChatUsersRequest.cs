using MediatR;

namespace GenericSocialMedia.Application.Features.ChatUsers.UpdateChatUsers
{
    public sealed record UpdateChatUsersRequest : IRequest<UpdateChatUsersResponse>;
}
