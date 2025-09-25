using EletronicPoint.Application.DTOs.Auth;
using EletronicPoint.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPoint.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.AuthenticateAsync(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
