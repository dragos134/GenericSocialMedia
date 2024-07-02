using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.GetAllTickets
{
    public class GetAllTicketsHandler : IRequestHandler<GetAllTicketsRequest, List<GetAllTicketsResponse>>
    {
        private readonly ISupportTicketRepository _supportTicketsRepository;
        private readonly IMapper _mapper;
        public GetAllTicketsHandler(ISupportTicketRepository supportTicketRepository, IMapper mapper)
        {
            _supportTicketsRepository = supportTicketRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllTicketsResponse>> Handle(GetAllTicketsRequest request, CancellationToken cancellationToken)
        {
            var tickets = await _supportTicketsRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllTicketsResponse>>(tickets.OrderByDescending(x => x.CreatedAt));
        }
    }
}
