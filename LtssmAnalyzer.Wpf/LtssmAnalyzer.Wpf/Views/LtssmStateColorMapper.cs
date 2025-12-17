using System.Windows.Media;
using LtssmAnalyzer.Core;

namespace LtssmAnalyzer.Wpf.Views
{
    public static class LtssmStateColorMapper
    {
        public static Brush GetColor(LtssmState state)
        {
            if (state == LtssmState.Recovery)
                return Brushes.OrangeRed;

            switch (state)
            {
                case LtssmState.Detect:
                    return Brushes.SteelBlue;

                case LtssmState.Polling:
                    return Brushes.DeepSkyBlue;

                case LtssmState.Configuration:
                    return Brushes.DodgerBlue;

                case LtssmState.L0:
                    return Brushes.Green;

                default:
                    return Brushes.Gray;
            }
        }
    }
}
