using System.Security.Claims;

namespace API_Layer.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim == null ? throw new Exception("User ID claim not found") : int.Parse(userIdClaim.Value);
        }
    }
}
