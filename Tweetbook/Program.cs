using Tweetbook.Installers;

var builder = WebApplication.CreateBuilder(args);

// Installers do everything
builder.Services.InstallServicesInAssembly(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
