using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Computes shortest distances from one source using Dijkstra's algorithm.
/// </summary>
/// <remarks>
/// All edge weights must be non-negative. The implementation uses a priority queue
/// and runs in O((V + E) log V) time for the adjacency-list representation.
/// </remarks>
public sealed class Dijkstra
{
    /// <summary>
    /// Computes the shortest known distance from the source to every reachable vertex.
    /// </summary>
    public AlgorithmResult<IReadOnlyDictionary<T, double>> Execute<T>(
        DSWeightedGraph<T> graph,
        T start)
        where T : notnull
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var distances = graph.Vertices.ToDictionary(vertex => vertex, _ => double.PositiveInfinity);
        var queue = new PriorityQueue<T, double>();

        if (!distances.ContainsKey(start))
            return new(distances, stopwatch.Elapsed);

        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.TryDequeue(out var current, out var currentDistance))
        {
            if (currentDistance > distances[current])
                continue;

            foreach (var (neighbor, weight) in graph.Neighbors(current))
            {
                var candidate = currentDistance + weight;

                if (candidate >= distances[neighbor])
                    continue;

                distances[neighbor] = candidate;
                queue.Enqueue(neighbor, candidate);
            }
        }

        return new(distances, stopwatch.Elapsed);
    }
}
