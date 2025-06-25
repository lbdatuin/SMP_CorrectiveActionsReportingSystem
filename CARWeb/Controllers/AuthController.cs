using CARWeb.Server.Services.AuthService;
using CARWeb.Shared.DTOs.AuthDTO;
using Microsoft.AspNetCore.Mvc;
using CARWeb.Response;

namespace CARWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthController(IAuthService authService, IHttpContextAccessor contextAccessor)
        {
            _authService = authService;
            _contextAccessor = contextAccessor;
        }


        [HttpGet("get-single-user")]
        public async Task<ActionResult<EditProfileDTO>> GetSingleUser()
        {
            var response = await _authService.GetSingleUser();
            if (response is null)
                return NotFound("User not Found");

            return Ok(response);
        }

        [HttpGet("single-user-avatar")]
        public async Task<ActionResult<string>> GetSingleUserAvater()
        {
            var result = await _authService.GetSingleUserAvatar();

            return Ok(result);
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<int>> UpdateUser([FromQuery] Guid userId, [FromBody] EditUserDTO request)
        {
            int status = await _authService.UpdateUser(userId, request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpPost("register")]
        public async Task<ActionResult<int>> Register(RegisterDTO request)
        {
            int response = await _authService.Register(request);
            if (response == 0) return BadRequest("Invalid Credentials");

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            LoginResponse response = await _authService.Login(request);
            if (string.IsNullOrEmpty(response.AccessToken)) return BadRequest("Invalid Credential");

            Response.Cookies.Append("refreshToken", response.RefreshToken, response.CookieOptions);

            return Ok(response.AccessToken);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> GenRefreshToken()
        {
            string? refToken = Request.Cookies["refreshToken"];

            var response = await _authService.ReRefreshToken(refToken);
            if (response == null) return BadRequest("");
            if (string.IsNullOrEmpty(response.AccessToken)) return BadRequest("");

            Response.Cookies.Append("refreshToken", response.RefreshToken, response.CookieOptions);

            return Ok(response.AccessToken);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<string>> Logout()
        {
            LoginResponse response = _authService.Logout();
            Response.Cookies.Append("refreshToken", response.RefreshToken, response.CookieOptions);
            return Ok(response.AccessToken);
        }

        [HttpPut("change-pass")]
        public async Task<IActionResult> ChangePassword(ChangePassDTO request)
        {
            int response = await _authService.ChangePassword(request);

            if (response == 0) return BadRequest(0);

            return Ok(1);
        }

        [HttpPost("forgot-pass")]
        public async Task<IActionResult> ForgotPass(ForgotPasswordDTO request)
        {
            var response = await _authService.ForgotPass(request);
            if (response == 0) return BadRequest();
            return Ok(response);
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode(VerifyCodeDTO request)
        {
            var response = await _authService.VerifyCode(request);
            if (response == 0) return BadRequest();
            return Ok(response);
        }

    }
}
