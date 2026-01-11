using Tweetbook.Installers; // 👈 IMPORTANT
using Microsoft.OpenApi;

namespace Tweetbook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔗 LINK YOUR INSTALLERS HERE
            builder.Services.InstallServicesInAssembly(builder.Configuration);

            var app = builder.Build();

           
            app.Run();
        }
    }
}
