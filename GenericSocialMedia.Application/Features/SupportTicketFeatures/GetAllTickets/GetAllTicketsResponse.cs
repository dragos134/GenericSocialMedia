namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.GetAllTickets
{
    public sealed record GetAllTicketsResponse
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
