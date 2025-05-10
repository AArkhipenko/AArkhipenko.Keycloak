using AArkhipenko.Core.Exceptions;
using AArkhipenko.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            base.Logger.LogInformation("GetForUser");
            return GetArray();
        }
    }

    [HttpGet("for-admin")]
    [Authorize("AdminRole")]
    public IEnumerable<WeatherForecast> GetForAdmin()
    {
        using (base.BeginLoggingScope())
        {
            base.Logger.LogInformation("GetForAdmin");
            return GetArray();
        }
    }

    [HttpGet("user-id")]
    [Authorize]
    public string? GetUserId()
    {
        using (base.BeginLoggingScope())
        {
            base.Logger.LogInformation("GetUserId");
            if(!HttpContext.User.HasClaim(x => x.Type == Consts.KeycloakClaim.UserId))
            {
                throw new NotFoundException(Consts.KeycloakClaim.UserId);
            }

            return HttpContext.User.FindFirstValue(Consts.KeycloakClaim.UserId);
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
