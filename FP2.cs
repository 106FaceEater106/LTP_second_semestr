using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
    
using System.Reflection;

namespace FP2
{
    public class Log
    {
        public string Weekday { get; set; }
        public double Load { get; set; }
    }

    internal static class Program
    {
        public static void Main()
        {
            var logsStr = File.ReadAllLines("/Users/Desktop/noname.txt", System.Text.Encoding.Default);
            var logs = logsStr
                .Where(x => x.Split(' ').Length == 2)
                .Select(x => new Log { Load = int.Parse(x.Split(' ')[1]), Weekday = x.Split(' ')[0] })
                .ToArray();
            Console.WriteLine(GetAverageLoad(logs, "monday"));
            Console.WriteLine(GetAverageLoadWithoutLinq(logs, "monday"));
        }

        private static double GetAverageLoad(IEnumerable<Log> logs, string day)
        {
            return logs.Where(x => x.Weekday == day)
                .Select(x => x.Load)
                .DefaultIfEmpty(0)
                .Average(x => x);
        }

        private static double GetAverageLoadWithoutLinq(IEnumerable<Log> logs, string day)
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
