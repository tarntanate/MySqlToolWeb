using System;

namespace Ookbee.Ads.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset RoundUp(this DateTimeOffset dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTimeOffset(dt.Ticks + delta, dt.Offset);
        }

        public static DateTimeOffset RoundDown(this DateTimeOffset dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTimeOffset(dt.Ticks - delta, dt.Offset);
        }
    }
}
