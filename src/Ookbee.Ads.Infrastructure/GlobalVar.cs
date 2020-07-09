using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Infrastructure
{
    public static class GlobalVar
    {
        private static IServiceProvider services = null;
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                    throw new Exception("Can't set once a value has already been set.");
                services = value;
            }
        }

        private static AppSettings appSettings = null;
        public static AppSettings AppSettings
        {
            get
            {
                if (appSettings == null)
                    appSettings = (services.GetService(typeof(IOptions<AppSettings>)) as IOptions<AppSettings>)?.Value;

                return appSettings;
            }
        }

        private static IWebHostEnvironment hostingEnvironment = null;
        public static IWebHostEnvironment HostingEnvironment
        {
            get
            {
                if (hostingEnvironment == null)
                    hostingEnvironment = services.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;

                return hostingEnvironment;
            }
        }

        private static HttpContext httpContext = null;
        public static HttpContext HttpContext
        {
            get
            {
                if (hostingEnvironment == null)
                    httpContext = (services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor)?.HttpContext;

                return httpContext;
            }
        }
    }
}
