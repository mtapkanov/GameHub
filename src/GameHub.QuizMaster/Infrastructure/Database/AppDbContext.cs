using GameHub.Domain;
using Microsoft.EntityFrameworkCore;

namespace GameHub.QuizMaster.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<GameSession> GameSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GameSession>(entity =>
        {
            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();
        });
    }
}
