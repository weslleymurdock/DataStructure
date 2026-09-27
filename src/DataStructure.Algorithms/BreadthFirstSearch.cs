using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Traverses a directed graph level by level using a queue.
/// </summary>
public sealed class BreadthFirstSearch
{
    /// <summary>
    /// Returns vertices in breadth-first traversal order.
    /// </summary>
    public AlgorithmResult<IReadOnlyList<T>> Execute<T>(
        DSGraph<T> graph,
        T start)
        where T : notnull
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = graph.BreadthFirst(start).ToArray();
        return new(result, stopwatch.Elapsed);
    }
}
