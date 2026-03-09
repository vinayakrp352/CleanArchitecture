using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CleanArchitecture.Web.Endpoints;

public class HealthCheck : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetHealth);
    }

    [EndpointName(nameof(GetHealth))]
    [EndpointSummary("Get Health Status")]
    [EndpointDescription("Returns the health status and version of the application.")]
    public async Task<Ok<HealthCheckResponse>> GetHealth(HealthCheckService healthCheckService)
    {
        var version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? "unknown";

        var report = await healthCheckService.CheckHealthAsync();

        return TypedResults.Ok(new HealthCheckResponse(version, report.Status.ToString()));
    }
}

public record HealthCheckResponse(string Version, string Status);
