using LtssmAnalyzer.Core;
using LtssmAnalyzer.Wpf.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LtssmAnalyzer.Wpf.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            // ✅ Create ONE ViewModel instance
            _vm = new MainViewModel();
            DataContext = _vm;

            ToolTipService.SetInitialShowDelay(this, 100);
            ToolTipService.SetShowDuration(this, 15000);
            ToolTipService.SetBetweenShowDelay(this, 50);

            // ✅ Pass VM explicitly
            Loaded += (s, e) =>
            {
                RenderTimeline(_vm);
                RenderTimeAxis(_vm);
            };
        }

        // ✅ MUST be inside class
        private static ToolTip BuildTransitionToolTip(LtssmTransition t)
        {
            return new ToolTip
            {
                Content =
                    $"From: {t.FromState}\n" +
                    $"To: {t.ToState}\n" +
                    $"Duration: {t.DurationMs:F2} ms\n" +
                    $"Speed: Gen{t.LinkSpeed}\n" +
                    $"Port: {t.Port}",
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12
            };
        }

        private void RenderTimeline(MainViewModel vm)
        {
            TimelineCanvas.Children.Clear();

            if (vm.Transitions == null || vm.Transitions.Count == 0)
                return;

            double pixelsPerMs = 6.0;
            double laneHeight = 28;

            double upstreamY = 20;
            double downstreamY = 65;

            DateTime t0 = vm.Transitions.Min(t => t.StartTime);

            var ordered = vm.Transitions
                            .OrderBy(t => t.StartTime)
                            .ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                var t = ordered[i];

                double startMs = (t.StartTime - t0).TotalMilliseconds;
                double widthMs = t.DurationMs;

                double x = startMs * pixelsPerMs;
                double width = Math.Max(widthMs * pixelsPerMs, 3);

                double y = t.Port == "Upstream" ? upstreamY : downstreamY;

                // Speed renegotiation marker
                if (i > 0 && ordered[i - 1].LinkSpeed != t.LinkSpeed)
                {
                    TimelineCanvas.Children.Add(new Line
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 0,
                        Y2 = TimelineCanvas.Height,
                        Stroke = Brushes.Yellow,
                        StrokeThickness = 2,
                        StrokeDashArray = new DoubleCollection { 4, 2 }
                    });
                }

                Rectangle rect = new Rectangle
                {
                    Width = width,
                    Height = laneHeight,
                    Fill = LtssmStateColorMapper.GetColor(t.ToState),
                    Stroke = t.ToState == LtssmState.Recovery ? Brushes.Red : Brushes.Black,
                    StrokeThickness = t.ToState == LtssmState.Recovery ? 2 : 0.5,
                    RadiusX = 4,
                    RadiusY = 4,
                    ToolTip = BuildTransitionToolTip(t)
                };

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);

                TimelineCanvas.Children.Add(rect);
            }

            TimelineCanvas.Width =
                ordered.Max(t =>
                    (t.StartTime - t0).TotalMilliseconds + t.DurationMs)
                * pixelsPerMs + 60;
        }

        private void RenderTimeAxis(MainViewModel vm)
        {
            TimeAxisCanvas.Children.Clear();

            if (vm.Transitions == null || vm.Transitions.Count == 0)
                return;

            double pixelsPerMs = 6.0;
            DateTime t0 = vm.Transitions.Min(t => t.StartTime);

            double totalMs = vm.Transitions.Max(t =>
                (t.StartTime - t0).TotalMilliseconds + t.DurationMs);

            for (int ms = 0; ms <= totalMs; ms += 10)
            {
                double x = ms * pixelsPerMs;

                TimeAxisCanvas.Children.Add(new Line
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 15,
                    Y2 = 30,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1
                });

                TextBlock label = new TextBlock
                {
                    Text = $"{ms} ms",
                    Foreground = Brushes.LightGray,
                    FontSize = 10
                };

                Canvas.SetLeft(label, x + 2);
                Canvas.SetTop(label, 0);

                TimeAxisCanvas.Children.Add(label);
            }
        }

    }
}
