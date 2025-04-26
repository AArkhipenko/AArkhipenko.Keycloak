using AArkhipenko.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AArkhipenko.Keycloak.Test.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : LoggingControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
        : base(logger)
    {
    }

    [HttpGet("for-user")]
    [Authorize("UserRole")]
    public IEnumerable<WeatherForecast> GetForUser()
    {
        using (base.BeginLoggingScope())
        {
            base.Logger.LogInformation("Get");
            return GetArray();
        }
    }

    [HttpGet("for-admin")]
    [Authorize("AdminRole")]
    public IEnumerable<WeatherForecast> GetForAdmin()
    {
        using (base.BeginLoggingScope())
        {
            base.Logger.LogInformation("Get");
            return GetArray();
        }
    }

    private IEnumerable<WeatherForecast> GetArray()
    {
        using (base.BeginLoggingScope())
        {
            base.Logger.LogInformation("GetArray");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
