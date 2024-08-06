using Intl.Realty.Firm.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DENR_FAPIS.Helper
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _dbContext;

        public DynamicPermissionAuthorizationHandler(IAuthorizationService authorizationService, ApplicationDbContext dbContext)
        {
            _authorizationService = authorizationService;
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicPermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Retrieve user's roles and permissions from the database
            //var userRoles2 = await _dbContext.Roles
            //    .Where(ur => ur.Id == Convert.ToInt32(userId))
            //    .Select(ur => ur.Id).FirstOrDefault();

            var userRole = await _dbContext.Users.Select(o => o.RoleId).FirstOrDefaultAsync();



            var userPermissions = _dbContext.RolePermissions
                .Where(rp => rp.RoleId == userRole).Select(o => o.Roles.Description);



            // Build the authorization policy dynamically based on user's permissions
            foreach (var permission in userPermissions)
            {
                var result = await _authorizationService.AuthorizeAsync(context.User, null, new ClaimRequirement(permission));
                if (result.Succeeded)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }
    }

    public class ClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public ClaimRequirement(string claimValue, string claimType = ClaimTypes.Role)
        {
            ClaimValue = claimValue;
            ClaimType = claimType;
        }
    }

}
