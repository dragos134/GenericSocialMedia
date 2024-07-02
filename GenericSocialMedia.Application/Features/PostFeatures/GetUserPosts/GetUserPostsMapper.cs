using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PostFeatures.GetUserPosts
{
    public sealed class GetUserPostsMapper : Profile
    {
        public GetUserPostsMapper()
        {
            CreateMap<Post, GetUserPostsResponse>()
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.PhotoId.ToString()));
        }
    }
}
