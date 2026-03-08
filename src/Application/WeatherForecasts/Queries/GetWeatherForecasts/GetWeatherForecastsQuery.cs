namespace CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>;

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecast>>
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var rng = new Random();

        return Enumerable.Range(1, 5).Select(index =>
        {
            var temperatureC = rng.Next(-20, 55);
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = temperatureC,
                Summary = GetSummary(temperatureC)
            };
        }).ToList();
    }

    public static string GetSummary(int temperatureC) => temperatureC switch
    {
        <= -10 => "Freezing",
        <= 0   => "Bracing",
        <= 5   => "Chilly",
        <= 10  => "Cool",
        <= 20  => "Mild",
        <= 25  => "Warm",
        <= 30  => "Balmy",
        <= 35  => "Hot",
        <= 45  => "Sweltering",
        _      => "Scorching"
    };
}
