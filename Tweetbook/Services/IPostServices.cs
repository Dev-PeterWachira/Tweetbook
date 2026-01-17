using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook.Models;

namespace Tweetbook.Services
{
    public interface IPostServices
    {
        Task<List<Post>> GetPosts();
        Task<Post?> GetPostById(Guid postId);  
        Task<bool> CreatePost(Post post);     
        Task<bool> UpdatePost(Post postToUpdate);
        Task<bool> DeletePost(Guid postId);
    }
}
