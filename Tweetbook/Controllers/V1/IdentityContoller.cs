using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Services;
using Tweetbook.Contracts.V1.Responses;

namespace Tweetbook.Controllers.V1
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.email, request.password);

            if(!authResponse.success)
            {
                return BadRequest(new AuthFailedResponse
                    { 
                    Errors = authResponse.Errors
                });

            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }


        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });

            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
              
            });
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Login([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });

            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token

            });
        }
    }
}
 