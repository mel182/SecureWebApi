using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.DateTimeGenerator
{
    public class TimeStampGenerator
    {
        public static long CurrentTimeStamp
        {
            get
            {
                return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        }
    }
}
