using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Extentions
{
    public static class TimeExtentions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {            
            return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
        }
    }
}
