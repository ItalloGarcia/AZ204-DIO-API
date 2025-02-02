using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APITempoDIO.Controllers
{
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
        public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var forecasts = Enumerable.Range(1, pageSize).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(forecasts);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("Forecast is null.");
            }

            // Simulate saving the forecast
            return CreatedAtAction(nameof(Get), new { id = 1 }, forecast);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Simulate deleting the forecast
            return NoContent();
        }
    }

    public class WeatherForecast
    {
        [Required]
        public DateOnly Date { get; set; }

        [Range(-20, 55)]
        public int TemperatureC { get; set; }

        [StringLength(20)]
        public string? Summary { get; set; }
    }
}