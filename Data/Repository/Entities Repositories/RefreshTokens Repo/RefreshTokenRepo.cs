using Data.DatabaseContext;
using Data.models.RefreshTokens;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repository.Entities_Repositories.RefreshTokens_Repo
{
    public class RefreshTokenRepo : Repository<RefreshToken>, IRefreshTokenRepo
    {
        AppDbContext _context;
        public RefreshTokenRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<EntityEntry> AddItemAsync(RefreshToken Item)
        {
            var oldTokens = await _context.RefreshTokens
                .Where(t => t.UserId == Item.UserId && t.RevokedOn == null && t.Expiration > DateTime.UtcNow)
                .ToListAsync();

            foreach (var token in oldTokens)
            {
                await RevokeTokenAsync(token);
            }

            return await base.AddItemAsync(Item);
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
