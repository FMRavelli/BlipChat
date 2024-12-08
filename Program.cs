using BlipChat.Services;
using BlipChat.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<IGitHubRepository, GitHubRepository>();
builder.Services.AddHttpClient<GitHubRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();