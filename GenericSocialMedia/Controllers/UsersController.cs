using MediatR;
using GenericSocialMedia.Application.Features.UserFeatures.CheckEmailExists;
using GenericSocialMedia.Application.Features.UserFeatures.CheckUsernameExists;
using GenericSocialMedia.Application.Features.UserFeatures.CreateUser;
using GenericSocialMedia.Application.Features.UserFeatures.GetAllRecruitedUsers;
using GenericSocialMedia.Application.Features.UserFeatures.GetAllRegisteredUsers;
using GenericSocialMedia.Application.Features.UserFeatures.GetAllUsers;
using GenericSocialMedia.Application.Features.UserFeatures.GetChatUsers;
using GenericSocialMedia.Application.Features.UserFeatures.GetCurrentUser;
using GenericSocialMedia.Application.Features.UserFeatures.GetPaginatedChatUsers;
using GenericSocialMedia.Application.Features.UserFeatures.GetUserByUsername;
using GenericSocialMedia.Application.Features.UserFeatures.HardDeleteRecruitedUser;
using GenericSocialMedia.Application.Features.UserFeatures.SearchUsers;
using GenericSocialMedia.Application.Features.UserFeatures.SoftDeleteRecruitedUser;
using GenericSocialMedia.Application.Features.UserFeatures.UpdateUser;
using GenericSocialMedia.Application.Features.UserFeatures.UploadPhoto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericSocialMedia.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<GetAllUsersResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUsersRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("registered")]
        public async Task<ActionResult<List<GetAllRegisteredUsersResponse>>> GetAllRegistered(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllRegisteredUsersRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("{username}")]
        [Authorize]
        public async Task<ActionResult<GetUserByUsernameResponse>> GetByUsername([FromRoute] string username, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserByUsernameRequest(username), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("recruited")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<GetAllRecruitedUsersResponse>>> GetAllRecruited(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllRecruitedUsersRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("recruited")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateRecruitedUser(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete]
        [Route("recruited/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<SoftDeleteRecruitedUserResponse>> SoftDeleteRecruitedUser([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteRecruitedUserRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpDelete]
        [Route("recruited/{id}/hard")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<HardDeleteRecruitedUserResponse>> HardDeleteRecruitedUser([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new HardDeleteRecruitedUserRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Route("current")]
        public async Task<ActionResult<GetCurrentUserResponse>> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCurrentUserRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [Route("current/photo")]
        public async Task<ActionResult<UploadPhotoResponse>> UploadPhoto([FromForm] UploadPhotoRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("chatusers")]
        public async Task<ActionResult<List<GetChatUsersResponse>>> GetChatUsers(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetChatUsersRequest(), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("paginatedchatusers")]
        public async Task<ActionResult<List<GetPaginatedChatUsersResponse>>> GetChatUsers([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPaginatedChatUsersRequest(skip, take), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("chatusers/{searchText}")]
        public async Task<ActionResult<List<SearchUsersResponse>>> GetChatUsers([FromRoute] string searchText, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SearchUsersRequest(searchText), cancellationToken);
            return Ok(response);
        }



        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("check-email/{searchText}")]
        public async Task<ActionResult<CheckEmailExistsResponse>> CheckEmail([FromRoute] string searchText, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CheckEmailExistsRequest(searchText), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("check-username/{searchText}")]
        public async Task<ActionResult<CheckUsernameExistsResponse>> CheckUsername([FromRoute] string searchText, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CheckUsernameExistsRequest(searchText), cancellationToken);
            return Ok(response);
        }
    }
}
