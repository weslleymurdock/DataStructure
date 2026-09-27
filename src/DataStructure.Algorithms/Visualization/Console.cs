namespace DataStructure.Algorithms.Visualization;

/// <summary>
/// Composite frame received by <see cref="AlgorithmVisualizationDemo"/> callbacks.
/// It renders all sorting algorithms side by side and exposes their progress.
/// </summary>
public sealed class Console
{
    /// <summary>Creates a composite visualization frame.</summary>
    public Console(
        string structure,
        IReadOnlyList<PanelState> panels,
        IReadOnlyList<string> completionOrder)
    {
        ArgumentNullException.ThrowIfNull(structure);
        ArgumentNullException.ThrowIfNull(panels);
        ArgumentNullException.ThrowIfNull(completionOrder);

        Structure = structure;
        Panels = [.. panels];
        CompletionOrder = [.. completionOrder];

        foreach (var panel in Panels)
            panel.Structure = structure;
    }

    /// <summary>Gets the data structure used by this race.</summary>
    public string Structure { get; }

    /// <summary>Gets the six algorithm panels.</summary>
    public IReadOnlyList<PanelState> Panels { get; }

    /// <summary>Gets algorithms ordered by the moment they finished.</summary>
    public IReadOnlyList<string> CompletionOrder { get; }

    /// <summary>Draws all six panels side by side using ANSI escape sequences.</summary>
    public void Render()
    {
        System.Console.Write("[2J[H");

        System.Console.WriteLine(
            $"SORT RACE | Structure: {Structure} | " +
            $"Algorithms: {Panels.Count}");

        System.Console.WriteLine(
            CompletionOrder.Count == 0
                ? "Finish order: -"
                : $"Finish order: {FormatCompletionOrder()}");

        System.Console.WriteLine();

        RenderHeaders();
        RenderCharts();
        RenderFooter();
    }

    private void RenderHeaders()
    {
        foreach (var panel in Panels)
        {
            var status = panel.Completed
                ? $"#{panel.CompletionOrder}"
                : "running";

            var header =
                $"{panel.Algorithm} {status} | {panel.Iterations:N0} iter";

            System.Console.Write(
                header.PadRight(GetPanelWidth()));
        }

        System.Console.WriteLine();
    }

    private void RenderCharts()
    {
        var count = Panels.Count == 0
            ? 0
            : Panels.Max(panel => panel.Values.Count);

        for (var y = count; y >= 1; y--)
        {
            foreach (var panel in Panels)
            {
                for (var x = 0; x < panel.Values.Count; x++)
                {
                    var value = panel.Values[x];

                    if (value < y)
                    {
                        System.Console.Write(" ");
                        continue;
                    }

                    var color =
                        16 + value * 200 / Math.Max(1, count);

                    System.Console.Write(
                        $"[48;5;{color}m [0m");
                }

                System.Console.Write(
                    new string(
                        ' ',
                        Math.Max(0, GetPanelWidth() - panel.Values.Count)));
            }

            System.Console.WriteLine();
        }

        foreach (var panel in Panels)
        {
            System.Console.Write(
                new string('─', panel.Values.Count));

            System.Console.Write(
                new string(
                    ' ',
                    Math.Max(0, GetPanelWidth() - panel.Values.Count)));
        }

        System.Console.WriteLine();
    }

    private void RenderFooter()
    {
        foreach (var panel in Panels)
        {
            var status = panel.Completed
                ? $"DONE #{panel.CompletionOrder}"
                : "RUNNING";

            var text =
                $"{status} | iterations={panel.Iterations:N0}";

            System.Console.Write(
                text.PadRight(GetPanelWidth()));
        }

        System.Console.WriteLine();
        System.Console.WriteLine(
            "Finish rank: #1 is first; " +
            $"#{Panels.Count} is last.");
    }

    private string FormatCompletionOrder()
    {
        return string.Join(
            " -> ",
            CompletionOrder.Select(
                (algorithm, index) => $"#{index + 1} {algorithm}"));
    }

    private static int GetPanelWidth()
    {
        return 20;
    }
}
