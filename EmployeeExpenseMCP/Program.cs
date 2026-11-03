using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services
    .AddMcpServer()
    .WithHttpTransport(o => o.SessionMode = HttpServerSessionMode.Stateless)
    .WithToolsFromAssembly();

var app = builder.Build();

app.UseCors();

app.MapMcp("/mcp");

app.Run();