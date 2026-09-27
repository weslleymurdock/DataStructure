namespace DataStructure.Algorithms.Visualization;

/// <summary>
/// Mutable state for one algorithm panel while a race is running.
/// </summary>
public sealed class PanelState
{
    /// <summary>Creates a panel with the initial unsorted sequence.</summary>
    public PanelState(string algorithm, IReadOnlyList<int> values)
    {
        ArgumentNullException.ThrowIfNull(algorithm);
        ArgumentNullException.ThrowIfNull(values);

        Algorithm = algorithm;
        Values = [.. values];
    }

    /// <summary>Gets the algorithm represented by the panel.</summary>
    public string Algorithm { get; }

    /// <summary>Gets the data structure used by this execution.</summary>
    public string Structure { get; internal set; } = string.Empty;

    /// <summary>Gets the latest sequence snapshot.</summary>
    public IReadOnlyList<int> Values { get; private set; }

    /// <summary>Gets the number of algorithm mutation callbacks received.</summary>
    public int Iterations { get; private set; }

    /// <summary>Gets whether this algorithm has completed.</summary>
    public bool Completed { get; private set; }

    /// <summary>Gets the completion position in the race.</summary>
    public int CompletionOrder { get; private set; }

    /// <summary>Updates the panel with a new algorithm snapshot.</summary>
    public void Update(IReadOnlyList<int> values)
    {
        Values = [.. values];
        Iterations++;
    }

    /// <summary>Marks the algorithm as complete and records its finish order.</summary>
    public void Complete(int completionOrder)
    {
        Completed = true;
        CompletionOrder = completionOrder;
    }
}
