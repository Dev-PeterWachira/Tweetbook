using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tweetbook.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=TweetbookDb;User Id=sa;Password=Manchester62!;TrustServerCertificate=True");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
