using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Services;
using AutoMapper;

namespace Tweetbook.Controllers.V1
{
    
    
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public class TagsController : Controller
        {
            private readonly IPostServices _postService;
        private readonly IMapper _mapper;
            public TagsController(IPostServices postService, IMapper mapper)
            {
                _postService = postService;
            _mapper = mapper;

            }

            [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "TagsViewer")]

        public async Task<IActionResult> GetAll()
            {
            var tags = await _postService.GetAllTagsAsync();
            return Ok(tags);
            }

        //[HttpGet(ApiRoutes.Tags.Get)]
        //public async Task<IActionResult> Get([FromRoute]string tagName)
        //{
        //    var tag = await _postService.GetTagByNameAsync(tagName);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<TagResponse>(tag));
        //}
        }
    }

