using System.Security.Claims;
using Hien_mau.Dto;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Interface;

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
        public async Task<ActionResult<Users>> Register(LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.RegisterAsync(request);
            if (user == null)
                return BadRequest("Email already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(request);            
            if (result == null)
            {
                return BadRequest("Invalid username or password");
            }
            if (result == "Account is banned")
                return Unauthorized();

            return Ok(result);
        }

        [HttpPatch("change-password/{id}")]
        public async Task<IActionResult> PatchPassword(int id, [FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _authService.ChangePasswordAsync(id, dto);
            if (!success)
                return BadRequest("Mật khẩu hiện tại không đúng hoặc người dùng không tồn tại.");

            return Ok("Mật khẩu đã được cập nhật thành công.");
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/api/Auth/google-callback"
            };
            //var redirectUrl = Url.Action(nameof(GoogleResponse), "Auth", null, Request.Scheme);
            //var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string frontendUrl = "http://localhost:5173";

            if (!result.Succeeded)
                //return BadRequest("Google authentication failed");
                return Redirect($"{frontendUrl}/signin-google?error=auth_failed&error_description=Google authentication failed");

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email))
                //return BadRequest("Email not received from Google");
                return Redirect($"{frontendUrl}/signin-google?error=missing_email&error_description=Email not received from Google");

            var user = await _authService.GetUserByEmailAsync(email);

            // Kiểm tra NULL trước khi sử dụng
            if (user == null)
            {
                user = await _authService.CreateUserFromGoogleAsync(email, name ?? "No Name");

                if (user == null) // Kiểm tra lại sau khi tạo
                    //return StatusCode(500, "Failed to create user");
                    return Redirect($"{frontendUrl}/signin-google?error=user_creation_failed&error_description=Failed to create user");
            }

            if (user.Status == 0)
            {
                //return BadRequest("Account is banned");
                return Redirect($"{frontendUrl}/signin-google?error=account_banned&error_description=Account is banned");
            }

            var token = _authService.CreateToken(user);

            //return Ok(token);
            return Redirect($"{frontendUrl}/signin-google?token={token}");
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("Logged out");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var user = await _authService.GetUserByEmailAsync(email);
            if (user == null)
                return BadRequest("Email not found");

            if (user.Status == 0)
                return BadRequest("User is inactive or banned");

            var token = await _authService.GeneratePasswordResetTokenAsync(user.Email);
            if (string.IsNullOrEmpty(token))
                return BadRequest("Email not found or user is inactive");           

            return Ok("Reset password email sent.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ResetPasswordAsync(dto.Token, dto.NewPassword);

            if (!result)
                return BadRequest("Invalid or expired token");

            return Ok("Password has been reset successfully.");
        }

    }
}
