using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test5
{
    public static class TimeZone
    {
        public static string ConvertTime(this DateTime dataTime, HttpContext context)
        {           
            var time = context.Request.Cookies["timezone"];
            if (time != null)
            {
                var timeOffSet = int.Parse(time.ToString());
                dataTime = dataTime.AddMinutes(-1 * timeOffSet);
                return dataTime.ToString();
            }
            return dataTime.ToLocalTime().ToString();
        }
    }
}
