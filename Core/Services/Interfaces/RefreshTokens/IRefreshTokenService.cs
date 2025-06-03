using Data.models.RefreshTokens;

namespace Core.Services.Interfaces.RefreshTokens
{
    public interface IRefreshTokenService
    {

        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeTokenAsync(RefreshToken token);
        Task<RefreshToken> GenerateRefreshTokenAsync(int userId);


    }
}
