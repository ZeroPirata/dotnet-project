using Microsoft.AspNetCore.Mvc;
using TrainingRestFullApi.src.DTOs;
using TrainingRestFullApi.src.DTOs.User;
using TrainingRestFullApi.src.Interfaces;
using TrainingRestFullApi.src.Service;

namespace TrainingRestFullApi.src.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UserController(IUserAccount userAccountService) : ControllerBase
    {
        private readonly IUserAccount _userAccountService = userAccountService;

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(UserDTO userDTO)
        {
            var response = await _userAccountService.CreateAccount(userDTO);
            return StatusCode(response.Flag, response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            var response = await _userAccountService.LoginAccount(loginDTO);
            return StatusCode(response.Flag, new { Token = response.Token ?? "", Message = response.Message! });
        }
    }

}
