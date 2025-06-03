using AutoMapper;
using Core.Services.Interfaces.RefreshTokens;
using Core.Unit_Of_Work;
using Data.models.RefreshTokens;
using Data.Repository.Entities_Repositories.RefreshTokens_Repo;

namespace Core.Services.Concrete.RefreshTokens
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IRefreshTokenRepo, RefreshToken> _uowRefreshToken;


        public RefreshTokenService(IMapper mapper, IUnitOfWork<IRefreshTokenRepo, RefreshToken> uowRefreshToken)
        {
            _mapper = mapper;
            _uowRefreshToken = uowRefreshToken;
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


    }
}
