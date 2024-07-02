using AutoMapper;
using Azure.Storage.Blobs;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.PostFeatures.DeletePost
{
    public class DeletePostHandler : IRequestHandler<DeletePostRequest, DeletePostResponse>
    {
        private readonly BlobContainerClient _blobServiceClient;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeletePostHandler(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            BlobContainerClient blobServiceClient)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;

        }
        public async Task<DeletePostResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(request.PostId, cancellationToken);

            if (post.PhotoId != null)
            {
                await _blobServiceClient.GetBlobClient(post.PhotoId.ToString()).DeleteAsync();
            }

            _postRepository.Delete(post);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<DeletePostResponse>(post);
        }
    }
}
