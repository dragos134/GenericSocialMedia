using MediatR;

namespace GenericSocialMedia.Application.Features.TestFeatures.ListCometchatUsers
{
    public sealed record ListCometchatUsersRequest(string SearchKey) : IRequest<List<ListCometchatUsersResponse>>;
}
