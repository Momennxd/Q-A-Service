using Data.DatabaseContext;
using Data.models.RefreshTokens;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Entities_Repositories.RefreshTokens_Repo
{
    public class RefreshTokenRepo : Repository<RefreshToken>, IRefreshTokenRepo
    {
        AppDbContext _context;
        public RefreshTokenRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public Task RevokeTokenAsync(RefreshToken token)
        {
            token.RevokedOn = DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}
