using Microsoft.AspNetCore.Http;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Infrastructure
{
    public static class GlobalVar
    {
        public static AppSettings AppSettings
            => ConfigurationHelper.GetAppSettings<AppSettings>();

        public static string EnvironmentVariable 
            => ConfigurationHelper.GetEnvironmentVariable();

        public static HttpContext HttpContext 
            => HttpContextHelper.Current;
    }
}
