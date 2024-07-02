using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetChatUsers
{
    public sealed record GetChatUsersRequest : IRequest<List<GetChatUsersResponse>>;

}
