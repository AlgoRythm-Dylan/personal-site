using System.Security.Claims;

namespace Web.Lib
{
    public static class RazorHelpers
    {
        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {
            return user.Identity?.IsAuthenticated ?? false;
        }
    }
}
