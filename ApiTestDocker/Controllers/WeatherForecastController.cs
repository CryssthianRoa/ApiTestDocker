using Microsoft.AspNetCore.Mvc;

namespace ApiTestDocker.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        LibraryTest.Response response = new LibraryTest.Response();
        string resp = response.GetResponse("Is my response 22222.");

        string customeName = response.GetAll("server=127.0.0.1;port=3306;database=GenieDev;uid=docker;password=dM57Me4KSb");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = resp//Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

