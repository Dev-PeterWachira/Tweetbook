using Microsoft.OpenApi;
using Tweetbook.Installers;

namespace Tweetbook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();

            services.AddSwaggerGen(x =>
           {
               x.SwaggerDoc("v1", new OpenApiInfo { Title = "Tweetbook API", Version = " v1" });
           });
        }
    }
}
