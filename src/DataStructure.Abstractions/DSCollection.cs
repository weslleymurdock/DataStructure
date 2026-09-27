namespace DataStructure.Abstractions;

/// <summary>
/// General-purpose dynamically sized collection backed by a contiguous array.
/// Appending is amortized O(1); indexed access is O(1); inserting/removing
/// in the middle is O(n) because subsequent values must be shifted.
/// </summary>
public class DSCollection<T> : ICollection<T>
{
    private T[] _items;
    private int _count;

    /// <summary>Creates a collection with the specified initial capacity.</summary>
    public DSCollection(int capacity = 4)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        // A zero-capacity collection starts without an allocated backing array.
        _items = capacity == 0 ? [] : new T[capacity];
    }

    /// <summary>Gets the number of stored values.</summary>
    public int Count => _count;

    /// <summary>Gets whether the collection is read-only. This implementation is mutable.</summary>
    public bool IsReadOnly => false;

    /// <summary>Adds a value to the end of the collection.</summary>
    public virtual void Add(T item)
    {
        // Grow only when the existing array has no free slot.
        EnsureCapacity(_count + 1);

        // The new value occupies the first unused position.
        _items[_count++] = item;
    }

    /// <summary>Removes all values from the collection.</summary>
    public void Clear()
    {
        // Clear releases references held by the unused logical range.
        Array.Clear(_items, 0, _count);
        _count = 0;
    }

    /// <summary>Checks whether a value exists using a linear scan.</summary>
    public bool Contains(T item)
        => IndexOf(item) >= 0;

    /// <summary>Copies the logical contents into a destination array.</summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 ||
            arrayIndex > array.Length ||
            array.Length - arrayIndex < _count)
        {
            throw new ArgumentException(
                "The destination array is too small.",
                nameof(array));
        }

        // Array.Copy performs one contiguous copy of the logical contents.
        Array.Copy(_items, 0, array, arrayIndex, _count);
    }

    /// <summary>Removes the first matching value.</summary>
    public bool Remove(T item)
    {
        // Search first because the collection does not keep an index of values.
        var index = IndexOf(item);

        if (index < 0)
            return false;

        RemoveAt(index);
        return true;
    }

    /// <summary>Gets or sets a value by index in O(1).</summary>
    public T this[int index]
    {
        get
        {
            ValidateIndex(index);
            return _items[index];
        }
        set
        {
            ValidateIndex(index);
            _items[index] = value;
        }
    }

    /// <summary>Finds the first matching value using a linear O(n) scan.</summary>
    protected int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < _count; index++)
        {
            if (comparer.Equals(_items[index], item))
                return index;
        }

        return -1;
    }

    /// <summary>Inserts a value and shifts the suffix one position to the right.</summary>
    protected void InsertAt(int index, T item)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        EnsureCapacity(_count + 1);

        // Copying the suffix creates the empty position required for the new value.
        Array.Copy(
            _items,
            index,
            _items,
            index + 1,
            _count - index);

        _items[index] = item;
        _count++;
    }

    /// <summary>Removes a value and shifts the suffix one position to the left.</summary>
    protected T RemoveAt(int index)
    {
        ValidateIndex(index);

        var value = _items[index];

        // Shift all values after the removed position toward the front.
        Array.Copy(
            _items,
            index + 1,
            _items,
            index,
            _count - index - 1);

        // Clear the old last slot so reference types can be collected.
        _items[--_count] = default!;

        return value;
    }

    /// <summary>Expands the backing array when the requested size no longer fits.</summary>
    protected void EnsureCapacity(int required)
    {
        if (required <= _items.Length)
            return;

        // Doubling gives append operations an amortized O(1) cost.
        var capacity = _items.Length == 0 ? 4 : _items.Length;

        while (capacity < required)
            capacity *= 2;

        Array.Resize(ref _items, capacity);
    }

    private void ValidateIndex(int index)
    {
        // Converting both operands to uint makes negative indexes fail the same check.
        if ((uint)index >= (uint)_count)
            throw new ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>Enumerates only the logical portion of the backing array.</summary>
    public IEnumerator<T> GetEnumerator()
    {
        for (var index = 0; index < _count; index++)
            yield return _items[index];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
