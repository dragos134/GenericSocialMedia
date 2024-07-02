using MediatR;
using GenericSocialMedia.Application.Features.MessageFeatures.GetConversation;
using GenericSocialMedia.Application.Features.MessageFeatures.ReadMessagesFromConversation;
using GenericSocialMedia.Application.Features.MessageFeatures.SendMessage;
using GenericSocialMedia.Application.Features.MessageFeatures.SendSpamMessage;
using GenericSocialMedia.Application.Features.MessageFeatures.UpdateMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GenericSocialMedia.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("user/{username}")]
        public async Task<ActionResult<List<GetConversationResponse>>> GetAll([FromRoute] GetConversationRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SendMessageResponse>> SendMessage(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UpdateMessageResponse>> UpdateMessage(UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("spam")]
        public async Task<ActionResult<SendSpamMessageResponse>> SendSpamMessage(SendSpamMessageRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [Route("read/{senderUsername}")]
        public async Task<ActionResult<ReadMessagesFromConversationResponse>> UpdateUnreadMessages([FromRoute] string senderUsername, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ReadMessagesFromConversationRequest(senderUsername), cancellationToken);
            return Ok(response);
        }
    }
}
