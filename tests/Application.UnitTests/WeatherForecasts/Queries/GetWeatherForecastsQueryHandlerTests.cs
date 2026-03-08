using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using NUnit.Framework;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.WeatherForecasts.Queries;

public class GetWeatherForecastsQueryHandlerTests
{
    private static readonly string[] ValidSummaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private GetWeatherForecastsQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _handler = new GetWeatherForecastsQueryHandler();
    }

    [Test]
    public async Task Handle_ShouldReturnFiveForecasts()
    {
        var result = await _handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        result.Count().ShouldBe(5);
    }

    [Test]
    public async Task Handle_ShouldReturnForecastsWithValidSummaries()
    {
        var result = await _handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        foreach (var forecast in result)
        {
            forecast.Summary.ShouldNotBeNullOrWhiteSpace();
            ValidSummaries.ShouldContain(forecast.Summary);
        }
    }

    [Test]
    public async Task Handle_ShouldReturnForecastsWithFutureDates()
    {
        var today = DateTime.Today;

        var result = await _handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        foreach (var forecast in result)
        {
            forecast.Date.Date.ShouldBeGreaterThan(today);
        }
    }

    [Test]
    public async Task Handle_ShouldReturnForecastsWithTemperaturesInExpectedRange()
    {
        var result = await _handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        foreach (var forecast in result)
        {
            forecast.TemperatureC.ShouldBeInRange(-20, 55);
        }
    }
}
