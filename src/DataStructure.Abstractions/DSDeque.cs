namespace DataStructure.Abstractions;

/// <summary>
/// Double-ended queue backed by a doubly linked chain.
/// Both ends support insertion and removal in O(1).
/// </summary>
public sealed class DSDeque<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    /// <summary>Gets the number of values currently stored.</summary>
    public int Count { get; private set; }

    /// <summary>Adds a value before the head in O(1).</summary>
    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
        }
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    /// <summary>Adds a value after the tail in O(1).</summary>
    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {
            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    /// <summary>Removes and returns the first value in O(1).</summary>
    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException("The deque is empty.");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    /// <summary>Removes and returns the last value in O(1).</summary>
    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException("The deque is empty.");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }

    /// <summary>Reads the first value without removing it.</summary>
    public T PeekFirst()
    {
        if (_head is null)
            throw new InvalidOperationException("The deque is empty.");

        return _head.Value;
    }

    /// <summary>Reads the last value without removing it.</summary>
    public T PeekLast()
    {
        if (_tail is null)
            throw new InvalidOperationException("The deque is empty.");

        return _tail.Value;
    }
}
