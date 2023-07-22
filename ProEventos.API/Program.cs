using Data.Configuration;
using Application.Configurations.Extensions;
using Application.Configurations.Middleware;
using ProEventos.API.Extensions.Dependencies;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm} {Level:u3}] {Message:lj}{NewLine}{Exception}\n",
    theme: AnsiConsoleTheme.Sixteen)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddCors();
builder.Services.AddControllersConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyInjections();
builder.Services.AddConfigureDatabase(builder.Configuration);


// HTTP request pipeline.
var app = builder.Build();
//app.UseSerilogRequestLogging();

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


app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
