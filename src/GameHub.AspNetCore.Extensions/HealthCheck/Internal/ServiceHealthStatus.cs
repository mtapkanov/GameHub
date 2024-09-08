namespace GameHub.AspNetCore.Extensions.HealthCheck.Internal;

internal enum ServiceHealthStatus
{
    Ok = 0,
    Degraded = 1,
    Fail = 2
}
