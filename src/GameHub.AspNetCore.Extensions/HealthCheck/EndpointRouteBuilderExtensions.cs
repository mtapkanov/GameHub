using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GameHub.AspNetCore.Extensions.HealthCheck;

/// <summary>
///     Provides extension methods for Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
///     to add health checks.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    ///     Adds a health checks endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
    ///     with the conventional template and options.
    /// </summary>
    /// <returns>A convention routes for the health checks endpoint.</returns>
    public static IEndpointConventionBuilder MapHealthChecks(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = 200,
                [HealthStatus.Degraded] = 200,
                [HealthStatus.Unhealthy] = 200
            },
            ResponseWriter = HealthCheckResponseWriter.WriteResponse
        });
}
