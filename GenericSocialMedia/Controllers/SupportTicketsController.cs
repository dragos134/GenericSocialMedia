using MediatR;
using GenericSocialMedia.Application.Features.SupportTicketFeatures.CreateTicket;
using GenericSocialMedia.Application.Features.SupportTicketFeatures.GetAllTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericSocialMedia.Controllers
{
    [Route("api/support")]
    [ApiController]
    public class SupportTicketsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public SupportTicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<GetAllTicketsResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllTicketsRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTicketResponse>> Create(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
