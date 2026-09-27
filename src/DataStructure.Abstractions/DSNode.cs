namespace DataStructure.Abstractions;

/// <summary>
/// Node used by linked data structures.
/// A node stores its value and links to adjacent nodes.
/// </summary>
public sealed class DSNode<T>(T value)
{
    /// <summary>Gets or sets the value stored by the node.</summary>
    public T Value { get; set; } = value;

    /// <summary>Gets or sets the next node in a forward chain.</summary>
    public DSNode<T>? Next { get; set; }

    /// <summary>Gets or sets the previous node in a doubly linked chain.</summary>
    public DSNode<T>? Previous { get; set; }
}
