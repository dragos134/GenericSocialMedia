using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.GetAllTickets
{
    public sealed class GetAllTicketsMapper : Profile
    {
        public GetAllTicketsMapper()
        {
            CreateMap<SupportTicket, GetAllTicketsResponse>().
                ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name)).
                ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.DateTime));
        }
    }
}
