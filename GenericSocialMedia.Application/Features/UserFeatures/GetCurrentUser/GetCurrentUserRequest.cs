using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetCurrentUser
{
    public sealed record GetCurrentUserRequest : IRequest<GetCurrentUserResponse>;
}
