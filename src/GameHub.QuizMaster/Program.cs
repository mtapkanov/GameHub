using GameHub.QuizMaster.Infrastructure.Configuration;
using GameHub.QuizMaster.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.RegisterSettings(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>((_, options) => options
    .UseNpgsql(AppConfiguration.ConnectionString), optionsLifetime: ServiceLifetime.Scoped);

builder.Services.AddHealthChecks();

var app = builder.Build();
app.MapHealthChecks("health");

await app.SeedAppDbContext();

app.Run();
