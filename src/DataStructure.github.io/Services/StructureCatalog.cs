namespace DataStructure.github.io.Services;

public sealed record MethodGuide(
    string Name,
    string Signature,
    string Explanation,
    string Complexity);

public sealed record StructureGuide(
    string Key,
    string Name,
    string Description,
    string UseCase,
    string Source,
    IReadOnlyList<MethodGuide> Methods);

public static class StructureCatalog
{
    public static IReadOnlyList<StructureGuide> All { get; } =
    [
        new(
            "array",
            "DSArray<T>",
            "Fixed-size, zero-based storage backed by a contiguous array.",
            "Use when the size is known and indexed O(1) access matters.",
            """
namespace DataStructure.Abstractions;

public sealed class DSArray<T> : IReadOnlyList<T>
{
    private readonly T[] _items;

    public DSArray(int length)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        _items = new T[length];
    }

    public DSArray(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = [.. items];
    }

    public int Count => _items.Length;

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public IEnumerator<T> GetEnumerator()
        => ((IEnumerable<T>)_items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
""",
            [
                new("DSArray(int)", "public DSArray(int length)", "Allocates the complete fixed-size storage.", "O(n) allocation"),
                new("DSArray(IEnumerable<T>)", "public DSArray(IEnumerable<T> items)", "Materializes the input into contiguous storage.", "O(n)"),
                new("Count", "public int Count", "Returns the fixed number of positions.", "O(1)"),
                new("indexer", "public T this[int index]", "Reads or replaces a value at a zero-based position.", "O(1)")
            ]),
        new(
            "collection",
            "DSCollection<T>",
            "Dynamically sized contiguous collection with amortized constant-time append.",
            "Use as a general-purpose mutable collection when indexed access is useful.",
            """
namespace DataStructure.Abstractions;

public class DSCollection<T> : ICollection<T>
{
    private T[] _items;
    private int _count;

    public DSCollection(int capacity = 4)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
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

        if (arrayIndex < 0 ||
            arrayIndex > array.Length ||
            array.Length - arrayIndex < _count)
        {
            throw new ArgumentException("The destination array is too small.", nameof(array));
        }

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

        for (var index = 0; index < _count; index++)
        {
            if (comparer.Equals(_items[index], item))
                return index;
        }

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
        for (var index = 0; index < _count; index++)
            yield return _items[index];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
""",
            [
                new("Add", "public virtual void Add(T item)", "Ensures capacity and writes the item into the next logical slot.", "Amortized O(1)"),
                new("Clear", "public void Clear()", "Clears the logical range and resets Count.", "O(n)"),
                new("Contains", "public bool Contains(T item)", "Uses IndexOf to perform a linear membership check.", "O(n)"),
                new("CopyTo", "public void CopyTo(T[] array, int arrayIndex)", "Copies the logical range to a destination array.", "O(n)"),
                new("Remove", "public bool Remove(T item)", "Finds and removes the first matching item.", "O(n)"),
                new("indexer", "public T this[int index]", "Validates and accesses an existing logical position.", "O(1)"),
                new("InsertAt", "protected void InsertAt(int index, T item)", "Creates a position by shifting the suffix right.", "O(n)"),
                new("RemoveAt", "protected T RemoveAt(int index)", "Removes a position and shifts the suffix left.", "O(n)"),
                new("EnsureCapacity", "protected void EnsureCapacity(int required)", "Doubles backing capacity until the requested size fits.", "Amortized O(1) growth")
            ]),
        new(
            "list",
            "DSList<T>",
            "Indexed dynamic list derived from DSCollection<T>.",
            "Use when indexed reads and insertion/removal by index are both required.",
            """
namespace DataStructure.Abstractions;

public sealed class DSList<T>(int capacity = 4)
    : DSCollection<T>(capacity), IReadOnlyList<T>
{
    public void Insert(int index, T item)
        => InsertAt(index, item);

    public new T RemoveAt(int index)
        => base.RemoveAt(index);

    public new int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < Count; index++)
        {
            if (comparer.Equals(this[index], item))
                return index;
        }

        return -1;
    }

    public new bool Contains(T item)
        => IndexOf(item) >= 0;
}
""",
            [
                new("Insert", "public void Insert(int index, T item)", "Delegates to the protected collection insertion operation.", "O(n)"),
                new("RemoveAt", "public new T RemoveAt(int index)", "Removes and returns an indexed item.", "O(n)"),
                new("IndexOf", "public new int IndexOf(T item)", "Scans the logical list for the first matching item.", "O(n)"),
                new("Contains", "public new bool Contains(T item)", "Checks membership through IndexOf.", "O(n)")
            ]),
        new(
            "linkedlist",
            "DSLinkedList<T>",
            "Doubly linked list with explicit head and tail nodes.",
            "Use when frequent insertion/removal at either end is more important than indexed access.",
            """
namespace DataStructure.Abstractions;

public sealed class DSLinkedList<T> : IReadOnlyList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public T this[int index] => GetNode(index).Value;

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

            for (var position = 0; position < index; position++)
                current = current.Next!;

            return current;
        }

        var reverse = _tail!;

        for (var position = Count - 1; position > index; position--)
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

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
""",
            [
                new("AddFirst", "public void AddFirst(T item)", "Creates a node before the current head and fixes both links.", "O(1)"),
                new("AddLast", "public void AddLast(T item)", "Creates a node after the current tail and fixes both links.", "O(1)"),
                new("RemoveFirst", "public T RemoveFirst()", "Moves head to the next node and detaches the old head.", "O(1)"),
                new("RemoveLast", "public T RemoveLast()", "Moves tail to the previous node and detaches the old tail.", "O(1)"),
                new("Insert", "public void Insert(int index, T item)", "Finds a position and reconnects the surrounding nodes.", "O(n)"),
                new("Remove", "public bool Remove(T item)", "Searches for a matching node and bypasses it.", "O(n)"),
                new("indexer", "public T this[int index]", "Walks from the closest end to locate a node.", "O(n)")
            ]),
        new(
            "nodelist",
            "DSNodeList<T>",
            "Simple singly linked list retained as a teaching example.",
            "Use to study one-directional node chains and traversal cost.",
            """
namespace DataStructure.Abstractions;

public sealed class DSNodeList<T> where T : notnull
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void Add(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
            _head = _tail = node;
        else
        {
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

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

    public bool Remove(T item)
    {
        if (_head is null)
            return false;

        if (_head.Value.Equals(item))
        {
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

            current.Next = current.Next.Next;

            if (current.Next is null)
                _tail = current;

            Count--;
            return true;
        }

        return false;
    }
}
""",
            [
                new("Add", "public void Add(T item)", "Appends a node and advances the tail.", "O(1)"),
                new("indexer", "public T this[int index]", "Walks from the head until the requested node.", "O(n)"),
                new("Remove", "public bool Remove(T item)", "Finds the predecessor and bypasses the matching node.", "O(n)")
            ]),
        new(
            "queue",
            "DSQueue<T>",
            "FIFO queue implemented with head and tail pointers.",
            "Use when the oldest inserted item must be processed first.",
            """
namespace DataStructure.Abstractions;

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
""",
            [
                new("Enqueue", "public void Enqueue(T item)", "Adds a node after the tail.", "O(1)"),
                new("Dequeue", "public T Dequeue()", "Removes the node at the head.", "O(1)"),
                new("Peek", "public T Peek()", "Reads the oldest value without removing it.", "O(1)")
            ]),
        new(
            "stack",
            "DSStack<T>",
            "LIFO stack implemented with a singly linked chain.",
            "Use when the most recently pushed value must be processed first.",
            """
namespace DataStructure.Abstractions;

public sealed class DSStack<T>
{
    private DSNode<T>? _top;

    public int Count { get; private set; }

    public void Push(T item)
    {
        var node = new DSNode<T>(item)
        {
            Next = _top
        };

        _top = node;
        Count++;
    }

    public T Pop()
    {
        if (_top is null)
            throw new InvalidOperationException("The stack is empty.");

        var value = _top.Value;
        _top = _top.Next;
        Count--;
        return value;
    }

    public T Peek()
    {
        if (_top is null)
            throw new InvalidOperationException("The stack is empty.");

        return _top.Value;
    }
}
""",
            [
                new("Push", "public void Push(T item)", "Creates a node pointing to the previous top.", "O(1)"),
                new("Pop", "public T Pop()", "Moves the top reference to the next node.", "O(1)"),
                new("Peek", "public T Peek()", "Reads the current top without mutation.", "O(1)")
            ]),
        new(
            "deque",
            "DSDeque<T>",
            "Double-ended queue backed by a doubly linked chain.",
            "Use when both ends need O(1) insertion and removal.",
            """
namespace DataStructure.Abstractions;

public sealed class DSDeque<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

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

    public T PeekFirst()
        => _head?.Value ?? throw new InvalidOperationException("The deque is empty.");

    public T PeekLast()
        => _tail?.Value ?? throw new InvalidOperationException("The deque is empty.");
}
""",
            [
                new("AddFirst", "public void AddFirst(T item)", "Links a new node before the head.", "O(1)"),
                new("AddLast", "public void AddLast(T item)", "Links a new node after the tail.", "O(1)"),
                new("RemoveFirst", "public T RemoveFirst()", "Removes the head and advances it.", "O(1)"),
                new("RemoveLast", "public T RemoveLast()", "Removes the tail and moves it backward.", "O(1)"),
                new("PeekFirst", "public T PeekFirst()", "Reads the first value without mutation.", "O(1)"),
                new("PeekLast", "public T PeekLast()", "Reads the last value without mutation.", "O(1)")
            ]),
        new(
            "deck",
            "DSDeck<T>",
            "Legacy double-ended node structure retained as a structural example.",
            "Use to study bidirectional endpoint mechanics.",
            """
namespace DataStructure.Abstractions;

public sealed class DSDeck<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

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
            _tail.Next = node;
            node.Previous = _tail;
            _tail = node;
        }

        Count++;
    }

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
""",
            [
                new("AddFirst", "public void AddFirst(T item)", "Inserts a node before the head.", "O(1)"),
                new("AddLast", "public void AddLast(T item)", "Inserts a node after the tail.", "O(1)"),
                new("RemoveFirst", "public T RemoveFirst()", "Removes the head node.", "O(1)"),
                new("RemoveLast", "public T RemoveLast()", "Removes the tail node.", "O(1)")
            ]),
        new(
            "priorityqueue",
            "DSPriorityQueue<T>",
            "Min-priority queue backed by a binary heap.",
            "Use when the next item must always be the smallest priority value.",
            """
namespace DataStructure.Abstractions;

public sealed class DSPriorityQueue<T> where T : IComparable<T>
{
    private readonly DSList<T> _heap = [];

    public int Count => _heap.Count;

    public void Enqueue(T item)
    {
        _heap.Add(item);
        SiftUp(_heap.Count - 1);
    }

    public T Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        return _heap[0];
    }

    public T Dequeue()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        var result = _heap[0];
        var last = _heap[^1];
        _heap.RemoveAt(_heap.Count - 1);

        if (_heap.Count > 0)
        {
            _heap[0] = last;
            SiftDown(0);
        }

        return result;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parent = (index - 1) / 2;

            if (_heap[parent].CompareTo(_heap[index]) <= 0)
                break;

            (_heap[parent], _heap[index]) =
                (_heap[index], _heap[parent]);

            index = parent;
        }
    }

    private void SiftDown(int index)
    {
        while (true)
        {
            var left = index * 2 + 1;
            var right = left + 1;
            var smallest = index;

            if (_heap.Count > left &&
                _heap[left].CompareTo(_heap[smallest]) < 0)
                smallest = left;

            if (_heap.Count > right &&
                _heap[right].CompareTo(_heap[smallest]) < 0)
                smallest = right;

            if (smallest == index)
                return;

            (_heap[index], _heap[smallest]) =
                (_heap[smallest], _heap[index]);

            index = smallest;
        }
    }
}
""",
            [
                new("Enqueue", "public void Enqueue(T item)", "Adds a value and moves it upward until the min-heap property is restored.", "O(log n)"),
                new("Peek", "public T Peek()", "Returns the root, which is the smallest priority.", "O(1)"),
                new("Dequeue", "public T Dequeue()", "Removes the root and restores heap order downward.", "O(log n)"),
                new("SiftUp", "private void SiftUp(int index)", "Compares a node with its parent until heap order is valid.", "O(log n)"),
                new("SiftDown", "private void SiftDown(int index)", "Moves a node toward the correct child position.", "O(log n)")
            ])
    ];

    public static StructureGuide Get(string key)
        => All.FirstOrDefault(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
            ?? All[0];

    public static string Normalize(string? key)
        => Get(key ?? All[0].Key).Key;
}
