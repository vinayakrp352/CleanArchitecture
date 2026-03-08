using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using NUnit.Framework;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.WeatherForecasts;

public class GetWeatherForecastsQueryHandlerTests
{
    [TestCase(-20, "Freezing")]
    [TestCase(-10, "Freezing")]
    [TestCase(-9,  "Bracing")]
    [TestCase(0,   "Bracing")]
    [TestCase(1,   "Chilly")]
    [TestCase(5,   "Chilly")]
    [TestCase(6,   "Cool")]
    [TestCase(10,  "Cool")]
    [TestCase(11,  "Mild")]
    [TestCase(20,  "Mild")]
    [TestCase(21,  "Warm")]
    [TestCase(25,  "Warm")]
    [TestCase(26,  "Balmy")]
    [TestCase(30,  "Balmy")]
    [TestCase(31,  "Hot")]
    [TestCase(35,  "Hot")]
    [TestCase(36,  "Sweltering")]
    [TestCase(45,  "Sweltering")]
    [TestCase(46,  "Scorching")]
    [TestCase(54,  "Scorching")]
    public void GetSummary_ShouldReturnExpectedDescription_ForTemperature(int temperatureC, string expectedSummary)
    {
        var summary = GetWeatherForecastsQueryHandler.GetSummary(temperatureC);

        summary.ShouldBe(expectedSummary);
    }

    [Test]
    public async Task Handle_ShouldReturnFiveForecasts()
    {
        var handler = new GetWeatherForecastsQueryHandler();

        var result = (await handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None)).ToList();

        result.Count.ShouldBe(5);
    }

    [Test]
    public async Task Handle_ShouldReturnForecasts_WithSummariesMatchingTemperature()
    {
        var handler = new GetWeatherForecastsQueryHandler();

        var result = (await handler.Handle(new GetWeatherForecastsQuery(), CancellationToken.None)).ToList();

        foreach (var forecast in result)
        {
            var expectedSummary = GetWeatherForecastsQueryHandler.GetSummary(forecast.TemperatureC);
            forecast.Summary.ShouldBe(expectedSummary);
        }
    }
}
