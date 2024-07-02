using MediatR;
using GenericSocialMedia.Application.Features.ChatUsers.UpdateChatUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericSocialMedia.Controllers
{
    public class ChatUsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("update-chatusers")]
        public async Task<ActionResult<UpdateChatUsersResponse>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UpdateChatUsersRequest(), cancellationToken);
            return Ok(response);
        }
    }
}
