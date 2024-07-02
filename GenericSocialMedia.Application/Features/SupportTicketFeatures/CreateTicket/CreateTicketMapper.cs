using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.SupportTicketFeatures.CreateTicket
{
    public sealed class CreateTicketMapper : Profile
    {
        public CreateTicketMapper()
        {
            CreateMap<CreateTicketRequest, SupportTicket>().
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));
        }
    }
}
