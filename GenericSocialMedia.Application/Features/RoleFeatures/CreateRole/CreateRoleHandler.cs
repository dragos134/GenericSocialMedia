using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.RoleFeatures.CreateRole
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleRequest, CreateRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var roleToAdd = _mapper.Map<Role>(request);
            _roleRepository.Create(roleToAdd);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<CreateRoleResponse>(roleToAdd);
        }
    }
}
