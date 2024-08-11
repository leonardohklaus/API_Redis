
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "TodoAPI", Version = "v1"});
});

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis_cache:6379";
});

var app = builder.Build();

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