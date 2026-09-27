using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Linear search visits values in their natural traversal order until the
/// target is found. It requires no ordering and has O(n) worst-case time.
/// </summary>
public sealed class LinearSearch
{
    public AlgorithmResult<int> ExecuteDSArray<T>(
        DSArray<T> data,
        T target)
        => Run(data, target);

    public AlgorithmResult<int> ExecuteDSList<T>(
        DSList<T> data,
        T target)
        => Run(data, target);

    public AlgorithmResult<int> ExecuteDSLinkedList<T>(
        DSLinkedList<T> data,
        T target)
        => Run(data, target);

    public AlgorithmResult<int> ExecuteDSCollection<T>(
        DSCollection<T> data,
        T target)
        => Run(data, target);

    public AlgorithmResult<int> ExecuteDSQueue<T>(
        DSQueue<T> data,
        T target)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var count = data.Count;
        var found = -1;

        for (var index = 0; index < count; index++)
        {
            // Dequeue exposes the oldest item; enqueue puts it back so the
            // queue has the same logical order after the search.
            var item = data.Dequeue();

            if (found < 0 &&
                EqualityComparer<T>.Default.Equals(item, target))
            {
                found = index;
            }

            data.Enqueue(item);
        }

        return new(found, stopwatch.Elapsed);
    }

    public AlgorithmResult<int> ExecuteDSStack<T>(
        DSStack<T> data,
        T target)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var buffer = new DSStack<T>();
        var found = -1;
        var index = 0;

        // Pop values into a second stack while searching from the top.
        while (data.Count > 0)
        {
            var item = data.Pop();

            if (found < 0 &&
                EqualityComparer<T>.Default.Equals(item, target))
            {
                found = index;
            }

            buffer.Push(item);
            index++;
        }

        // Reverse the temporary stack back into the original stack.
        while (buffer.Count > 0)
            data.Push(buffer.Pop());

        return new(found, stopwatch.Elapsed);
    }

    public AlgorithmResult<int> ExecuteDSDeque<T>(
        DSDeque<T> data,
        T target)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var count = data.Count;
        var found = -1;

        for (var index = 0; index < count; index++)
        {
            // Rotating the deque preserves the original order.
            var item = data.RemoveFirst();

            if (found < 0 &&
                EqualityComparer<T>.Default.Equals(item, target))
            {
                found = index;
            }

            data.AddLast(item);
        }

        return new(found, stopwatch.Elapsed);
    }

    public AlgorithmResult<int> ExecuteDSPriorityQueue<T>(
        DSPriorityQueue<T> data,
        T target)
        where T : IComparable<T>
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var buffer = new List<T>();
        var found = -1;
        var index = 0;

        // A priority queue exposes values by priority, not insertion order.
        // Dequeue every value to inspect that priority order.
        while (data.Count > 0)
        {
            var item = data.Dequeue();

            if (found < 0 && item.CompareTo(target) == 0)
                found = index;

            buffer.Add(item);
            index++;
        }

        // Reinsert every value to restore the heap.
        foreach (var item in buffer)
            data.Enqueue(item);

        return new(found, stopwatch.Elapsed);
    }

    public AlgorithmResult<int> ExecuteDSCircularLinkedList<T>(
        DSCircularLinkedList<T> data,
        T target)
        => Run(data.Enumerate(), target);

    public AlgorithmResult<int> ExecuteDSBinaryTree<T>(
        DSBinaryTree<T> data,
        T target)
        => Run(data.PreOrder(), target);

    public AlgorithmResult<int> ExecuteDSBinarySearchTree<T>(
        DSBinarySearchTree<T> data,
        T target)
        where T : IComparable<T>
        => Run(data.PreOrder(), target);

    public AlgorithmResult<int> ExecuteDSHeap<T>(
        DSHeap<T> data,
        T target)
        where T : IComparable<T>
        => Run(data.AsArray(), target);

    public AlgorithmResult<int> ExecuteDSGraph<T>(
        DSGraph<T> data,
        T target)
        where T : notnull
        => Run(data.Vertices, target);

    private static AlgorithmResult<int> Run<T>(
        IEnumerable<T> data,
        T target)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var index = 0;

        foreach (var item in data)
        {
            if (EqualityComparer<T>.Default.Equals(item, target))
                return new(index, stopwatch.Elapsed);

            index++;
        }

        return new(-1, stopwatch.Elapsed);
    }
}
