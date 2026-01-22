using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tweetbook.Data;
using Tweetbook.Installers;
using Tweetbook.Options;
using Tweetbook.Services;

public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));

        // ----------------------------
        // IdentityCore (no cookies)
        // ----------------------------
        services.AddIdentityCore<IdentityUser>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

        // ----------------------------
        // JWT Settings
        // ----------------------------
        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
        };
        services.AddSingleton(tokenValidationParameters);

        // ----------------------------
        // Application Services
        // ----------------------------
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IPostServices, PostService>();

        // ----------------------------
        // JWT Authentication (only once!)
        // ----------------------------
        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.SaveToken = true;
        //    options.TokenValidationParameters = tokenValidationParameters;
        //});

        // ----------------------------
        // Swagger
        // ----------------------------
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tweetbook API", Version = "v1" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter: Bearer {your JWT token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
        });
    }
}
