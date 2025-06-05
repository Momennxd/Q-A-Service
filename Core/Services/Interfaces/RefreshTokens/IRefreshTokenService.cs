using Core.DTOs.RefreshTokens;
using Data.models.RefreshTokens;
using static Core.DTOs.RefreshTokens.RefreshTokenDTOs;

namespace Core.Services.Interfaces.RefreshTokens
{
    public interface IRefreshTokenService
    {

        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeTokenAsync(RefreshToken token);
        Task<RefreshToken> GenerateRefreshTokenAsync(int userId);
        Task<RefreshTokenDTOs.LoginResponseDto> GenerateTokensForUserAsync(int userId);
        Task<TokenResponseDto> RefreshTokensAsync(string oldToken);
        Task<bool> LogoutAsync(string refreshToken);
    }
}
