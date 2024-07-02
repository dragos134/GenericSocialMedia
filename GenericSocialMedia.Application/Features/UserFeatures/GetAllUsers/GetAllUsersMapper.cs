using AutoMapper;
using GenericSocialMedia.Application.Common.Helpers;
using GenericSocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllUsers
{
    public sealed class GetAllUsersMapper : Profile
    {
        public GetAllUsersMapper()
        {
            CreateMap<User, GetAllUsersResponse>()
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.Subscription.Name));
        }
    }
}
