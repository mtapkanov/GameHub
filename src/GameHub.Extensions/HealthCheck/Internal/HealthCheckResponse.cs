namespace GameHub.Extensions.HealthCheck.Internal;

internal record HealthCheckResponse
{
    public required ServiceHealthInfo Self { get; init; }
    public required IList<ServiceHealthInfo> Dependencies { get; init; }
}
