using Data.models.RefreshTokens;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.RefreshTokens_Repo
{
    public interface IRefreshTokenRepo : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeTokenAsync(RefreshToken token);
    }
}
