namespace GameHub.Domain;

public record GameSession
{
    public required long Id { get; init; }

    public required DateTime StartTime { get; init; }

    public DateTime? EndTime { get; init; }
}
