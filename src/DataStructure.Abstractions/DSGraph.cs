namespace DataStructure.Abstractions;

/// <summary>
/// Directed graph represented by adjacency lists.
/// </summary>
public sealed class DSGraph<T> where T : notnull
{
    private readonly Dictionary<T, HashSet<T>> _adjacency = [];

    public IReadOnlyCollection<T> Vertices => _adjacency.Keys;

    public void AddVertex(T vertex)
        => _adjacency.TryAdd(vertex, []);

    public void AddEdge(T from, T to)
    {
        AddVertex(from);
        AddVertex(to);
        _adjacency[from].Add(to);
    }

    public bool HasEdge(T from, T to)
        => _adjacency.TryGetValue(from, out var neighbors)
            && neighbors.Contains(to);

    public IReadOnlyCollection<T> Neighbors(T vertex)
        => _adjacency.TryGetValue(vertex, out var neighbors)
            ? neighbors
            : [];

    public IEnumerable<T> BreadthFirst(T start)
    {
        if (!_adjacency.ContainsKey(start))
            yield break;

        var visited = new HashSet<T> { start };
        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            yield return vertex;

            foreach (var neighbor in _adjacency[vertex])
            {
                if (visited.Add(neighbor))
                    queue.Enqueue(neighbor);
            }
        }
    }

    public IEnumerable<T> DepthFirst(T start)
    {
        if (!_adjacency.ContainsKey(start))
            yield break;

        var visited = new HashSet<T>();

        foreach (var vertex in DepthFirstCore(start, visited))
            yield return vertex;
    }

    private IEnumerable<T> DepthFirstCore(T vertex, HashSet<T> visited)
    {
        if (!visited.Add(vertex))
            yield break;

        yield return vertex;

        foreach (var neighbor in _adjacency[vertex])
        {
            foreach (var next in DepthFirstCore(neighbor, visited))
                yield return next;
        }
    }
}
