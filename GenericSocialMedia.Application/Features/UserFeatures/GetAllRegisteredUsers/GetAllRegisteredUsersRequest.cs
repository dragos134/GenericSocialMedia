using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRegisteredUsers
{
    public sealed record GetAllRegisteredUsersRequest : IRequest<List<GetAllRegisteredUsersResponse>>;
}
