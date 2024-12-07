using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PAWG1.Architecture.Helpers;

/// <summary>
/// A helper class providing utility methods for user-related operations, 
/// such as retrieving user information from claims and hashing passwords.
/// </summary>
public static class UserHelper
{
    /// <summary>
    /// Retrieves the authenticated user's ID from the claims in the provided ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal representing the authenticated user.</param>
    /// <returns>The authenticated user's ID as an integer, or null if it cannot be retrieved.</returns>  
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
    /// Retrieves the role of the authenticated user from the claims in the provided ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal representing the authenticated user.</param>
    /// <returns>The user's role as a string, or null if the role claim is not found.</returns>
    public static string GetUserRole(ClaimsPrincipal user)
    {
        var roleClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        return roleClaim?.Value;
    }

    /// <summary>
    /// Retrieves the name of the authenticated user from the claims in the provided ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal representing the authenticated user.</param>
    /// <returns>The user's name as a string, or null if the name claim is not found.</returns>
    public static string GetUserName(ClaimsPrincipal user)
    {
        var nameClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        return nameClaim?.Value;
    }

    /// <summary>
    /// Hashes a password using SHA256 and returns the hashed result as a hexadecimal string.
    /// </summary>
    /// <param name="password">The plain text password to be hashed.</param>
    /// <returns>The hashed password as a hexadecimal string.</returns>
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}
