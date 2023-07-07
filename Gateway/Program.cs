using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationSchema", options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});
builder.Services.AddOcelot();
builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToString().ToLower()}.json");

//builder.Configuration
//    .SetBasePath(builder.Environment.ContentRootPath)
//    .AddJsonFile("appsettings.json", true, true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
//    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
//    .AddEnvironmentVariables();


var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
