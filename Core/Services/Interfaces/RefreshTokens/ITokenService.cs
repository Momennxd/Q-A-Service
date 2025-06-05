using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces.RefreshTokens
{
    public interface ITokenService
    {
        string CreateToken(int userId);
        int GetUserIdFromClaims(ClaimsPrincipal user);
    }
}
