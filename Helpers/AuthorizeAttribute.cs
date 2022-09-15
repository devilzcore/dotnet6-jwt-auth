using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using dotnet6_jwt_auth.Entities;

namespace dotnet6_jwt_auth.Helpers
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class AuthorizeAttribute : Attribute, IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var user = (User)context.HttpContext.Items["User"];
      if (user == null)
      {
        context.Result = new JsonResult(new { message = "Unauthorized" })
        { StatusCode = StatusCodes.Status401Unauthorized };
      }
    }
  }
}