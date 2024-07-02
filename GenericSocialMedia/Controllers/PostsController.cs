using MediatR;
using GenericSocialMedia.Application.Features.PostFeatures.CreatePost;
using GenericSocialMedia.Application.Features.PostFeatures.DeletePost;
using GenericSocialMedia.Application.Features.PostFeatures.GetAllPosts;
using GenericSocialMedia.Application.Features.PostFeatures.GetUserPosts;
using GenericSocialMedia.Application.Features.PostFeatures.UpdatePost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericSocialMedia.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllPostsResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllPostsRequest(), cancellationToken);
            Random rng = new Random();
            int n = response.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = response[k];
                response[k] = response[n];
                response[n] = value;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<IEnumerable<GetUserPostsResponse>>> GetUserPosts([FromRoute] string username, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserPostsRequest(username), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreatePostResponse>> Create([FromForm] CreatePostRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("id")]
        public async Task<ActionResult<UpdatePostResponse>> Update(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirst("userId")?.ToString() ?? "0");
            if (userId != request.UserId)
            {
                return Unauthorized();
            }

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete]
        [Route("id")]
        public async Task<ActionResult<DeletePostResponse>> Delete(DeletePostRequest request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirst("userId")?.ToString() ?? "0");
            if (userId != request.UserId)
            {
                return Unauthorized();
            }

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
