namespace CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public class WeatherForecast
{
    public DateTime Date { get; init; }

    public int TemperatureC { get; init; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary => TemperatureC switch
    {
        < 0 => "Freezing",
        < 5 => "Bracing",
        < 10 => "Chilly",
        < 15 => "Cool",
        < 20 => "Mild",
        < 25 => "Warm",
        < 30 => "Balmy",
        < 40 => "Hot",
        < 50 => "Sweltering",
        _ => "Scorching"
    };
}
