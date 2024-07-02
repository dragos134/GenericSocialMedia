using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.UpdateUser
{
    public sealed record UpdateUserRequest(int Id, string FirstName, string LastName, string Email, int SubscriptionId) : IRequest<UpdateUserResponse>;
}
