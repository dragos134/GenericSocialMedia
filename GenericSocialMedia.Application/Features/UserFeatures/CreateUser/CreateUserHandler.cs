using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository, ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var userRole = await _roleRepository.GetRoleByName("User");
            if (userRole == null)
            {
                throw new Exception();
            }
            var subscription = await _subscriptionRepository.GetByName("Free");
            if (subscription == null)
            {
                throw new Exception();
            }
            user.Roles.Add(userRole);
            user.Subscription = subscription;
            user.ChatSecret = Guid.NewGuid();
            user.RemainingMessages = 10;

            _userRepository.Create(user);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<CreateUserResponse>(user);
        }
    }
}
