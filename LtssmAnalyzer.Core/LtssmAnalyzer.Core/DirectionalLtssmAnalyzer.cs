using System.Collections.Generic;
using System.Linq;

namespace LtssmAnalyzer.Core
{
    public class DirectionalLtssmAnalyzer
    {
        public bool StateDivergenceDetected { get; private set; }

        public void Analyze(IEnumerable<LtssmTransition> transitions)
        {
            var upstream = transitions.Where(t => t.Port == "Upstream");
            var downstream = transitions.Where(t => t.Port == "Downstream");

            LtssmState? lastUp = upstream.LastOrDefault()?.ToState;
            LtssmState? lastDown = downstream.LastOrDefault()?.ToState;

            StateDivergenceDetected = lastUp.HasValue &&
                                      lastDown.HasValue &&
                                      lastUp.Value != lastDown.Value;
        }
    }
}
