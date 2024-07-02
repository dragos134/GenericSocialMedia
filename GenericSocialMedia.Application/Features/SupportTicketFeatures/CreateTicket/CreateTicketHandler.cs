using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.CreateTicket
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketRequest, CreateTicketResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly IMapper _mapper;
        public CreateTicketHandler(ISupportTicketRepository supportTicketRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _supportTicketRepository = supportTicketRepository;
        }

        public async Task<CreateTicketResponse> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var ticketToAdd = _mapper.Map<SupportTicket>(request);
            _supportTicketRepository.Create(ticketToAdd);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<CreateTicketResponse>(null);
        }
    }
}
