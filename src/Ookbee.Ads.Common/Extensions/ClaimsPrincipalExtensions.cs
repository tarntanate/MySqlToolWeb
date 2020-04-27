using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Ookbee.Ads.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Claim Claim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.FindFirst(claimType);
        }

        public static string ClaimNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claim(ClaimTypes.NameIdentifier)?.Value;
        }

        public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        }

        public static long Id(this ClaimsPrincipal claimsPrincipal)
        {
            return long.TryParse(claimsPrincipal.ClaimNameIdentifier(), out var value) ? value : 0;
        }

        public static IEnumerable<T> Roles<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum
        {
            return claimsPrincipal.ClaimRoles().Select(value => (T)Enum.Parse(typeof(T), value)).ToList();
        }

        public static T RolesFlag<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum
        {
            var roles = claimsPrincipal.Roles<T>().Sum(value => Convert.ToInt64(value));

            return (T)Enum.Parse(typeof(T), roles.ToString(), true);
        }
    }
}
