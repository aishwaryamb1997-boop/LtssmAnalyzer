using System;
using System.Collections.Generic;

namespace LtssmAnalyzer.Core
{
    public static class TraceSimulator
    {
        public static List<TraceEvent> GenerateSampleTrace()
        {
            DateTime t = DateTime.Now;

            return new List<TraceEvent>
            {
                NewEvent(t, "Detect", "Upstream", 5),
                NewEvent(t.AddMilliseconds(5), "Polling", "Upstream", 5),
                NewEvent(t.AddMilliseconds(20), "Config", "Upstream", 5),
                NewEvent(t.AddMilliseconds(40), "L0", "Upstream", 5),

                NewEvent(t, "Detect", "Downstream", 5),
                NewEvent(t.AddMilliseconds(5), "Polling", "Downstream", 5),
                NewEvent(t.AddMilliseconds(20), "Recovery", "Downstream", 4),
                NewEvent(t.AddMilliseconds(60), "L0", "Downstream", 4)
            };
        }

        private static TraceEvent NewEvent(
            DateTime time,
            string type,
            string port,
            int speed)
        {
            return new TraceEvent
            {
                Timestamp = time,
                EventType = type,
                Port = port,
                LinkSpeed = speed
            };
        }
    }
}
