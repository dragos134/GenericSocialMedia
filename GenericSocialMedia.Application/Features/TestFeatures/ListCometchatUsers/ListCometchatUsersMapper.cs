using AutoMapper;
using GenericSocialMedia.Domain.ServicesModels.Cometchat;

namespace GenericSocialMedia.Application.Features.TestFeatures.ListCometchatUsers
{
    public sealed class ListCometchatUsersMapper : Profile
    {
        public ListCometchatUsersMapper()
        {
            CreateMap<CometchatUser, ListCometchatUsersResponse>();
        }
    }
}
