using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class SupportTicketRepository : BaseRepository<SupportTicket>, ISupportTicketRepository
    {
        public SupportTicketRepository(
            DataContext context,
            IUserService userService) : base(context, userService)
        {
        }
    }
}
