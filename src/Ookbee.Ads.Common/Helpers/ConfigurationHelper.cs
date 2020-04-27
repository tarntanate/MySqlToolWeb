using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Ookbee.Ads.Common.Helpers
{
    public static class ConfigurationHelper
    {
        private static object AppSettings { get; set; }
        private static string EnvironmentVariable { get; set; }
        private static IConfiguration Configuration { get; set; }

        public static T GetAppSettings<T>()
        {
            if (AppSettings == null)
            {
                var value = Activator.CreateInstance<T>();
                GetConfiguration().Bind("AppSettings", value);
                AppSettings = value;
            }
            return (T)Convert.ChangeType(AppSettings, typeof(T));
        }

        public static IConfiguration GetConfiguration()
        {
            if (Configuration == null)
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{GetEnvironmentVariable()}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
            return Configuration;
        }

        public static string GetEnvironmentVariable()
        {
            if (string.IsNullOrEmpty(EnvironmentVariable))
                EnvironmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return EnvironmentVariable;
        }
    }
}