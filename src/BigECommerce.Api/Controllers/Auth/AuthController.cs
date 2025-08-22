using BigECommerce.Auth.Dtos;
using BigECommerce.Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigECommerce.Api.Controllers.Auth
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;

        public AuthController(
            IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        /// <summary>
        /// Obter o token de acesso para autenticação.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var token = await _jwtService.GenerateTokenAsync(loginRequestDto.Email, loginRequestDto.Password);

                return Ok(new { Token = token });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
