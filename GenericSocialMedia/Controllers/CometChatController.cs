using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GenericSocialMedia.Controllers
{
    [Route("api/cometchat")]
    [ApiController]
    public class CometChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CometChatController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
