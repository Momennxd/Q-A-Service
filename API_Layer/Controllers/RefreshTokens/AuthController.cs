using Core.Services.Interfaces.RefreshTokens;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.RefreshTokens.RefreshTokenDTOs;

namespace API_Layer.Controllers.RefreshTokens
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        public AuthController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenDTO request)
        {
            
            var newTokens = await _refreshTokenService.RefreshTokensAsync(request.Token);
            if (newTokens == null)
                return NotFound(new { message = "Invalid or expired refresh token." });
            return Ok(newTokens);
            
        }

    }
}
