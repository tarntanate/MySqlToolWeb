using Microsoft.Extensions.Logging;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using System;
using System.Runtime.InteropServices;
using TimeZoneConverter;

namespace Ookbee.Ads.Common
{
    public class MechineDateTime
    {
        private static ILogger Logger { get; set; }

        public MechineDateTime(ILogger logger)
        {
            Logger = logger;
        }

        private static string windowsTimeZoneId;
        public static string WindowsTimeZoneId
        {
            get
            {
                if (!windowsTimeZoneId.HasValue())
                {
                    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                    var osPlatform = isWindows
                        ? $"{nameof(OSPlatform.Windows)}"
                        : $"{nameof(OSPlatform.Linux)}";
                    windowsTimeZoneId = ConfigurationHelper.GetConfiguration().GetSection($"AppSettings:TimeZone:{osPlatform}").Value;
                }
                return windowsTimeZoneId;
            }
        }

        public static DateTimeOffset Now
        {
            get
            {
                try
                {
                    var timeZoneInfo = TZConvert.GetTimeZoneInfo(WindowsTimeZoneId);
                    var dateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, timeZoneInfo);
                    return dateTime;
                }
                catch (TimeZoneNotFoundException)
                {
                    Logger.LogWarning("Unable to identify target time zone for conversion.");
                    return DateTimeOffset.Now;
                }
            }
        }

        public static DateTimeOffset UtcNow => Now.ToUniversalTime();

        public static DateTimeOffset FirstTimeOfWeek(DateTimeOffset caculateDate) => caculateDate.AddDays(-((int)caculateDate.DayOfWeek)).Date;

        public static DateTimeOffset LastTimeOfWeek(DateTimeOffset caculateDate) => FirstTimeOfWeek(caculateDate).AddDays(7).AddTicks(-1);

        public static DateTimeOffset FirstTimeOfMonth(DateTimeOffset caculateDate) => caculateDate.AddDays(-(caculateDate.Day - 1)).Date;

        public static DateTimeOffset LastTimeOfMonth(DateTimeOffset caculateDate) => FirstTimeOfMonth(caculateDate).AddMonths(1).AddTicks(-1);

        public static DateTimeOffset FirstTimeOfYear(DateTimeOffset caculateDate) => caculateDate.AddDays(-(caculateDate.DayOfYear - 1)).Date;

        public static DateTimeOffset LastTimeOfYear(DateTimeOffset caculateDate) => FirstTimeOfYear(caculateDate).AddYears(1).AddTicks(-1);
    }
}
