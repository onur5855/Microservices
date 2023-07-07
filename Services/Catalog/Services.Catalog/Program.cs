using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Services.Catalog.ConfigurationSettings;
using Services.Catalog.Dtos;
using Services.Catalog.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());//tüm controllerlarýmýz Authorize Etrubutu eklenmiþ olucak
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

#region databaseSettings
//herhangi bi clasýn ctor ýnda IDataBaseSettings geçince Dolu gelicek(IDataBaseSettings=>DataBaseSettings=>jsondaki DataBaseSettings dan bilgileri okuyacak)
builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("DataBaseSettings"));
builder.Services.AddSingleton<IDataBaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DataBaseSettings>>().Value;
});
#endregion



builder.Services.AddScoped<ICatagoryService, CatagoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();


var app = builder.Build();
using (var scope=app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var categoryService=serviceProvider.GetRequiredService<ICatagoryService>();
    if (!categoryService.GetAllAsync().Result.Data.Any())
    {
        categoryService.CreateAsync(new CategoryDto { Name="FakeData Categori 1"}).Wait();
        categoryService.CreateAsync(new CategoryDto { Name="FakeData Categori 2"}).Wait();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
