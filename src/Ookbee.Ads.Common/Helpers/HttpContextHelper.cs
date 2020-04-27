using Microsoft.AspNetCore.Http;

namespace Ookbee.Ads.Common.Helpers
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor HttpContextAccessor { get; set; }

        public static void Configure(IHttpContextAccessor httpContextAccessor)
            => HttpContextAccessor = httpContextAccessor;

        public static HttpContext Current
            => HttpContextAccessor?.HttpContext;
    }
}
