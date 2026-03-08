using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Web.Endpoints;

public class HealthCheck : EndpointGroupBase
{
    private static readonly string _version =
        typeof(HealthCheck).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? typeof(HealthCheck).Assembly.GetName().Version?.ToString()
        ?? "unknown";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetHealth).AllowAnonymous();
    }

    [EndpointName(nameof(GetHealth))]
    [EndpointSummary("Get Health Status")]
    [EndpointDescription("Returns the health status and version of the application.")]
    public Ok<HealthCheckResponse> GetHealth()
    {
        return TypedResults.Ok(new HealthCheckResponse("Healthy", _version));
    }
}

public record HealthCheckResponse(string Status, string Version);
