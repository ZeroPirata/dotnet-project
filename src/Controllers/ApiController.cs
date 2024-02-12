using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingRestFullApi.src.Middleware;
namespace TrainingRestFullApi.src.Controllers
{
    [Route("v1/api")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public ApiController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet("version")]
        public IActionResult Version()
        {
            var apiControllSection = _config.GetSection("ApiControll");
            string? version = apiControllSection["version"];
            string? date = apiControllSection["date"];

            dynamic response = new { Version = version, Date = date };
            return Ok(response);
        }

        [HttpGet("database-check")]
        public IActionResult DataBaseCheck()
        {
            try
            {
                var testDatabase = _context.Database.CanConnect();
                if (testDatabase is false) return BadRequest("Banco esta off-line. :(");
                return Ok("Banco esta ativo. :)");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor. {ex.Message}");
            }
        }

    }
}
