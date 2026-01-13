using Tweetbook.Services;
using Tweetbook.Models;

namespace Tweetbook.Services
{
    public interface IPostServices
    {
        List<Post> GetPosts();
        Post GetPostById(Guid postId);
    }
}
