using System.Collections.Generic;

namespace LtssmAnalyzer.Core
{
    public class RecoveryAnalyzer
    {
        public int RecoveryCount { get; private set; }
        public bool ExcessiveRecoveryDetected { get; private set; }

        public void Analyze(IEnumerable<LtssmTransition> transitions, int threshold)
        {
            RecoveryCount = 0;

            foreach (LtssmTransition t in transitions)
            {
                if (t.ToState == LtssmState.Recovery)
                {
                    RecoveryCount++;
                }
            }

            ExcessiveRecoveryDetected = RecoveryCount >= threshold;
        }
    }
}
