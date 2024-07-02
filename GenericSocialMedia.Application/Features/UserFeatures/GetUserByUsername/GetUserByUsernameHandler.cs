using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetUserByUsername
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameRequest, GetUserByUsernameResponse>
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        public GetUserByUsernameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<GetUserByUsernameResponse> Handle(GetUserByUsernameRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username, cancellationToken);
            if (user == null)
            {
                throw new Exception();
            }

            return _mapper.Map<GetUserByUsernameResponse>(user);
        }
    }
}
