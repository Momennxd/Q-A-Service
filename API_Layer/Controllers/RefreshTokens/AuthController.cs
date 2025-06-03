using Core.Services.Interfaces.RefreshTokens;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.RefreshTokens.RefreshTokenDTOs;

namespace API_Layer.Controllers.RefreshTokens
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IRefreshTokenService _refreshTokenService;
        public AuthController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenDTO request)
        {
            if (string.IsNullOrEmpty(request.Token))
                return BadRequest(new { message = "Refresh token is required" });

            try
            {
                var response = await _refreshTokenService.RefreshTokensAsync(request.Token);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}
