namespace DataStructure.Abstractions;

/// <summary>
/// Directed weighted graph represented by adjacency lists.
/// </summary>
/// <typeparam name="T">The vertex type.</typeparam>
/// <remarks>
/// Edge weights are expected to be non-negative for shortest-path algorithms such as
/// Dijkstra and A*.
/// </remarks>
public sealed class DSWeightedGraph<T> where T : notnull
{
    private readonly Dictionary<T, List<Edge>> _adjacency = [];

    /// <summary>
    /// Gets all vertices currently stored in the graph.
    /// </summary>
    public IReadOnlyCollection<T> Vertices => _adjacency.Keys;

    /// <summary>
    /// Adds a vertex when it does not already exist.
    /// </summary>
    public void AddVertex(T vertex)
        => _adjacency.TryAdd(vertex, []);

    /// <summary>
    /// Adds or replaces a directed weighted edge.
    /// </summary>
    /// <param name="from">The source vertex.</param>
    /// <param name="to">The destination vertex.</param>
    /// <param name="weight">The non-negative edge weight.</param>
    public void AddEdge(T from, T to, double weight)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(weight);

        AddVertex(from);
        AddVertex(to);

        var edges = _adjacency[from];

        for (var index = 0; index < edges.Count; index++)
        {
            if (!EqualityComparer<T>.Default.Equals(edges[index].To, to))
                continue;

            edges[index] = new Edge(to, weight);
            return;
        }

        edges.Add(new Edge(to, weight));
    }

    /// <summary>
    /// Gets the outgoing weighted edges of a vertex.
    /// </summary>
    public IReadOnlyCollection<(T To, double Weight)> Neighbors(T vertex)
        => _adjacency.TryGetValue(vertex, out var edges)
            ? edges.Select(edge => (edge.To, edge.Weight)).ToArray()
            : [];

    private readonly record struct Edge(T To, double Weight);
}
