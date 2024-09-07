using Microsoft.EntityFrameworkCore;

namespace GameHub.QuizMaster.Infrastructure.Database;

public static class AppDbContextSeed
{
    public static async Task SeedAppDbContext(this IHost host)
    {
        await using var scope = host.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "The process seeding database is failed.");
            throw;
        }
    }
}
