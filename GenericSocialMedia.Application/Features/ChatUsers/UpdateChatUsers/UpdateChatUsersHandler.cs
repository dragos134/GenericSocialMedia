using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.ChatUsers.UpdateChatUsers
{
    public sealed class UpdateChatUsersHandler : IRequestHandler<UpdateChatUsersRequest, UpdateChatUsersResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatUsersRepository _chatUsersRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateChatUsersHandler(IUserRepository userRepository, IChatUsersRepository chatUsersRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _chatUsersRepository = chatUsersRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateChatUsersResponse> Handle(UpdateChatUsersRequest request, CancellationToken cancellationToken)
        {
            var allUsers = await _userRepository.GetAll(cancellationToken);
            foreach (var user in allUsers)
            {
                var chatUsers = await _userRepository.GetChatUsers(user.Id, cancellationToken);
                foreach (var chatUser in chatUsers)
                {
                    var lastMessageAt = chatUser.SentMessages
                                .Union(chatUser.ReceivedMessages)
                                .OrderByDescending(u => u.CreatedAt)
                                .FirstOrDefault().CreatedAt;
                    _chatUsersRepository.Create(new Domain.Entities.ChatUsers
                    {
                        UserId = user.Id,
                        ChatUserId = chatUser.Id,
                        LastMessageAt = lastMessageAt.DateTime,
                    });
                }
            }
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<UpdateChatUsersResponse>(null);
        }
    }
}
