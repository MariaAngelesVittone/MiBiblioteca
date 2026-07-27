using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Services;

namespace MiBiblioteca.WebAPI.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var user = await _authService.RegisterAsync(dto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token is null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            return Ok(token);
        }

        // Endpoint protegido solo para demostrar como se leen las claims
        // del token ya validado. ASP.NET Core arma el objeto "User" (de tipo
        // ClaimsPrincipal) automaticamente a partir del JWT antes de que
        // el endpoint se ejecute.
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var userId = User.FindFirst("sub")?.Value;
            var username = User.FindFirst("username")?.Value;

            return Ok(new { userId, username });
        }
    }
}
