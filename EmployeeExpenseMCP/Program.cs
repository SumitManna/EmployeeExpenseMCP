using Azure.Identity;
using EmployeeExpenseMCP;
using EmployeeExpenseMCP.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

var options = new DefaultAzureCredentialOptions
{
    // Exclude production credentials when running locally to avoid timeout delays
    ExcludeManagedIdentityCredential = builder.Environment.IsDevelopment(),
    ExcludeEnvironmentCredential = builder.Environment.IsDevelopment(),

    // Increase the timeout if your machine is slow to shell out to the Azure CLI
    CredentialProcessTimeout = TimeSpan.FromSeconds(20)
};

var credential = new DefaultAzureCredential(options);
builder.Services.AddSingleton(credential);
builder.Configuration.AddAzureAppConfiguration(options =>
{
    string endpoint = Environment.GetEnvironmentVariable("AppConfig")!;
    options.Connect(new Uri(endpoint!), credential);
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("EmployeeExpense:Settings"));

var appSettings = builder.Configuration.GetSection("EmployeeExpense:Settings").Get<AppSettings>();
Console.WriteLine($"EmployeeExpenseApiUrl: {appSettings?.EmployeeExpenseApiUrl}");

builder.Services.AddHttpClient("EmployeeExpenseApi", client =>
{
    client.BaseAddress = new Uri(appSettings?.EmployeeExpenseApiUrl ?? "https://employeeexpenseapi-dfbqamd5b2e7dqf8.indiasouthcentral-01.azurewebsites.net");
});
builder.Services.AddHttpClient<EmployeeDetails>("EmployeeExpenseApi");

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