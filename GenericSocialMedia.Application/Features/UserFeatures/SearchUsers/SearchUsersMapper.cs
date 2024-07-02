using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.SearchUsers
{
    public sealed class SearchUsersMapper : Profile
    {
        public SearchUsersMapper()
        {
            CreateMap<User, SearchUsersResponse>()
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(src => src.PhotoId.ToString()))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
