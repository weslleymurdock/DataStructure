namespace DataStructure.Abstractions;

/// <summary>
/// Simple singly linked list kept as a teaching example.
/// Indexed access is O(n); append is O(1) because a tail reference is retained.
/// </summary>
public sealed class DSNodeList<T> where T : notnull
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    /// <summary>Gets the number of nodes.</summary>
    public int Count { get; private set; }

    /// <summary>Adds a node to the tail in O(1).</summary>
    public void Add(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            // The first node represents both ends.
            _head = _tail = node;
        }
        else
        {
            // Link the old tail to the new node and advance the tail.
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    /// <summary>Reads a value by walking from the head in O(n).</summary>
    public T this[int index]
    {
        get
        {
            if ((uint)index >= (uint)Count)
                throw new IndexOutOfRangeException();

            var current = _head;

            for (var position = 0; position < index; position++)
                current = current!.Next;

            return current!.Value;
        }
    }

    /// <summary>Removes the first matching node by reconnecting the chain.</summary>
    public bool Remove(T item)
    {
        if (_head is null)
            return false;

        if (_head.Value.Equals(item))
        {
            // Removing the head only requires moving the head pointer.
            _head = _head.Next;

            if (_head is null)
                _tail = null;

            Count--;
            return true;
        }

        var current = _head;

        while (current.Next is not null)
        {
            if (!current.Next.Value.Equals(item))
            {
                current = current.Next;
                continue;
            }

            // Bypass the matching node.
            current.Next = current.Next.Next;

            if (current.Next is null)
                _tail = current;

            Count--;
            return true;
        }

        return false;
    }
}
