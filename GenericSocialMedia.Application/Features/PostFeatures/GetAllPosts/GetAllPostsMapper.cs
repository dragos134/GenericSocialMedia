using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PostFeatures.GetAllPosts
{
    public class GetAllPostsMapper : Profile
    {
        public GetAllPostsMapper()
        {
            CreateMap<Post, GetAllPostsResponse>()
                .ForMember(x => x.PostTime, opt => opt.MapFrom(source => source.CreatedAt.UtcDateTime))
                .ForMember(x => x.Username, opt => opt.MapFrom(source => source.User.Username))
                .ForMember(x => x.ImageId, opt => opt.MapFrom(source => source.PhotoId.ToString()))
                .ForMember(x => x.ProfilePictureId, opt => opt.MapFrom(source => source.User.PhotoId.ToString()))
                .ForMember(x => x.UserFullName, opt => opt.MapFrom(source => $"{source.User.FirstName} {source.User.LastName}"));
        }
    }
}
