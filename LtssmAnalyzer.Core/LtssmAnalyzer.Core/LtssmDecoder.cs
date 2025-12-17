using System;
using System.Collections.Generic;

namespace LtssmAnalyzer.Core
{
    public class LtssmDecoder
    {
        private class PortLtssmContext
        {
            public LtssmState CurrentState;
            public DateTime StateEnterTime;
            public bool IsInitialized;
        }

        private readonly Dictionary<string, PortLtssmContext> _ports =
            new Dictionary<string, PortLtssmContext>();

        public List<LtssmTransition> Transitions { get; private set; }

        public LtssmDecoder()
        {
            Transitions = new List<LtssmTransition>();
        }

        public void ProcessEvent(TraceEvent traceEvent)
        {
            if (!_ports.ContainsKey(traceEvent.Port))
            {
                _ports[traceEvent.Port] = new PortLtssmContext
                {
                    CurrentState = LtssmState.Detect,
                    IsInitialized = false
                };
            }

            PortLtssmContext ctx = _ports[traceEvent.Port];

            if (!ctx.IsInitialized)
            {
                ctx.StateEnterTime = traceEvent.Timestamp;
                ctx.IsInitialized = true;
            }

            LtssmState? nextState = DecodeNextState(traceEvent);
            if (!nextState.HasValue || nextState.Value == ctx.CurrentState)
                return;

            LtssmTransition transition = new LtssmTransition
            {
                FromState = ctx.CurrentState,
                ToState = nextState.Value,
                StartTime = ctx.StateEnterTime,
                EndTime = traceEvent.Timestamp,
                Port = traceEvent.Port,
                LinkSpeed = traceEvent.LinkSpeed
            };

            // Guard against invalid timestamps
            if (transition.Duration.TotalMilliseconds >= 0)
            {
                Transitions.Add(transition);
            }

            ctx.CurrentState = nextState.Value;
            ctx.StateEnterTime = traceEvent.Timestamp;
        }

        private LtssmState? DecodeNextState(TraceEvent evt)
        {
            switch (evt.EventType)
            {
                case "Detect": return LtssmState.Detect;
                case "Polling": return LtssmState.Polling;
                case "Config": return LtssmState.Configuration;
                case "L0": return LtssmState.L0;
                case "L0s": return LtssmState.L0s;
                case "L1": return LtssmState.L1;
                case "Recovery": return LtssmState.Recovery;
                case "HotReset": return LtssmState.HotReset;
                case "Disabled": return LtssmState.Disabled;
                default: return null;
            }
        }
    }
}
