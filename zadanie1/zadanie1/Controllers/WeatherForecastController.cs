using Microsoft.AspNetCore.Mvc;

namespace zadanie1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс нельзя");
            }

            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Нельзя удалить отрицательное, либо то, что больше того, что в списке, пнятненько?");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс нельзя, окей?");
            }

            return Ok(Summaries[index]);
        }
        [HttpGet("find-by-name")]
        public IActionResult GetByName([FromQuery] string name)
        {
            //здесь был обработчик на случай пустого поля
            //но я его вырезал, т.к. выяснилось, что здесь уже есть обработка отсуствия необходимых параметров
            //поэтому он оказался не нужен

            int count = Summaries.Count(Summaries => Summaries.Contains(name));
            return Ok(count);
        }
        [HttpGet] //ДОДЕЛАТЬ
        public IActionResult GetAll(int? sortStrategy)
        {
            return Ok(sortStrategy);
        }
    }
}
