using Azure.Storage.Blobs;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using Microsoft.Extensions.Configuration;

namespace GenericSocialMedia.Application.Features.UserFeatures.UploadPhoto
{
    public class UploadPhotoHandler : IRequestHandler<UploadPhotoRequest, UploadPhotoResponse>
    {
        private readonly BlobContainerClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UploadPhotoHandler(BlobContainerClient blobServiceClient, IUserRepository userRepository, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _blobServiceClient = blobServiceClient;
            _userRepository = userRepository;
            _configuration = configuration;
            _unitOfWork = unitOfWork;

        }
        public async Task<UploadPhotoResponse> Handle(UploadPhotoRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetCurrentUser(cancellationToken);
            if (currentUser.PhotoId != null)
            {
                await _blobServiceClient.GetBlobClient(currentUser.PhotoId.ToString()).DeleteAsync();
            }
            var photoId = Guid.NewGuid();
            var result = await _blobServiceClient.UploadBlobAsync(photoId.ToString(), request.File.OpenReadStream(), cancellationToken);
            await _userRepository.UpdateUserProfilePhoto(photoId, cancellationToken);
            await _unitOfWork.Save(cancellationToken);
            return new UploadPhotoResponse { PhotoUrl = $"{_configuration["Azure:BlobUri"]}{photoId}" };
        }
    }
}
