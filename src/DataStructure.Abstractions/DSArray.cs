namespace DataStructure.Abstractions;

/// <summary>
/// Fixed-size, zero-based array abstraction backed by contiguous memory.
/// Indexed reads and writes are O(1).
/// </summary>
public sealed class DSArray<T> : IReadOnlyList<T>
{
    private readonly T[] _items;

    /// <summary>Creates an array containing the requested number of default values.</summary>
    public DSArray(int length)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        // A real array allocates the complete storage at construction time,
        // which is why this structure has a fixed capacity.
        _items = new T[length];
    }

    /// <summary>Creates an array containing a copy of the supplied sequence.</summary>
    public DSArray(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        // Collection expressions materialize the sequence into contiguous storage.
        _items = [.. items];
    }

    /// <summary>Gets the number of positions in the array.</summary>
    public int Count => _items.Length;

    /// <summary>
    /// Gets or sets a value by zero-based index.
    /// The underlying array performs the access in O(1).
    /// </summary>
    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    /// <summary>Returns an enumerator over the stored values.</summary>
    public IEnumerator<T> GetEnumerator()
        => ((IEnumerable<T>)_items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
