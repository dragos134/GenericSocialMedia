using AutoMapper;
using Azure.Storage.Blobs;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PostFeatures.CreatePost
{
    public sealed class CreatePostHandler : IRequestHandler<CreatePostRequest, CreatePostResponse>
    {
        private readonly BlobContainerClient _blobServiceClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreatePostHandler(IUnitOfWork unitOfWork, IPostRepository postRepository, IUserRepository userRepository, IMapper mapper, BlobContainerClient blobServiceClient)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<CreatePostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetCurrentUser(cancellationToken);
            var desc = request.Description;
            if (string.IsNullOrEmpty(desc))
            {
                desc = string.Empty;
            }
            var postToAdd = new Post
            {
                User = currentUser,
                Description = desc.Replace("\r", "\\r").Replace("\n", "\\n")
            };

            if (request.Photo != null)
            {
                var photoId = Guid.NewGuid();
                var result = await _blobServiceClient.UploadBlobAsync(photoId.ToString(), request.Photo.OpenReadStream(), cancellationToken);
                postToAdd.PhotoId = photoId;
            }

            _postRepository.Create(postToAdd);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<CreatePostResponse>(null);
        }
    }
}
