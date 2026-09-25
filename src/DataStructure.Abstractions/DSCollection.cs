namespace DataStructure.Abstractions;

/// <summary>
/// General-purpose dynamically sized collection backed by a contiguous array.
/// </summary>
public class DSCollection<T> : ICollection<T>
{
    private T[] _items;
    private int _count;

    public DSCollection(int capacity = 4)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        _items = capacity == 0 ? [] : new T[capacity];
    }

    public int Count => _count;

    public bool IsReadOnly => false;

    public virtual void Add(T item)
    {
        EnsureCapacity(_count + 1);
        _items[_count++] = item;
    }

    public void Clear()
    {
        Array.Clear(_items, 0, _count);
        _count = 0;
    }

    public bool Contains(T item) => IndexOf(item) >= 0;

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 || arrayIndex > array.Length || array.Length - arrayIndex < _count)
            throw new ArgumentException("The destination array is too small.", nameof(array));

        Array.Copy(_items, 0, array, arrayIndex, _count);
    }

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index < 0)
            return false;

        RemoveAt(index);
        return true;
    }

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

    protected int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = 0; i < _count; i++)
            if (comparer.Equals(_items[i], item))
                return i;

        return -1;
    }

    protected void InsertAt(int index, T item)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        EnsureCapacity(_count + 1);
        Array.Copy(_items, index, _items, index + 1, _count - index);
        _items[index] = item;
        _count++;
    }

    protected T RemoveAt(int index)
    {
        ValidateIndex(index);
        var value = _items[index];

        Array.Copy(_items, index + 1, _items, index, _count - index - 1);
        _items[--_count] = default!;
        return value;
    }

    protected void EnsureCapacity(int required)
    {
        if (required <= _items.Length)
            return;

        var capacity = _items.Length == 0 ? 4 : _items.Length;
        while (capacity < required)
            capacity *= 2;

        Array.Resize(ref _items, capacity);
    }

    private void ValidateIndex(int index)
    {
        if ((uint)index >= (uint)_count)
            throw new ArgumentOutOfRangeException(nameof(index));
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < _count; i++)
            yield return _items[i];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
