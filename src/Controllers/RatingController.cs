using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingRestFullApi.src.DTOs.Rating;
using TrainingRestFullApi.src.Interfaces;

namespace TrainingRestFullApi.src.Controllers
{
    [Route("v1/rating")]
    [ApiController]
    public class RatingController(IRating ratingController, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly IRating _ratingController = ratingController;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpPost("{movieGuid}/like")]
        [Authorize( Policy = "User")]
        public async Task<IActionResult> LikeMovie(Guid movieGuid, RatingDTO ratingDTO) 
        {
            var httpContext = _httpContextAccessor.HttpContext;
            Guid userGuid = Guid.Parse(httpContext!.Items["Sub"]!.ToString()!);
            var response = await _ratingController.LikeMovie(movieGuid, userGuid, ratingDTO);
            return StatusCode(response.Flag, response.Message);
        }
        [HttpPost("{movieGuid}/score")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> ScoreMovie(Guid movieGuid, RatingDTO ratingDTO)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            Guid userGuid = Guid.Parse(httpContext!.Items["Sub"]!.ToString()!);
            var response = await _ratingController.ScoreMovie(movieGuid, userGuid, ratingDTO);
            return StatusCode(response.Flag, response.Message);
        }
        [HttpPost("{movieGuid}/rating")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> RatingMovie(Guid movieGuid, RatingDTO ratingDTO)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            Guid userGuid = Guid.Parse(httpContext!.Items["Sub"]!.ToString()!);
            var response = await _ratingController.RatingMovie(movieGuid, userGuid, ratingDTO);
            return StatusCode(response.Flag, response.Message);
        }

    }
}
