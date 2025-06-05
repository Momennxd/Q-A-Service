using Data.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API_Layer.Authorization
{
    public class PermissionBasedAuthorizationFilters(AppDbContext appDbContext) : IAsyncAuthorizationFilter
    {


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attribute = (CheckPermissionAttribute?)context.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(Attribute => Attribute is CheckPermissionAttribute);

            if (attribute == null)
                return;


            var Claim = context.HttpContext.User.Identity as ClaimsIdentity;

            if (Claim == null || !Claim.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }
            if (!int.TryParse(Claim.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var user = await appDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            // Convert the user's permissions from byte to int
            int userPermissions = Convert.ToInt32(user.Permissions);

            if ((userPermissions & (int)attribute.Permission) != (int)attribute.Permission)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
