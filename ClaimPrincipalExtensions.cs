using System.Security.Claims;

namespace RunWepApp_withIdentity_TeddySmith_Youtube;

public static class ClaimPrincipalExtensions
{
    public  static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier).Value;    
    }
}
