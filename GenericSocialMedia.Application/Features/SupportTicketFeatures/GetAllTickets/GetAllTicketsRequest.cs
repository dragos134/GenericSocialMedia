using MediatR;

namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.GetAllTickets
{
    public sealed record GetAllTicketsRequest : IRequest<List<GetAllTicketsResponse>>;
}
