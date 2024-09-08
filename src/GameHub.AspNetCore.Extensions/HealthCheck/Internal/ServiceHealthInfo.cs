using System.Text.Json.Serialization;

namespace GameHub.AspNetCore.Extensions.HealthCheck.Internal;

internal record ServiceHealthInfo(
    string Name,
    string Message,
    [property: JsonIgnore] ServiceHealthStatus HealthStatus)
{
    public string Status { get; } = HealthStatus.ToString();
}
