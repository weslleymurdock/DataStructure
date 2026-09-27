namespace DataStructure.Abstractions;

/// <summary>
/// Circular singly linked list whose tail points back to the head.
/// </summary>
public sealed class DSCircularLinkedList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
            node.Next = node;
        }
        else
        {
            node.Next = _head;
            _head = node;
            _tail!.Next = _head;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
            node.Next = node;
        }
        else
        {
            node.Next = _head;
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException("The circular list is empty.");

        var value = _head.Value;

        if (Count == 1)
        {
            _head = _tail = null;
        }
        else
        {
            _head = _head.Next;
            _tail!.Next = _head;
        }

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException("The circular list is empty.");

        var value = _tail.Value;

        if (Count == 1)
        {
            _head = _tail = null;
        }
        else
        {
            var current = _head!;

            while (current.Next != _tail)
                current = current.Next!;

            current.Next = _head;
            _tail = current;
        }

        Count--;
        return value;
    }

    public bool Contains(T item)
    {
        if (_head is null)
            return false;

        var comparer = EqualityComparer<T>.Default;
        var current = _head;

        do
        {
            if (comparer.Equals(current.Value, item))
                return true;

            current = current.Next!;
        }
        while (current != _head);

        return false;
    }

    public IEnumerable<T> Enumerate()
    {
        if (_head is null)
            yield break;

        var current = _head;

        do
        {
            yield return current.Value;
            current = current.Next!;
        }
        while (current != _head);
    }
}
