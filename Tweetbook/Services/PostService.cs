using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Models;
using Tweetbook.Data;

namespace Tweetbook.Services
{
    public class PostService : IPostServices
    {
        private readonly DataContext _datcontext;

        public PostService(DataContext datcontext)
        {
            _datcontext = datcontext;
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _datcontext.Posts.ToListAsync();
        }

        public async Task<Post?> GetPostById(Guid postId)
        {
            return await _datcontext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }


        public async Task<bool> CreatePostAsync(Post post)
        {
           await _datcontext.Posts.AddAsync(post);
            var created = await _datcontext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePost(Post postToUpdate)
        {
            _datcontext.Posts.Update(postToUpdate);
            var updated = await _datcontext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            var post = await GetPostById(postId);
            if (post == null) return false;

            _datcontext.Posts.Remove(post);
            var deleted = await _datcontext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await _datcontext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);
            if (post == null) 
                return false;
            
            
                return post.UserID == userId;
            
            return true;
        }          
    }
}
