
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CoinTradeOKX
{
    public static class DateUtil
    {
        public static long time_diff = 0;


        /*
        public static Task<bool> CorrectingServerTime(string url)
        {
            return Task.Run<bool>(async () =>
            {
                string error = string.Empty;
                JObject time_response = await Http.HttpGet(url);
                if (time_response.ContainsKey("data") && time_response["code"].Value<int>() == 0)
                {
                    time_response = time_response["data"] as JObject;
                }

                if (time_response.ContainsKey("epoch"))
                {
                    DateUtil.SetServerTimestamp((long)(time_response["epoch"].Value<double>() * 1000));
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
        */

        public static string DateTime2ISO8601(DateTime time)
        {

            return time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

        }

        public static string GetServerTimeISO8601()
        {
            DateTime now = DateTime.Now;
            now = now.AddMilliseconds(time_diff);

            return DateTime2ISO8601(now);
        }

        public static string GetServerUTCTimeISO8601()
        {
            DateTime now = GetServerUTCDateTime();
            return DateTime2ISO8601(now);
        }

        public static DateTime GetServerUTCDateTime()
        {
            DateTime now = DateTime.UtcNow;
            now = now.AddMilliseconds(time_diff);
            return now;
        }

        public static void SetServerTimestamp(long timestampMS)
        {
            var local = GetTimestampMS();
            time_diff = timestampMS - local;
        }

        public static long GetServerUTCTimestampSec()
        {
            long local = GetServerUTCTimestampMS();
            return local / 1000;
        }
        public static long GetServerUTCTimestampMS()
        {
            DateTime utc = DateTime.UtcNow;

            long local = GetTimestampMS(utc);
            return local + time_diff;
        }

        public static DateTime GetServerDateTime()
        {
            DateTime now = DateTime.Now;
            now = now.AddMilliseconds(time_diff);

            return now;
        }

        public static long GetServerTimestampMS()
        {
            long local = GetTimestampMS();
            return local + time_diff;
        }

        public static long GetServerTimestampSec()
        {
            long ms = GetServerTimestampMS();
            return ms / 1000;
        }

        //返回当前毫秒时间戳
        public static long GetTimestampMS()
        {
            return GetTimestampMS(DateTime.Now);
        }
        static DateTime DATE_TIME_1970 = new DateTime(1970, 1, 1);

        //返回毫秒时间戳
        public static long GetTimestampMS(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DATE_TIME_1970); // 当地时区
            return (long)(time - startTime).TotalMilliseconds; // 相差毫秒数
        }

        //返回秒时间戳
        public static long GetTimestampSec(DateTime time)
        {
            return GetTimestampMS(time) / 1000;
        }


        public static DateTime TimestampSecToDateTime(long timestamp)
        {
            return DATE_TIME_1970.AddSeconds(timestamp);
        }

        public static DateTime TimestampMSToDateTime(long timestamp)
        {
            return DATE_TIME_1970.AddMilliseconds(timestamp);
        }

        //返回秒时间戳
        public static long GetTimestampSec()
        {
            return GetTimestampSec(DateTime.Now);
        }

        public static DateTime UtcToLocalTime(DateTime utcTime)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(utcTime);
        }

        public static DateTime LocalTimeToUtc(DateTime localTime)
        {
            return TimeZone.CurrentTimeZone.ToUniversalTime(localTime);
        }
    }
}
