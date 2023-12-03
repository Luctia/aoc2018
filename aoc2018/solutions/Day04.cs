using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2018.solutions
{
    internal class Day04 : Day
    {
        public override void Part1()
        {
            // Construct log entries
            LogEntry[] logEntries = GetInputLines()
                .Select(x => new LogEntry(x))
                .OrderBy(x => x.GetDateTime)
                .ToArray();

            // Count how many minutes guards were sleeping
            Dictionary<int, List<(int, int)>> guardMinutesSleep = new Dictionary<int, List<(int, int)>>();
            int guardId = -1;
            DateTime fellAsleepAt = new DateTime();
            foreach (var logEntry in logEntries)
            {
                if (logEntry.Content.StartsWith("Guard")) {
                    guardId = int.Parse(Regex.Match(logEntry.Content, @"\d+").Value);
                    if (!guardMinutesSleep.ContainsKey(guardId))
                    {
                        guardMinutesSleep[guardId] = new List<(int, int)>();
                    }
                } else if (logEntry.Content.StartsWith("falls"))
                {
                    fellAsleepAt = logEntry.GetDateTime;
                } else
                {
                    guardMinutesSleep[guardId].Add((fellAsleepAt.Minute, logEntry.GetDateTime.Minute));
                }
            }

            // Determine sleepiest guard
            int sleepiestGuardId = 0;
            int mostMinutes = 0;
            foreach (int guard in guardMinutesSleep.Keys)
            {
                int minutesSleep = 0;
                foreach (var timespan in guardMinutesSleep[guard])
                {
                    minutesSleep += timespan.Item2 - timespan.Item1;
                }
                if (minutesSleep > mostMinutes)
                {
                    mostMinutes = minutesSleep;
                    sleepiestGuardId = guard;
                }
            }

            // Determine minute where most was slept
            List<int> distintMinutes = new List<int>();
            foreach (var timespan in guardMinutesSleep[sleepiestGuardId])
            {
                for (int i = timespan.Item1; i < timespan.Item2; i++)
                {
                    distintMinutes.Add(i);
                }
            }
            Answer(sleepiestGuardId * distintMinutes.GroupBy(i => i).OrderByDescending(i => i.Count()).Select(i => i.Key).First());
        }
            
        public override void Part2()
        {
            // Construct log entries
            LogEntry[] logEntries = GetInputLines()
                .Select(x => new LogEntry(x))
                .OrderBy(x => x.GetDateTime)
                .ToArray();

            // Count how many minutes guards were sleeping
            Dictionary<int, List<(int, int)>> guardMinutesSleep = new Dictionary<int, List<(int, int)>>();
            int guardId = -1;
            DateTime fellAsleepAt = new DateTime();
            foreach (var logEntry in logEntries)
            {
                if (logEntry.Content.StartsWith("Guard"))
                {
                    guardId = int.Parse(Regex.Match(logEntry.Content, @"\d+").Value);
                    if (!guardMinutesSleep.ContainsKey(guardId))
                    {
                        guardMinutesSleep[guardId] = new List<(int, int)>();
                    }
                }
                else if (logEntry.Content.StartsWith("falls"))
                {
                    fellAsleepAt = logEntry.GetDateTime;
                }
                else
                {
                    guardMinutesSleep[guardId].Add((fellAsleepAt.Minute, logEntry.GetDateTime.Minute));
                }
            }

            // Determine sleepiest guard and minute
            int sleepiestGuardId = -1;
            int minute = -1;
            int sameMinutes = -1;
            foreach (int guard in guardMinutesSleep.Keys)
            {
                List<int> distintMinutes = new List<int>();
                foreach (var timespan in guardMinutesSleep[guard])
                {
                    for (int i = timespan.Item1; i < timespan.Item2; i++)
                    {
                        distintMinutes.Add(i);
                    }
                }
                if (distintMinutes.Count > 0)
                {
                    int mostFrequentMinute = distintMinutes.GroupBy(i => i).OrderByDescending(i => i.Count()).Select(i => i.Key).First();
                    int count = distintMinutes.Count(i => i == mostFrequentMinute);
                    if (count > sameMinutes)
                    {
                        sleepiestGuardId = guard;
                        minute = mostFrequentMinute;
                        sameMinutes = count;
                    }
                }
            }

            Answer(sleepiestGuardId * minute);
        }

        private class LogEntry
        {
            DateTime dateTime;
            string content;
            public LogEntry(string repr)
            {
                var elements = repr.Split(new[] { "[", "-", " ", ":", "]" }, StringSplitOptions.RemoveEmptyEntries);
                this.dateTime = new DateTime(
                    int.Parse(elements[0]),
                    int.Parse(elements[1]),
                    int.Parse(elements[2]),
                    int.Parse(elements[3]),
                    int.Parse(elements[4]),
                    0
                    );
                this.content = repr.Split("] ")[1];
            }

            public DateTime GetDateTime { get { return dateTime; } }

            public string Content { get { return content; } }
        }
    }
}
