using MediatR;
using GenericSocialMedia.Application.Services;

namespace GenericSocialMedia.Application.Features.TestFeatures.CreateCometchatUser
{
    public class CreateCometchatUserHandler : IRequestHandler<CreateCometchatUserRequest, CreateCometchatUserResponse>
    {
        private readonly ICometChatService _cometChatService;
        public CreateCometchatUserHandler(ICometChatService cometChatService)
        {
            _cometChatService = cometChatService;
        }
        public async Task<CreateCometchatUserResponse> Handle(CreateCometchatUserRequest request, CancellationToken cancellationToken)
        {
            await _cometChatService.CreateUser(request.Username, request.Uid);
            return null;
        }
    }
}
