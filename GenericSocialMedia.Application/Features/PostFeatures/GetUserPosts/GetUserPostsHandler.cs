using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.PostFeatures.GetUserPosts
{
    public class GetUserPostsHandler : IRequestHandler<GetUserPostsRequest, IEnumerable<GetUserPostsResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserPostsHandler(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetUserPostsResponse>> Handle(GetUserPostsRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username, cancellationToken);
            var posts = await _postRepository.GetUserPosts(user.Id, cancellationToken);
            return _mapper.Map<IEnumerable<GetUserPostsResponse>>(posts);
        }
    }
}
