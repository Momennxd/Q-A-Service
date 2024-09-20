using Core_Layer.AppDbContext;
using DataAccess_Layer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Layer.Authorization
{
    public class PermissionBasedAuthorizationFilters : IAsyncAuthorizationFilter
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

            

            using (AppDbContext EF_Core_Context = await clsService.contextFactory!.CreateDbContextAsync())
            {

                // logic perform permissions proces
            }
        }
    }
}
