using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRecruitedUsers
{
    public sealed record GetAllRecruitedUsersRequest : IRequest<List<GetAllRecruitedUsersResponse>>;
}
