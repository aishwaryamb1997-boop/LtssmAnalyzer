using System;
using System.Collections.Generic;

namespace LtssmAnalyzer.Core
{
    public class LtssmMetrics
    {
        public Dictionary<LtssmState, TimeSpan> TimePerState { get; private set; }

        public LtssmMetrics()
        {
            TimePerState = new Dictionary<LtssmState, TimeSpan>();
        }

        public void Compute(IEnumerable<LtssmTransition> transitions)
        {
            foreach (LtssmTransition transition in transitions)
            {
                if (!TimePerState.ContainsKey(transition.FromState))
                {
                    TimePerState[transition.FromState] = TimeSpan.Zero;
                }

                TimePerState[transition.FromState] += transition.Duration;
            }
        }
    }
}
