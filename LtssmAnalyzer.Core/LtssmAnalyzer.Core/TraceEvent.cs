using System;

namespace LtssmAnalyzer.Core
{
    public class TraceEvent
    {
        public DateTime Timestamp { get; set; }
        public string Port { get; set; }   // Upstream / Downstream
        public string EventType { get; set; } // TS1, TS2, RecoveryReq, etc.
        public int LinkSpeed { get; set; } // 1..5 (Gen1–Gen5)
    }
}
