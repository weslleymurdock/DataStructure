using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Finds a shortest path using A* with a caller-provided admissible heuristic.
/// </summary>
/// <remarks>
/// The heuristic should never overestimate the remaining cost when optimality is
/// required. With a zero heuristic, A* behaves like Dijkstra's algorithm.
/// </remarks>
public sealed class AStar
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
        var distances = graph.Vertices.ToDictionary(vertex => vertex, _ => double.PositiveInfinity);
        var previous = new Dictionary<T, T>();
        var open = new PriorityQueue<T, double>();

        if (!distances.ContainsKey(start) || !distances.ContainsKey(goal))
            return new([], stopwatch.Elapsed);

        distances[start] = 0;
        open.Enqueue(start, heuristic(start, goal));

        while (open.TryDequeue(out var current, out _))
        {
            if (EqualityComparer<T>.Default.Equals(current, goal))
                return new(BuildPath(previous, start, goal), stopwatch.Elapsed);

            foreach (var (neighbor, weight) in graph.Neighbors(current))
            {
                var candidate = distances[current] + weight;

                if (candidate >= distances[neighbor])
                    continue;

                distances[neighbor] = candidate;
                previous[neighbor] = current;
                open.Enqueue(neighbor, candidate + heuristic(neighbor, goal));
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
