namespace DataStructure.Algorithms.Visualization;

/// <summary>
/// Represents one frame of a sorting visualization.
/// The frame is deliberately named <c>Console</c> because it is the object
/// received by the visualization callback; it is not <see cref="System.Console"/>.
/// </summary>
public sealed class Console
{
    /// <summary>Creates a visualization frame from the current values.</summary>
    public Console(string algorithm, IReadOnlyList<int> values, int step)
    {
        ArgumentNullException.ThrowIfNull(algorithm);
        ArgumentNullException.ThrowIfNull(values);

        Algorithm = algorithm;
        Values = [.. values];
        Step = step;
    }

    /// <summary>Gets the algorithm currently being animated.</summary>
    public string Algorithm { get; }

    /// <summary>Gets an immutable snapshot of the values represented by this frame.</summary>
    public IReadOnlyList<int> Values { get; }

    /// <summary>Gets the zero-based frame number.</summary>
    public int Step { get; }

    /// <summary>Gets the side length of the conceptual X/Y chart.</summary>
    public int Count => Values.Count;

    /// <summary>Draws the frame using ANSI escape sequences.</summary>
    public void Render()
    {
        System.Console.Write("\x1b[2J\x1b[H");
        System.Console.WriteLine($"{Algorithm}  |  frame {Step:N0}  |  N = {Count}");
        System.Console.WriteLine();

        if (Count == 0)
        {
            System.Console.WriteLine("(empty)");
            return;
        }

        // Y is Count and X contains one column per value.
        // A value of N reaches the top row, while a value of 1 reaches one row.
        for (var y = Count; y >= 1; y--)
        {
            for (var x = 0; x < Count; x++)
            {
                var value = Values[x];

                if (value < y)
                {
                    System.Console.Write(" ");
                    continue;
                }

                // ANSI 256-color output gives each bar a visible terminal color.
                var color = 16 + (value * 200 / Math.Max(1, Count));
                System.Console.Write($"\x1b[48;5;{color}m \x1b[0m");
            }

            System.Console.WriteLine();
        }

        System.Console.WriteLine(new string('─', Count));
        System.Console.WriteLine($"0{new string(' ', Math.Max(0, Count - 1))}{Count}");
    }
}
