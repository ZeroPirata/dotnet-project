using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingRestFullApi.src.DTOs.Movie;
using TrainingRestFullApi.src.Interfaces;

namespace TrainingRestFullApi.src.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IMovie _movieController;

        public MovieController(IMovie movieController)
        {
            _movieController = movieController;
        }

        [HttpGet("/{guid}")]
        public async Task<IActionResult> ReadOne(Guid guid)
        {
            try
            {
                var response = await _movieController.ReadOne(guid);
                return StatusCode(response.Flag, new { response.Data, response.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to get a movie: {ex.Message}");
                throw;
            }
        }

        [HttpGet("all-movies")]
        public async Task<IActionResult> ReadMany()
        {
            try
            {
                var response = await _movieController.ReadMany();
                return StatusCode(response.Flag, new { response.Data, response.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to get all movies: {ex.Message}");
                throw;
            }
        }

        [HttpGet("get-by-crew")]
        public async Task<IActionResult> GetByCrew(string crew = "producer", string crewName = "Stacey Sher")
        {
            try
            {
                var response = await _movieController.GetByCrew(crew, crewName);
                return StatusCode(response.Flag, new { response.Data, response.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to get a movie: {ex.Message}");
                throw;
            }
        }

        [HttpPost("/")]
        public async Task<IActionResult> Create(MCreateDTO createDto)
        {
            try
            {
                var response = await _movieController.Create(createDto);
                return StatusCode(response.Flag, response.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to create a movie: {ex.Message}");
                throw;
            }
        }

        [HttpDelete("/{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                var response = await _movieController.Delete(guid);
                return StatusCode(response.Flag, response.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to get a movie: {ex.Message}");
                throw;
            }
        }

    }
}
