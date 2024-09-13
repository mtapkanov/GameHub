using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace GameHub.AspNetCore.Extensions.ServiceInfo;

/// <summary>
///     Provides extension methods for Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
///     to add getting information about service.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    private static readonly AssemblyName? _fullName = Assembly.GetEntryAssembly()?.GetName();

    /// <summary>
    ///     Adds a service information endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
    ///     with the conventional template and options.
    /// </summary>
    /// <returns>A convention routes for the service information endpoint.</returns>
    public static IEndpointConventionBuilder MapServiceInfo(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.Map("/id", async context =>
        {
            var content = _fullName is not null
                ? $"{_fullName.Name} {_fullName.Version}"
                : "Service information cannot be displayed";

            await context.Response.WriteAsync(content);
        });
    }
}
