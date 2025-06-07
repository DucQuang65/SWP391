using Hien_mau.Dto;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.RegisterAsync(request);
            if (user == null)
                return BadRequest("Email already exists or invalid IdToken.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> Login(UserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (token, idToken) = await _authService.LoginAsync(request);
            if (token == null || idToken == null)
                return BadRequest("Invalid IdToken or user not registered");
            return Ok(new { Token = token, IdToken = idToken });
        }
        
    }
}
