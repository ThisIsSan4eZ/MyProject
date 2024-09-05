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
                return BadRequest("����� ������ ������");
            }

            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("������ ������� �������������, ���� ��, ��� ������ ����, ��� � ������, ����������?");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("����� ������ ������, ����?");
            }

            return Ok(Summaries[index]);
        }
        [HttpGet("find-by-name")]
        public IActionResult GetByName([FromQuery] string name)
        {
            //����� ��� ���������� �� ������ ������� ����
            //�� � ��� �������, �.�. ����������, ��� ����� ��� ���� ��������� ��������� ����������� ����������
            //������� �� �������� �� �����

            int count = Summaries.Count(Summaries => Summaries.Contains(name));
            return Ok(count);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }
            else if (sortStrategy == 1)
            {
                return Ok(Summaries.OrderBy(x => x));
            }
            else if (sortStrategy == -1)
            {
                return Ok(Summaries.OrderByDescending(x => x));
            }
            else
            {
                return BadRequest("��� ������, �������� �� �����");
            }
        }

    }
}
