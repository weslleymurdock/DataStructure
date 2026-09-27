namespace DataStructure.Abstractions;

/// <summary>
/// Dynamic list derived from <see cref="DSCollection{T}"/>.
/// It exposes insertion and removal by index while retaining O(1) indexed reads.
/// </summary>
/// <remarks>Creates a list with the specified initial capacity.</remarks>
public sealed class DSList<T>(int capacity = 4) : DSCollection<T>(capacity), IReadOnlyList<T>
{


    /// <summary>Inserts a value at an index. The suffix shift costs O(n).</summary>
    public void Insert(int index, T item)
        => InsertAt(index, item);

    /// <summary>Removes and returns the value at an index. The suffix shift costs O(n).</summary>
    public new T RemoveAt(int index)
        => base.RemoveAt(index);

    /// <summary>Finds the first matching value using a linear O(n) scan.</summary>
    public new int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < Count; index++)
        {
            if (comparer.Equals(this[index], item))
                return index;
        }

        return -1;
    }

    /// <summary>Checks whether a value exists using IndexOf.</summary>
    public new bool Contains(T item)
        => IndexOf(item) >= 0;
}
