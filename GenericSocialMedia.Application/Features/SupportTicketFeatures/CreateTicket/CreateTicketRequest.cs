using MediatR;

namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.CreateTicket
{
    public sealed record CreateTicketRequest(string FullName, string Email, string Message) : IRequest<CreateTicketResponse>;
}
