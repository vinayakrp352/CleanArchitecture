using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using NUnit.Framework;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.WeatherForecasts;

public class GetWeatherForecastsQueryTests
{
    [Test]
    public async Task Handle_ShouldReturnFiveForecasts()
    {
        var handler = new GetWeatherForecastsQueryHandler();

        var result = await handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        result.Count().ShouldBe(5);
    }

    [Test]
    public async Task Handle_ShouldReturnForecastsWithFutureDates()
    {
        var handler = new GetWeatherForecastsQueryHandler();
        var before = DateTime.Now;

        var result = await handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        foreach (var forecast in result)
        {
            forecast.Date.ShouldBeGreaterThan(before);
        }
    }

    [Test]
    public async Task Handle_ShouldReturnForecastsWithNonEmptySummaries()
    {
        var handler = new GetWeatherForecastsQueryHandler();

        var result = await handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None);

        foreach (var forecast in result)
        {
            forecast.Summary.ShouldNotBeNullOrEmpty();
        }
    }

    [Test]
    [TestCase(-20, "Freezing")]
    [TestCase(-1, "Freezing")]
    [TestCase(0, "Bracing")]
    [TestCase(4, "Bracing")]
    [TestCase(5, "Chilly")]
    [TestCase(9, "Chilly")]
    [TestCase(10, "Cool")]
    [TestCase(14, "Cool")]
    [TestCase(15, "Mild")]
    [TestCase(19, "Mild")]
    [TestCase(20, "Warm")]
    [TestCase(24, "Warm")]
    [TestCase(25, "Balmy")]
    [TestCase(29, "Balmy")]
    [TestCase(30, "Hot")]
    [TestCase(39, "Hot")]
    [TestCase(40, "Sweltering")]
    [TestCase(49, "Sweltering")]
    [TestCase(50, "Scorching")]
    [TestCase(55, "Scorching")]
    public void WeatherForecast_Summary_ShouldReflectTemperature(int temperatureC, string expectedSummary)
    {
        var forecast = new WeatherForecast { TemperatureC = temperatureC };

        forecast.Summary.ShouldBe(expectedSummary);
    }
}
