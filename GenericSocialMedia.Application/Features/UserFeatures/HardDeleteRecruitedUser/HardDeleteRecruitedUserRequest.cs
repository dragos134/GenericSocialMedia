using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.HardDeleteRecruitedUser
{
    public sealed record HardDeleteRecruitedUserRequest(int UserId) : IRequest<HardDeleteRecruitedUserResponse>;
}
