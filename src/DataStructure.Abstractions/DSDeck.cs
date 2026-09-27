namespace DataStructure.Abstractions;

/// <summary>
/// Legacy double-ended queue implementation retained as a structural example.
/// Both ends are backed by explicit node references and operate in O(1).
/// </summary>
public sealed class DSDeck<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    /// <summary>Gets the number of values stored.</summary>
    public int Count { get; private set; }

    /// <summary>Adds a value at the beginning in O(1).</summary>
    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
            _head = _tail = node;
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    /// <summary>Adds a value at the end in O(1).</summary>
    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
            _head = _tail = node;
        else
        {
            _tail.Next = node;
            node.Previous = _tail;
            _tail = node;
        }

        Count++;
    }

    /// <summary>Removes and returns the first value in O(1).</summary>
    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException("The deck is empty.");

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
            throw new InvalidOperationException("The deck is empty.");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }
}
