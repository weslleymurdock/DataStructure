using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Finds a graph path by prioritizing the vertex with the smallest heuristic.
/// </summary>
/// <remarks>
/// Unlike Dijkstra and A*, this algorithm does not include the accumulated path cost
/// in its priority. It is therefore not guaranteed to find a shortest path.
/// </remarks>
public sealed class GreedyBestFirstSearch
{
    /// <summary>
    /// Finds a path from the start vertex to the goal vertex.
    /// </summary>
    public AlgorithmResult<IReadOnlyList<T>> Execute<T>(
        DSWeightedGraph<T> graph,
        T start,
        T goal,
        Func<T, T, double> heuristic)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(heuristic);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var open = new PriorityQueue<T, double>();
        var previous = new Dictionary<T, T>();
        var visited = new HashSet<T>();

        if (!graph.Vertices.Contains(start) || !graph.Vertices.Contains(goal))
            return new([], stopwatch.Elapsed);

        open.Enqueue(start, heuristic(start, goal));

        while (open.TryDequeue(out var current, out _))
        {
            if (!visited.Add(current))
                continue;

            if (EqualityComparer<T>.Default.Equals(current, goal))
                return new(BuildPath(previous, start, goal), stopwatch.Elapsed);

            foreach (var (neighbor, _) in graph.Neighbors(current))
            {
                if (visited.Contains(neighbor))
                    continue;

                previous.TryAdd(neighbor, current);
                open.Enqueue(neighbor, heuristic(neighbor, goal));
            }
        }

        return new([], stopwatch.Elapsed);
    }

    private static IReadOnlyList<T> BuildPath<T>(
        IReadOnlyDictionary<T, T> previous,
        T start,
        T goal)
        where T : notnull
    {
        var path = new List<T> { goal };
        var current = goal;

        while (!EqualityComparer<T>.Default.Equals(current, start))
        {
            if (!previous.TryGetValue(current, out current))
                return [];

            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}
