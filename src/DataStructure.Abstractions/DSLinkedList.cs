namespace DataStructure.Abstractions;

/// <summary>
/// Doubly linked list with O(1) insertion/removal at either end.
/// Indexed access is O(n).
/// </summary>
public sealed class DSLinkedList<T> : IReadOnlyList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public T this[int index]
    {
        get => GetNode(index).Value;
    }

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

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);
        if (_tail is null)
            _head = _tail = node;
        else
        {
            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

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

        var current = GetNode(index);
        var node = new DSNode<T>(item)
        {
            Previous = current.Previous,
            Next = current
        };

        current.Previous!.Next = node;
        current.Previous = node;
        Count++;
    }

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

        if (index < Count / 2)
        {
            var current = _head!;
            for (var i = 0; i < index; i++)
                current = current.Next!;

            return current;
        }

        var reverse = _tail!;
        for (var i = Count - 1; i > index; i--)
            reverse = reverse.Previous!;

        return reverse;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
