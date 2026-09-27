namespace DataStructure.Abstractions;

/// <summary>
/// Doubly linked list with O(1) insertion/removal at either end.
/// Indexed access is O(n), although traversal chooses the closest end.
/// </summary>
public sealed class DSLinkedList<T> : IReadOnlyList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    /// <summary>Gets the number of nodes in the list.</summary>
    public int Count { get; private set; }

    /// <summary>Gets a value by walking from the closest end. Access is O(n).</summary>
    public T this[int index]
        => GetNode(index).Value;

    /// <summary>Adds a node before the current head in O(1).</summary>
    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            // The first node is simultaneously both ends of the list.
            _head = _tail = node;
        }
        else
        {
            // Link the new node to the old head in both directions.
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    /// <summary>Adds a node after the current tail in O(1).</summary>
    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {
            // Link the old tail forward and the new node backward.
            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    /// <summary>Removes and returns the head node in O(1).</summary>
    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException("The linked list is empty.");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    /// <summary>Removes and returns the tail node in O(1).</summary>
    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException("The linked list is empty.");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }

    /// <summary>Inserts a node at an index. Finding the position costs O(n).</summary>
    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index == 0)
        {
            AddFirst(item);
            return;
        }

        if (index == Count)
        {
            AddLast(item);
            return;
        }

        // Find the node currently occupying the target position.
        var current = GetNode(index);
        var node = new DSNode<T>(item)
        {
            Previous = current.Previous,
            Next = current
        };

        // Reconnect both neighboring nodes around the inserted node.
        current.Previous!.Next = node;
        current.Previous = node;
        Count++;
    }

    /// <summary>Removes the first matching node and reconnects its neighbors.</summary>
    public bool Remove(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        var current = _head;

        while (current is not null)
        {
            if (comparer.Equals(current.Value, item))
            {
                if (current.Previous is null)
                    RemoveFirst();
                else if (current.Next is null)
                    RemoveLast();
                else
                {
                    // Bypass the node by connecting its two neighbors directly.
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    Count--;
                }

                return true;
            }

            current = current.Next;
        }

        return false;
    }

    private DSNode<T> GetNode(int index)
    {
        if ((uint)index >= (uint)Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        // Start at the head when the requested index is in the first half.
        if (index < Count / 2)
        {
            var current = _head!;

            for (var position = 0; position < index; position++)
                current = current.Next!;

            return current;
        }

        // Otherwise walk backward from the tail.
        var reverse = _tail!;

        for (var position = Count - 1; position > index; position--)
            reverse = reverse.Previous!;

        return reverse;
    }

    /// <summary>Enumerates nodes from head to tail.</summary>
    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;

        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
