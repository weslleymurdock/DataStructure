using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Quick sort chooses a pivot, partitions values around it, and recursively
/// sorts the resulting ranges. Average time is O(n log n), worst time is O(n²).
/// </summary>
public sealed class QuickSort
{
    private readonly Action<IReadOnlyList<int>>? _onStep;

    /// <summary>Creates the algorithm and optionally enables step notifications.</summary>
    public QuickSort(Action<IReadOnlyList<int>>? onStep = null)
    {
        _onStep = onStep;
    }

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

    private AlgorithmResult<int> Sort(
        int count,
        Func<int, int> get,
        Action<int, int> set)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var values = new int[count];

        for (var index = 0; index < count; index++)
            values[index] = get(index);

        Quick(values, 0, values.Length - 1);

        for (var index = 0; index < count; index++)
            set(index, values[index]);

        _onStep?.Invoke(values);

        return new(count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortCopy(int[] values)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        Quick(values, 0, values.Length - 1);

        return new(values.Length, stopwatch.Elapsed);
    }

    private void Quick(int[] values, int low, int high)
    {
        // A range containing zero or one element is already sorted.
        if (low >= high)
            return;

        // This implementation deliberately uses the last value as pivot.
        // Ordered input can therefore produce the O(n²) worst case.
        var pivotIndex = Partition(values, low, high);

        Quick(values, low, pivotIndex - 1);
        Quick(values, pivotIndex + 1, high);
    }

    private int Partition(int[] values, int low, int high)
    {
        var pivot = values[high];
        var smaller = low;

        // Move every value less than or equal to the pivot into the left partition.
        for (var index = low; index < high; index++)
        {
            if (values[index] > pivot)
                continue;

            (values[smaller], values[index]) =
                (values[index], values[smaller]);

            smaller++;
            _onStep?.Invoke(values);
        }

        // Put the pivot between the two partitions.
        (values[smaller], values[high]) =
            (values[high], values[smaller]);

        _onStep?.Invoke(values);

        return smaller;
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
