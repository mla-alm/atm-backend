using AtmBackend.Services;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var acceptedOrigins = builder.Configuration.GetSection("AcceptedOrigins").Get<string[]>() ?? [];

// Clear default logging providers and add only the console logger with JSON formatting
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService(builder.Environment.ApplicationName))
    .WithLogging();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins(acceptedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IDenominationService, DenominationService>();
builder.Services.AddTransient<IWithdrawalService, WithdrawalService>();

builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(d =>
{
    d.Title = "ATM Backend API";
    d.Version = "v1";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

await app.RunAsync();

public partial class Program { }
