using BLL.Interfaces;
using System.Security.Claims;

namespace Learning_System.EndPoint.Utility
{
    public static class ClaimUtility
    {
        public static int GetUserId(ClaimsPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }
    }
}
