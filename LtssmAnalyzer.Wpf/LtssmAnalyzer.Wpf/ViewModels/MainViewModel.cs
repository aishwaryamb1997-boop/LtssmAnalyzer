using LtssmAnalyzer.Core;
using System.Collections.ObjectModel;

namespace LtssmAnalyzer.Wpf.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<LtssmTransition> Transitions { get; private set; }
        public ObservableCollection<StateMetricView> StateMetrics { get; private set; }

        public MainViewModel()
        {
            // Run Core analyzer
            LtssmDecoder decoder = new LtssmDecoder();
            var trace = TraceSimulator.GenerateSampleTrace();

            foreach (var evt in trace)
            {
                decoder.ProcessEvent(evt);
            }

            // LTSSM transitions
            Transitions = new ObservableCollection<LtssmTransition>(
                decoder.Transitions
            );

            // Time-per-state metrics
            LtssmMetrics metrics = new LtssmMetrics();
            metrics.Compute(decoder.Transitions);

            StateMetrics = new ObservableCollection<StateMetricView>();
            foreach (var kvp in metrics.TimePerState)
            {
                StateMetrics.Add(new StateMetricView
                {
                    State = kvp.Key.ToString(),
                    TimeMs = kvp.Value.TotalMilliseconds
                });
            }
        }
    }
}
