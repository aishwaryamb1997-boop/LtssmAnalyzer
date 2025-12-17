using System;

namespace LtssmAnalyzer.Core
{
    public class LtssmTransition
    {
        public LtssmState FromState { get; set; }
        public LtssmState ToState { get; set; }

        public string Port { get; set; }     // Upstream / Downstream
        public int LinkSpeed { get; set; }   // Gen1–Gen5

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan Duration
        {
            get { return EndTime - StartTime; }
        }
        public double DurationMs
        {
            get { return Duration.TotalMilliseconds; }
        }


        public override string ToString()
        {
            return string.Format(
                "{0} → {1} | {2} ms | Gen{3} | {4}",
                FromState,
                ToState,
                Duration.TotalMilliseconds.ToString("F2"),
                LinkSpeed,
                Port);
        }
    }
}
