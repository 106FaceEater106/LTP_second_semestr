using System.Collections.Generic;
using System.Linq;

namespace FP2
{
    public class Log
    {
        public string Weekday { get; set; }
        public double Load { get; set; }
    }

    class Program
    {
        public double GetAverageLoad(IEnumerable<Log> logs, string day)
        {
            return logs.Where(x => x.Weekday == day)
                .Select(x => x.Load)
                .Average(x => x);
        }
        
        public double GetAverageLoad2(IEnumerable<Log> logs, string day)
        {
            var count = 0;
            var sum = 0.0;
            foreach (var e in logs)
                if (e.Weekday == day)
                {
                    sum += e.Load;
                    count++;
                }

            return sum / count;
        }
    }
}
