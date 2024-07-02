using GenericSocialMedia.Application.Features.UserFeatures.AuthenticateUser;
using GenericSocialMedia.Application.Features.UserFeatures.GetCurrentUser;
using GenericSocialMedia.Application.Features.UserFeatures.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GenericSocialMedia.Controllers
{

    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public class AuthenticationRequestBody
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public AuthenticationController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [Route("api/register")]
        [HttpPost]
        public async Task<ActionResult<RegisterUserResponse>> Register(RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Route("api/refresh")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> Refresh(
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCurrentUserRequest(), cancellationToken);
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("username", response.Username),
                new Claim("email", response.Email),
                new Claim("chat_secret", response.ChatSecret),
                new Claim("gender", response.Gender),
                new Claim("userId", response.Id.ToString()),
                new Claim("canUseComchat", response.CanUseComchat.ToString()),
                new Claim("canCall", response.CanCall.ToString())
            };


            foreach (var role in response.Roles)
            {
                claimsForToken.Add(new Claim("roles", role));
            }


            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(720),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }

        [Route("api/authenticate")]
        [HttpPost]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody request,
            CancellationToken cancellationToken)
        {
            var userIsValidated = await ValidateUserCredentials(request.Email, request.Password, cancellationToken);
            if (userIsValidated == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("username", userIsValidated.Username),
                new Claim("email", userIsValidated.Email),
                new Claim("chat_secret", userIsValidated.ChatSecret),
                new Claim("gender", userIsValidated.Gender),
                new Claim("userId", userIsValidated.Id.ToString()),
                new Claim("canUseComchat", userIsValidated.CanUseComchat.ToString()),
                new Claim("canCall", userIsValidated.CanCall.ToString())
            };


            foreach (var role in userIsValidated.Roles)
            {
                claimsForToken.Add(new Claim("roles", role));
            }


            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(720),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }

        private async Task<ValidateUserCredentialsResponse> ValidateUserCredentials(string email, string password,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ValidateUserCredentialsRequest(email, password), cancellationToken);
        }

    }
}
