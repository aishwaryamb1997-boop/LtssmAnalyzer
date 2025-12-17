using System.Collections.Generic;

namespace LtssmAnalyzer.Core
{
    public class SpeedRenegotiationAnalyzer
    {
        public bool SpeedFallbackDetected { get; private set; }
        public int InitialSpeed { get; private set; }
        public int FinalSpeed { get; private set; }

        public void Analyze(IEnumerable<LtssmTransition> transitions)
        {
            InitialSpeed = -1;
            FinalSpeed = -1;
            SpeedFallbackDetected = false;

            foreach (LtssmTransition t in transitions)
            {
                if (InitialSpeed == -1)
                {
                    InitialSpeed = t.LinkSpeed;
                }

                FinalSpeed = t.LinkSpeed;

                if (FinalSpeed < InitialSpeed)
                {
                    SpeedFallbackDetected = true;
                }
            }
        }
    }
}
