using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Extensions;
using Tweetbook.Models;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        // GET: api/v1/posts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postServices.GetPosts();
            return Ok(posts);
        }

        // GET: api/v1/posts/{postId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postServices.GetPostById(postId);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // POST: api/v1/posts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = new Post
            {
                Name = request.Name,
                UserID = HttpContext.GetUserId()
            };

            await _postServices.CreatePostAsync(post);

            var response = new PostResponse
            {
                Id = post.Id,
                Name = post.Name
            };

            return CreatedAtAction(nameof(Get), new { postId = post.Id }, response);
        }

        // PUT: api/v1/posts/{postId}
        [HttpPut("{postId}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid postId,
            [FromBody] UpdatePostRequests request)
        {
            var userOwnsPost = await _postServices.UserOwnsPostAsync(
                postId,
                HttpContext.GetUserId());

            if (!userOwnsPost)
                return BadRequest("You do not own this post");

            var post = await _postServices.GetPostById(postId);
            if (post == null)
                return NotFound();

            post.Name = request.Name;

            await _postServices.UpdatePost(post);

            return Ok(post);
        }

        // DELETE: api/v1/posts/{postId}
        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postServices.UserOwnsPostAsync(
                postId,
                HttpContext.GetUserId());

            if (!userOwnsPost)
                return BadRequest("You do not own this post");

            var deleted = await _postServices.DeletePost(postId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
