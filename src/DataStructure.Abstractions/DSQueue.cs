namespace DataStructure.Abstractions;

/// <summary>
/// FIFO queue implemented with head and tail pointers.
/// Enqueue and dequeue are O(1).
/// </summary>
public sealed class DSQueue<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
            _head = _tail = node;
        else
        {
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T Dequeue()
    {
        if (_head is null)
            throw new InvalidOperationException("The queue is empty.");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;

        Count--;
        return value;
    }

    public T Peek()
    {
        if (_head is null)
            throw new InvalidOperationException("The queue is empty.");

        return _head.Value;
    }
}
