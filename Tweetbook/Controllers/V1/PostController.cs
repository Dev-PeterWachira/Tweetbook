using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1;
using Tweetbook.Services;


namespace Tweetbook.Controllers.V1
{
    public class PostController : Controller
    {
        private IPostServices _postServices;

        public PostController (IPostServices postService)
        {
            _postServices = postService;
        }
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postServices.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postServices.GetPostById(postId);

            if (post == null)

                return NotFound();
            return Ok(post);
        }

    }
}
