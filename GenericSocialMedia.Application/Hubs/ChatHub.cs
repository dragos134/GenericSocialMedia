using GenericSocialMedia.Application.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace GenericSocialMedia.Application.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task GoOnline(string userId)
        {
            var user = await _userRepository.Get(int.Parse(userId), CancellationToken.None);
            user.IsOnline = true;
            user.ChatConnectionId = Context.ConnectionId;
            _userRepository.Update(user);
            await _unitOfWork.Save(CancellationToken.None);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var user = await _userRepository.GetByChatConnectionId(Context.ConnectionId, CancellationToken.None);
            if (user == null)
            {
                return;
            }
            user.IsOnline = false;
            user.ChatConnectionId = null;
            await _unitOfWork.Save(CancellationToken.None);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
