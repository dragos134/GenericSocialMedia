using MediatR;

namespace GenericSocialMedia.Application.Features.RoleFeatures.CreateRole
{
    public sealed record CreateRoleRequest(string Name) : IRequest<CreateRoleResponse>;
}
