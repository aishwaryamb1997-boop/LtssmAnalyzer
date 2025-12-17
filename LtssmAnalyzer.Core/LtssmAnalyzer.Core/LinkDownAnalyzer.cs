using System.Collections.Generic;

namespace LtssmAnalyzer.Core
{
    public class LinkDownAnalyzer
    {
        public bool LinkDownDetected { get; private set; }

        public void Analyze(IEnumerable<LtssmTransition> transitions)
        {
            foreach (LtssmTransition t in transitions)
            {
                if (t.ToState == LtssmState.Disabled)
                {
                    LinkDownDetected = true;
                    return;
                }
            }

            LinkDownDetected = false;
        }
    }
}
