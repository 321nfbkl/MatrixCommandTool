using System;

namespace FastSocket.SocketBase.Utils
{
    static public class Date
    {
        #region Private Members

        static private int lastTicks = -1;
        static private DateTime lastDateTime;

        /// <summary>
        /// Unix开始时间
        /// </summary>
        static private readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        static private readonly long dateTimeMaxValueMillisecondsSinceEpoch =
            (DateTime.MaxValue - unixEpoch).Ticks / 10000;

        #endregion

        #region Public Methods

        /// <summary>
        /// 优化获取Utc时间方法
        /// </summary>
        static public DateTime UtcNow
        {
            get
            {
                int tickCount = Environment.TickCount;
                if (tickCount == lastTicks) return lastDateTime;

                DateTime dt = DateTime.UtcNow;
                lastTicks = tickCount;
                lastDateTime = dt;
                return dt;
            }
        }

        /// <summary>
        /// 将 DateTime 转换为 UTC（对 MinValue 和 MaxValue 进行特殊处理）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        static public DateTime ToUniversalTime(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc) return dateTime;
            else
            {
                if (dateTime == DateTime.MinValue) return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
                else if (dateTime == DateTime.MaxValue)
                    return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
                else return dateTime.ToUniversalTime();
            }
        }

        /// <summary>
        /// Converts a DateTime to number of milliseconds since Unix epoch.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Number of seconds since Unix epoch</returns>
        static public long ToMillisecondsSinceEpoch(DateTime dateTime)
        {
            return (ToUniversalTime(dateTime) - unixEpoch).Ticks / 10000;
        }

        /// <summary>
        /// Converts a DateTime to number of seconds since Unix epoch.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Number of seconds since Unix epoch</returns>
        public static long ToSecondsSinceEpoch(DateTime dateTime)
        {
            return ToMillisecondsSinceEpoch(dateTime) / 1000;
        }

        /// <summary>
        /// Converts from number of milliseconds since Unix epoch to DateTime
        /// </summary>
        /// <param name="millisecondsSinceEpoch">The number of milliseconds since Unix epoch</param>
        /// <returns></returns>
        static public DateTime ToDateTimeFromMillisecondsSinceEpoch(long millisecondsSinceEpoch)
        {
            if (millisecondsSinceEpoch == dateTimeMaxValueMillisecondsSinceEpoch)
                return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
            else return unixEpoch.AddTicks(millisecondsSinceEpoch * 10000);
        }
        
        /// <summary>
        /// Converts from number of seconds since Unix epoch to DateTime.
        /// </summary>
        /// <param name="secondsSinceEpoch">The number of seconds since Unix epoch.</param>
        /// <returns></returns>
        static public DateTime ToDateTimeFromSecondsSinceEpoch(long secondsSinceEpoch)
        {
            return ToDateTimeFromMillisecondsSinceEpoch(secondsSinceEpoch * 1000);
        }

        /// <summary>
        /// Converts a DateTime to local time (with special handling for MinValue and MaxValue).
        /// </summary>
        /// <param name="dateTime">A DateTime.</param>
        /// <param name="kind">A DateTimeKind.</param>
        /// <returns>The DateTime in local time.</returns>
        static public DateTime ToLocalTime(DateTime dateTime, DateTimeKind kind)
        {
            if (dateTime.Kind == kind) return dateTime;
            else
            {
                if (dateTime == DateTime.MinValue) return DateTime.SpecifyKind(DateTime.MinValue, kind); 
                else if (dateTime == DateTime.MaxValue) return DateTime.SpecifyKind(DateTime.MaxValue, kind);
                else return DateTime.SpecifyKind(dateTime.ToLocalTime(), kind);
            }
        }
        
        /// <summary>
        /// SpecifyKind
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static DateTime SpecifyKind(DateTime dt, DateTimeKind kind)
        {
            if (dt.Kind == kind) return dt;
            return DateTime.SpecifyKind(dt, kind);
        }
        #endregion
    }
}