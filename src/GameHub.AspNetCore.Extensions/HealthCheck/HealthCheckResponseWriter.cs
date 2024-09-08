using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using GameHub.AspNetCore.Extensions.HealthCheck.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GameHub.AspNetCore.Extensions.HealthCheck;

/// <summary>
///     HealthCheck conventional response writer.
/// </summary>
public static class HealthCheckResponseWriter
{
    private static readonly string _entryAssemblyName = Assembly.GetEntryAssembly()?.GetName().Name!;

    private static readonly byte[] _emptyResponse = "{}"u8.ToArray();

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    /// <summary>
    ///     Writes conventional response to HttpContext response.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
    /// <param name="report">The <see cref="HealthReport"/>.</param>
    public static async Task WriteResponse(HttpContext httpContext, HealthReport? report)
    {
        if (report != null)
        {
            var dependencies = report.Entries.Select(x => GetServiceHealthInfo(x.Key, x.Value)).ToList();

            var healthStatus = dependencies.Count is 0 ? ServiceHealthStatus.Ok : dependencies.Max(x => x.HealthStatus);

            var healthInfo = new ServiceHealthInfo(
                Name: _entryAssemblyName,
                Message: FormatMessage(string.Empty, report.TotalDuration),
                HealthStatus: healthStatus);

            var healthCheckResponse = new HealthCheckResponse
            {
                Self = healthInfo,
                Dependencies = dependencies
            };

            httpContext.Response.ContentType = "application/json";
            await using MemoryStream responseStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(responseStream, healthCheckResponse, _jsonSerializerOptions);
            await httpContext.Response.Body.WriteAsync(responseStream.ToArray());
        }
        else
        {
            await httpContext.Response.Body.WriteAsync(_emptyResponse);
        }
    }

    private static ServiceHealthInfo GetServiceHealthInfo(string name, HealthReportEntry reportEntry)
    {
        return new ServiceHealthInfo(
            Name: name,
            HealthStatus: MapHealthStatus(reportEntry.Status),
            Message: FormatMessage(reportEntry.Description, reportEntry.Duration));
    }

    private static ServiceHealthStatus MapHealthStatus(HealthStatus healthStatus)
    {
        return healthStatus switch
        {
            HealthStatus.Healthy => ServiceHealthStatus.Ok,
            HealthStatus.Degraded => ServiceHealthStatus.Degraded,
            HealthStatus.Unhealthy => ServiceHealthStatus.Fail,
            _ => ServiceHealthStatus.Fail
        };
    }

    private static string FormatMessage(string? message, TimeSpan duration)
    {
        return $"{message} Time taken: {duration.TotalMilliseconds}ms".Trim();
    }
}
