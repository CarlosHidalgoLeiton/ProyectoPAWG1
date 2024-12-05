using System.Security.Claims;

using System;
using System.Linq;
using System.Security.Claims;

namespace PAWG1.Architecture.Helpers;
public static class UserHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns>id of the logged in user</returns>
    public static int? GetAuthenticatedUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns>role of the logged in user</returns>

    public static string GetUserRole(ClaimsPrincipal user)
    {
        var roleClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        return roleClaim?.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns>username of the logged in user </returns>
    public static string GetUserName(ClaimsPrincipal user)
    {
        var nameClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        return nameClaim?.Value;
    }
}
