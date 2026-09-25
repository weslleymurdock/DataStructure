namespace DataStructure.Abstractions;

/// <summary>
/// Fixed-size, zero-based array abstraction with constant-time indexed access.
/// </summary>
public sealed class DSArray<T> : IReadOnlyList<T>
{
    private readonly T[] _items;

    public DSArray(int length)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        _items = new T[length];
    }

    public DSArray(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToArray();
    }

    public int Count => _items.Length;

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
