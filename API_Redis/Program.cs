using API_Redis.Redis;
using Microsoft.OpenApi.Models;
using Redis.OM;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<IndexCreationService>();

builder.Services.AddSingleton(new RedisConnectionProvider(ConnectionMultiplexer.Connect("localhost:6379")));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "TodoAPI", Version = "v1"});
});

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