using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingRestFullApi.src.DTOs.Review;
using TrainingRestFullApi.src.Interfaces;

namespace TrainingRestFullApi.src.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController(IReview reviewService, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {

        private readonly IReview _reviewService = reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpPost("{movieGuid}/movie")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Create(Guid movieGuid, ReviewDTO reviewDTO)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                Guid userGuid = Guid.Parse(httpContext!.Items["Sub"]!.ToString()!);

                var response = await _reviewService.Create(movieGuid, userGuid, reviewDTO);
                return StatusCode(response.Flag, response.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/comment")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Update(Guid id, ReviewDTO reviewDTO)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                Guid userGuid = Guid.Parse(httpContext!.Items["Sub"]!.ToString()!);
                var response = await _reviewService.Update(id, userGuid, reviewDTO);
                return StatusCode(response.Flag, response.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete("{id}/delete")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                Guid userGuid = Guid.Parse(httpContext!.Items["Sub"]!.ToString()!);
                var response = await _reviewService.Delete(id, userGuid);
                return StatusCode(response.Flag, response.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
