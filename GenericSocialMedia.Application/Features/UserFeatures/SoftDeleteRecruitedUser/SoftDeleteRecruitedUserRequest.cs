using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.SoftDeleteRecruitedUser
{
    public sealed record SoftDeleteRecruitedUserRequest(int UserId) : IRequest<SoftDeleteRecruitedUserResponse>;
}
