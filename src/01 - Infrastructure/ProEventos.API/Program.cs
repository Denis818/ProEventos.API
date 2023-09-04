using Data.Configuration;
using Application.Configurations.Extensions;
using ProEventos.API.Configuration.Middleware;
using ProEventos.API.Configuration.Extensions.Dependencies;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSeriLog();
builder.Services.AddCors();
builder.Services.AddControllersConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyInjections();
builder.Services.AddConfigureDatabase(builder.Configuration);


// HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCorsPolicy();

app.UseAuthentication();

app.UseAuthorization();


app.UseMiddleware<MiddlewareException>();

app.MapControllers();

app.Run();
