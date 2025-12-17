using System;
using LtssmAnalyzer.Core;

class Program
{
    static void Main()
    {
        LtssmDecoder decoder = new LtssmDecoder();
        var trace = TraceSimulator.GenerateSampleTrace();

        foreach (var evt in trace)
        {
            decoder.ProcessEvent(evt);
        }

        Console.WriteLine("LTSSM Transitions:");
        Console.WriteLine("------------------");

        foreach (var t in decoder.Transitions)
        {
            Console.WriteLine(t);
        }

        // --- Metrics ---
        LtssmMetrics metrics = new LtssmMetrics();
        metrics.Compute(decoder.Transitions);

        Console.WriteLine("\nTime Spent Per State:");
        Console.WriteLine("---------------------");

        foreach (var kvp in metrics.TimePerState)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value.TotalMilliseconds:F2} ms");
        }

        // --- Recovery Analysis ---
        RecoveryAnalyzer recoveryAnalyzer = new RecoveryAnalyzer();
        recoveryAnalyzer.Analyze(decoder.Transitions, threshold: 2);

        Console.WriteLine("\nRecovery Analysis:");
        Console.WriteLine("------------------");
        Console.WriteLine($"Recovery Count: {recoveryAnalyzer.RecoveryCount}");
        Console.WriteLine($"Excessive Recovery Detected: {recoveryAnalyzer.ExcessiveRecoveryDetected}");

        // --- Link Down Analysis ---
        LinkDownAnalyzer linkDownAnalyzer = new LinkDownAnalyzer();
        linkDownAnalyzer.Analyze(decoder.Transitions);

        Console.WriteLine("\nLink Down Analysis:");
        Console.WriteLine("-------------------");
        Console.WriteLine($"Link Down Detected: {linkDownAnalyzer.LinkDownDetected}");

        Console.WriteLine("\nAnalysis Complete.");
        Console.ReadLine();

        // --- Speed Renegotiation ---
        SpeedRenegotiationAnalyzer speedAnalyzer = new SpeedRenegotiationAnalyzer();
        speedAnalyzer.Analyze(decoder.Transitions);

        Console.WriteLine("\nSpeed Analysis:");
        Console.WriteLine("---------------");
        Console.WriteLine("Initial Speed: Gen" + speedAnalyzer.InitialSpeed);
        Console.WriteLine("Final Speed: Gen" + speedAnalyzer.FinalSpeed);
        Console.WriteLine("Speed Fallback Detected: " + speedAnalyzer.SpeedFallbackDetected);

        // --- Directional Analysis ---
        DirectionalLtssmAnalyzer directionAnalyzer = new DirectionalLtssmAnalyzer();
        directionAnalyzer.Analyze(decoder.Transitions);

        Console.WriteLine("\nDirectional Analysis:");
        Console.WriteLine("---------------------");
        Console.WriteLine("Upstream / Downstream Divergence Detected: " +
                          directionAnalyzer.StateDivergenceDetected);

    }
}
