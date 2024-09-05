using GameHub.QuizMaster.Infrastructure.Configuration;
using GameHub.QuizMaster.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.RegisterSettings(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>((_, options) => options
    .UseNpgsql(AppConfiguration.ConnectionString), optionsLifetime: ServiceLifetime.Scoped);

var app = builder.Build();

await app.SeedAppDbContext();
app.Run();
