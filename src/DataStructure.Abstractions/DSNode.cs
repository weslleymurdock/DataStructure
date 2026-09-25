namespace DataStructure.Abstractions;

/// <summary>
/// Node used by linked data structures.
/// </summary>
public sealed class DSNode<T>(T value)
{
    public T Value { get; set; } = value;
    public DSNode<T>? Next { get; set; }
    public DSNode<T>? Previous { get; set; }
}
