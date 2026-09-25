namespace DataStructure.Abstractions;

/// <summary>
/// Dynamic list that provides O(1) indexed reads and amortized O(1) appends.
/// </summary>
public sealed class DSList<T> : DSCollection<T>, IReadOnlyList<T>
{
    public DSList(int capacity = 4) : base(capacity)
    {
    }

    public void Insert(int index, T item) => InsertAt(index, item);

    public T RemoveAt(int index) => base.RemoveAt(index);

    public int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = 0; i < Count; i++)
            if (comparer.Equals(this[i], item))
                return i;

        return -1;
    }

    public bool Contains(T item) => IndexOf(item) >= 0;
}
