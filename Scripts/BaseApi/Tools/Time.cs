using System;


namespace Tools.Time
{
    public static class TimeTool
    {
        public static DateTime ZeroTime() => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }


    public static class Extensions
    {
        public static bool IsZero(this DateTime dt) => dt.GetTimeFrom1Jan1970().TotalMilliseconds < 1.0;

        private static TimeSpan GetTimeFrom1Jan1970(this DateTime dt)
        {
            if (dt.Kind.Equals(DateTimeKind.Utc))
            {
                return dt - TimeTool.ZeroTime();
            }
            if (dt.Kind.Equals(DateTimeKind.Local))
            {
                return dt.ToUniversalTime() - TimeTool.ZeroTime();
            }
            throw new ArgumentException("Unexpected kind for datetime: " + dt.Kind);
        }

        public static uint GetTimeFrom1Jan1970AtSeconds(this DateTime dt)
        {
            var milliseconds = dt.GetTimeFrom1Jan1970AtMilliseconds();
            var seconds = milliseconds / 1000.0;
            seconds = Math.Floor(seconds);
            return (uint)seconds;
        }

        public static double GetTimeFrom1Jan1970AtMilliseconds(this DateTime dt)
        {
            return dt.GetTimeFrom1Jan1970().TotalMilliseconds;
        }
    }
}