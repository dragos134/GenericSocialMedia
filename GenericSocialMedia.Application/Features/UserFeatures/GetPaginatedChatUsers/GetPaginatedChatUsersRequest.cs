using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetPaginatedChatUsers
{
    public sealed record GetPaginatedChatUsersRequest(int Skip, int Take) : IRequest<List<GetPaginatedChatUsersResponse>>;
}
