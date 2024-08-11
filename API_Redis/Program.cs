
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "TodoAPI", Version = "v1"});
});

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
});

//services.AddScoped<IItemRepository, ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

// Ativa o Swagger UI
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();