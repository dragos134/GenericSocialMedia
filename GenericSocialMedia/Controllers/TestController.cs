using MediatR;
using GenericSocialMedia.Application.Features.TestFeatures.CreateCometchatUser;
using GenericSocialMedia.Application.Features.TestFeatures.ListCometchatUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericSocialMedia.Controllers
{
    [Route("api/test")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("cometchat")]
        public async Task<ActionResult<CreateCometchatUserResponse>> CreateCometchatUser(CreateCometchatUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("cometchat/{searchKey}")]
        public async Task<ActionResult<List<ListCometchatUsersResponse>>> ListCometchatUser([FromRoute] string searchKey, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ListCometchatUsersRequest(searchKey), cancellationToken);
            return Ok(response);
        }
    }
}
