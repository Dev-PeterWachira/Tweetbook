using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    
    
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public class TagsController : Controller
        {
            private readonly IPostServices _postService;
            public TagsController(IPostServices postService)
            {
                _postService = postService;

            }

            [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "TagsViewer")]

        public async Task<IActionResult> GetAll()
            {
            var tags = await _postService.GetAllTagsAsync();
            return Ok(tags);
            }
        }
    }

