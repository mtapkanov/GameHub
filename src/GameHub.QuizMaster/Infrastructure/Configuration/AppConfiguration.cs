namespace GameHub.QuizMaster.Infrastructure.Configuration;

public static class AppConfiguration
{
    public static string ConnectionString { get; set; } = null!;

    public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")!;

        ConnectionString = connectionString;
    }
}
