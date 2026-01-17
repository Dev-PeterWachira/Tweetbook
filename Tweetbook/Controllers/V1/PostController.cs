using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Models;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        // GET: api/v1/post
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postServices.GetPosts();
            return Ok(posts);
        }

        // GET: api/v1/post/{postId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postServices.GetPostById(postId);
            if (post == null) return NotFound();

            return Ok(post);
        }

        // PUT: api/v1/post/{postId}
        [HttpPut("{postId}")]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequests request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name 
            };

            var updated = await _postServices.UpdatePost(post); 
            if (!updated) return NotFound();

            return Ok(post);
        }

        // DELETE: api/v1/post/{postId}
        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postServices.DeletePost(postId); // ✅ await

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
