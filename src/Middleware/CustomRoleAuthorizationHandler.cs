using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TrainingRestFullApi.src.Records;

namespace TrainingRestFullApi.src.Middleware
{
    public class CustomRoleAuthorizationHandler : AuthorizationHandler<CustomRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext != null)
            {
                var userId = httpContext.Items["Jti"] as string;
                var userRoles = httpContext.Items["Roles"] as List<string>;

                if (!string.IsNullOrEmpty(userId) && userRoles != null && userRoles.Contains(requirement.Role))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }

    }
}
