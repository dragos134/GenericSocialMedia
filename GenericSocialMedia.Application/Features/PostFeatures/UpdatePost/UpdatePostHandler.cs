using AutoMapper;
using Azure.Storage.Blobs;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.PostFeatures.UpdatePost
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostRequest, UpdatePostResponse>
    {
        private readonly BlobContainerClient _blobServiceClient;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePostHandler(IPostRepository postRepository, IUnitOfWork unitOfWork, IMapper mapper, BlobContainerClient blobServiceClient)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _blobServiceClient = blobServiceClient;
        }
        public async Task<UpdatePostResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var postToUpdate = await _postRepository.GetUserPost(request.PostId, request.UserId, cancellationToken);

            if (postToUpdate == null)
            {
                throw new ArgumentException();
            }

            if (request.Photo == null && string.IsNullOrEmpty(request.Description))
            {
                throw new ArgumentException();
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                postToUpdate.Description = request.Description;
            }

            if (request.Photo != null)
            {
                var photoId = postToUpdate.PhotoId;
                await _blobServiceClient.GetBlobClient(photoId.ToString()).DeleteAsync();

                var result = await _blobServiceClient.UploadBlobAsync(photoId.ToString(), request.Photo.OpenReadStream(), cancellationToken);
            }

            _postRepository.Update(postToUpdate);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<UpdatePostResponse>(null);
        }
    }
}
