using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Services;

namespace GenericSocialMedia.Application.Features.TestFeatures.ListCometchatUsers
{
    public class ListCometchatUsersHandler : IRequestHandler<ListCometchatUsersRequest, List<ListCometchatUsersResponse>>
    {
        private readonly ICometChatService _cometChatService;
        private readonly IMapper _mapper;
        public ListCometchatUsersHandler(ICometChatService cometChatService, IMapper mapper)
        {
            _cometChatService = cometChatService;
            _mapper = mapper;
        }
        public async Task<List<ListCometchatUsersResponse>> Handle(ListCometchatUsersRequest request, CancellationToken cancellationToken)
        {
            var result = await _cometChatService.ListUsers(request.SearchKey);
            if (result == null)
            {
                throw new Exception();
            }
            return _mapper.Map<List<ListCometchatUsersResponse>>(result.Data);
        }
    }
}
