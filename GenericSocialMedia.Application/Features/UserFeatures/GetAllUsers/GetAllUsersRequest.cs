using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllUsers
{
    public sealed record GetAllUsersRequest : IRequest<List<GetAllUsersResponse>>;
}
