using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Domain;
using Tweetbook.Models;

namespace Tweetbook.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}
