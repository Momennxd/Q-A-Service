using AutoMapper;
using Core.DTOs.RefreshTokens;
using Core.Services.Interfaces.RefreshTokens;
using Core.Unit_Of_Work;
using Data.models.RefreshTokens;
using Data.Repository.Entities_Repositories.RefreshTokens_Repo;
using static Core.DTOs.RefreshTokens.RefreshTokenDTOs;

namespace Core.Services.Concrete.RefreshTokens
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IRefreshTokenRepo, RefreshToken> _uowRefreshToken;
        private readonly ITokenService _tokenService;


        public RefreshTokenService(IMapper mapper, IUnitOfWork<IRefreshTokenRepo, RefreshToken> uowRefreshToken, ITokenService tokenService)
        {
            _mapper = mapper;
            _uowRefreshToken = uowRefreshToken;
            _tokenService = tokenService;
        }

        public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
        {
            var repo = _uowRefreshToken.EntityRepo;

            var refreshToken = new RefreshToken
            {
                Token = GenerateSecureToken(),
                Expiration = DateTime.UtcNow.AddMonths(2),
                CreatedOn = DateTime.UtcNow,
                UserId = userId
            };

            await repo.AddItemAsync(refreshToken);
            await _uowRefreshToken.CompleteAsync();

            return refreshToken;
        }
        private string GenerateSecureToken()
        {
            var randomBytes = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            var repo = _uowRefreshToken.EntityRepo;
            return await repo.GetByTokenAsync(token);
        }

        public async Task RevokeTokenAsync(RefreshToken token)
        {
            token.RevokedOn = DateTime.UtcNow;
            var repo = _uowRefreshToken.EntityRepo;
            await _uowRefreshToken.CompleteAsync();
        }

        public async Task<RefreshTokenDTOs.LoginResponseDto> GenerateTokensForUserAsync(int userId)
        {
            var accessToken = _tokenService.CreateToken(userId);
            var refreshToken = await GenerateRefreshTokenAsync(userId);

            return new RefreshTokenDTOs.LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }


        public async Task<TokenResponseDto> RefreshTokensAsync(string oldToken)
        {
            var refreshToken = await GetByTokenAsync(oldToken);

            if (refreshToken == null || !refreshToken.IsActive)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            await RevokeTokenAsync(refreshToken);

            var newRefreshToken = await GenerateRefreshTokenAsync(refreshToken.UserId);

            var newAccessToken = _tokenService.CreateToken(refreshToken.UserId);

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var repo = _uowRefreshToken.EntityRepo;
            var token = await repo.GetByTokenAsync(refreshToken);

            if (token == null || !token.IsActive)
                return false;

            token.RevokedOn = DateTime.UtcNow;
            await _uowRefreshToken.CompleteAsync();
            return true;
        }


    }
}

