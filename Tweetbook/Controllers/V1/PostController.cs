using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Services;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Models;




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


        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequests request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var Updated = _postServices.updatePost(post);

            if (Updated)
            {
                return Ok(post);
                return NotFound();

            }

            return Ok(_postServices.GetPosts());
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            var deleted = _postServices.DeletePost(postId);
            if (deleted)
            {
                return NoContent();
            }
            return NotFound();
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
