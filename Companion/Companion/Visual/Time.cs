using System;
using System.Collections.Generic;
using System.Text;

namespace Companion.Visual
{
    class Time
    {
        // tool to compare elapsed time to a timeperiod
        public static bool Elapsed(DateTime previousTime, double timePeriod)
        {
            TimeSpan timeElapsed = DateTime.Now - previousTime;
            return (timeElapsed.TotalSeconds > timePeriod);
        }
    }
}
