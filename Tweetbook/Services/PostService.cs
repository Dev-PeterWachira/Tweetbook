using Tweetbook.Models;

namespace Tweetbook.Services
{
    public class PostService : IPostServices
    {
        private readonly List<Post> _posts;

        public PostService()
        {
            _posts = new List<Post>();

            for (var i = 0; i < 5; i++)
            {
                _posts.Add(new Post
                {
                    Id = Guid.NewGuid(),
                    Name = $"Post Name {i}"
                });
            }
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public Post GetPostById(Guid postId)
        {
            return _posts.SingleOrDefault(x => x.Id == postId);
        }

        public bool updatePost(Post postToUpdate)
        {
            var exists = GetPostById(postToUpdate.Id) != null;
            if (!exists)

                return false;

            var index = _posts.FindIndex(x => x.Id == postToUpdate.Id);
            _posts[index] = postToUpdate;
            return true;
            
        }

        public bool DeletePost(Guid postId)
        {
            var post = GetPostById(postId);
            if (post == null)
                return false;
            return _posts.Remove(post);
        }
    }
}
