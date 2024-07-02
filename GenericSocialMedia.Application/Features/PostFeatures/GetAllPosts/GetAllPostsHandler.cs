using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.PostFeatures.GetAllPosts
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsRequest, List<GetAllPostsResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPostsResponse>> Handle(GetAllPostsRequest request, CancellationToken cancellationToken)
        {
            var result = await _postRepository.GetAllWithUser(cancellationToken);

            return _mapper.Map<List<GetAllPostsResponse>>(result);
        }
    }
}
