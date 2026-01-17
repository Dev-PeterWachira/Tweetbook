using Microsoft.EntityFrameworkCore;
using Tweetbook.Models;

namespace Tweetbook.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;
    }
}
