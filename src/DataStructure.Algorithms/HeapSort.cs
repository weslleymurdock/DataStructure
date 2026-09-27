using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Heap sort builds a max-heap, repeatedly moves its root to the end,
/// and restores the heap property. All cases are O(n log n) with O(1)
/// auxiliary space for the sorting array.
/// </summary>
/// <remarks>Creates the algorithm and optionally enables step notifications.</remarks>
public sealed class HeapSort(Action<IReadOnlyList<int>>? onStep = null)
{
    private readonly Action<IReadOnlyList<int>>? _onStep = onStep;

    public AlgorithmResult<int> ExecuteDSArray(DSArray<int> data)
        => Sort(data.Count, index => data[index], (index, value) => data[index] = value);

    public AlgorithmResult<int> ExecuteDSList(DSList<int> data)
        => Sort(data.Count, index => data[index], (index, value) => data[index] = value);

    public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int> data)
        => SortCopy([.. data]);

    public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int> data)
        => SortCopy([.. data]);

    public AlgorithmResult<int> ExecuteDSQueue(DSQueue<int> data)
        => SortQueue(data);

    public AlgorithmResult<int> ExecuteDSStack(DSStack<int> data)
        => SortStack(data);

    public AlgorithmResult<int> ExecuteDSDeque(DSDeque<int> data)
        => SortDeque(data);

    public AlgorithmResult<int> ExecuteDSMaxHeap(DSMaxHeap<int> data)
        => SortCopy([.. data.AsArray()]);

    private AlgorithmResult<int> Sort(
        int count,
        Func<int, int> get,
        Action<int, int> set)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var values = new int[count];

        for (var index = 0; index < count; index++)
            values[index] = get(index);

        Heap(values);

        for (var index = 0; index < count; index++)
            set(index, values[index]);

        _onStep?.Invoke(values);

        return new(count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortCopy(int[] values)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        Heap(values);

        return new(values.Length, stopwatch.Elapsed);
    }

    private void Heap(int[] values)
    {
        // Every internal node is processed bottom-up to construct the max-heap.
        for (var index = values.Length / 2 - 1; index >= 0; index--)
            SiftDown(values, index, values.Length);

        // The root is the largest element, so place it at the end.
        for (var end = values.Length - 1; end > 0; end--)
        {
            (values[0], values[end]) = (values[end], values[0]);
            _onStep?.Invoke(values);

            // The remaining prefix no longer contains the extracted maximum.
            SiftDown(values, 0, end);
        }
    }

    private void SiftDown(int[] values, int root, int length)
    {
        while (true)
        {
            var left = root * 2 + 1;

            // A node without a left child is a leaf and already satisfies the heap property.
            if (left >= length)
                return;

            var right = left + 1;
            var largest = left;

            // Choose the larger child as the candidate parent replacement.
            if (right < length && values[right] > values[left])
                largest = right;

            // The heap property is already satisfied.
            if (values[root] >= values[largest])
                return;

            (values[root], values[largest]) =
                (values[largest], values[root]);

            _onStep?.Invoke(values);
            root = largest;
        }
    }

    private AlgorithmResult<int> SortQueue(DSQueue<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.Dequeue());

        var sorted = values.ToArray();
        var result = SortCopy(sorted);

        foreach (var value in sorted)
            data.Enqueue(value);

        return result;
    }

    private AlgorithmResult<int> SortStack(DSStack<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.Pop());

        var sorted = values.ToArray();
        var result = SortCopy(sorted);

        for (var index = sorted.Length - 1; index >= 0; index--)
            data.Push(sorted[index]);

        return result;
    }

    private AlgorithmResult<int> SortDeque(DSDeque<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.RemoveFirst());

        var sorted = values.ToArray();
        var result = SortCopy(sorted);

        foreach (var value in sorted)
            data.AddLast(value);

        return result;
    }
}
