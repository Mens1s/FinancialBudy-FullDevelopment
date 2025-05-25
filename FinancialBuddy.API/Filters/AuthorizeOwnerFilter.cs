using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FinancialBuddy.API.Filters
{
    public class AuthorizeOwnerFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;

            if(!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // admin istediğini yapar
            var userRole = user.FindFirstValue(ClaimTypes.Role);
            if(userRole == "Admin")
            {
                await next();
                return;
            }

            // kullanıcı kendini ilgilendiren işlemleri yapar
            if (context.ActionArguments.TryGetValue("id", out var routeIdObj))
            {
                var routeId = routeIdObj.ToString();
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                if (routeId != userId)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Missing Id parameter in route.");
                return;
            }

            await next();
        }
    }
}
