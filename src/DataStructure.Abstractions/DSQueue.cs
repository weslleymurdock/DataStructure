namespace DataStructure.Abstractions;

/// <summary>
/// FIFO queue implemented with head and tail pointers.
/// Enqueue and dequeue are O(1).
/// </summary>
public sealed class DSQueue<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    /// <summary>Gets the number of values currently stored.</summary>
    public int Count { get; private set; }

    /// <summary>Adds a value after the current tail in O(1).</summary>
    public void Enqueue(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            // The first item is both the head and the tail.
            _head = _tail = node;
        }
        else
        {
            // Append after the tail and move the tail pointer forward.
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    /// <summary>Removes and returns the oldest value in O(1).</summary>
    public T Dequeue()
    {
        if (_head is null)
            throw new InvalidOperationException("The queue is empty.");

        var value = _head.Value;

        // The next node becomes the new head.
        _head = _head.Next;

        if (_head is null)
            _tail = null;

        Count--;
        return value;
    }

    /// <summary>Returns the oldest value without removing it.</summary>
    public T Peek()
    {
        if (_head is null)
            throw new InvalidOperationException("The queue is empty.");

        return _head.Value;
    }
}
