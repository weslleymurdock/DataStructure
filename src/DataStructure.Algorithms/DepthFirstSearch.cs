using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Traverses a directed graph by exploring each branch before backtracking.
/// </summary>
public sealed class DepthFirstSearch
{
    /// <summary>
    /// Returns vertices in depth-first traversal order.
    /// </summary>
    public AlgorithmResult<IReadOnlyList<T>> Execute<T>(
        DSGraph<T> graph,
        T start)
        where T : notnull
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = graph.DepthFirst(start).ToArray();
        return new(result, stopwatch.Elapsed);
    }
}
