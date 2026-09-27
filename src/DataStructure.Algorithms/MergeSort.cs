using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Merge sort divides the sequence into halves, sorts both halves recursively,
/// and merges the ordered halves. Best, average and worst cases are O(n log n),
/// with O(n) auxiliary memory.
/// </summary>
public sealed class MergeSort
{
    private readonly Action<IReadOnlyList<int>>? _onStep;

    /// <summary>Creates the algorithm and optionally enables step notifications.</summary>
    public MergeSort(Action<IReadOnlyList<int>>? onStep = null)
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

        // Materialize the indexed structure so recursive merging can use contiguous memory.
        for (var index = 0; index < count; index++)
            values[index] = get(index);

        Merge(values, 0, values.Length - 1);

        // Copy the ordered result back to the original structure.
        for (var index = 0; index < count; index++)
            set(index, values[index]);

        _onStep?.Invoke(values);

        return new(count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortCopy(int[] values)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        Merge(values, 0, values.Length - 1);

        return new(values.Length, stopwatch.Elapsed);
    }

    private void Merge(int[] values, int low, int high)
    {
        // A single element is already sorted.
        if (low >= high)
            return;

        // Splitting at the midpoint guarantees logarithmic recursion depth.
        var middle = low + (high - low) / 2;

        Merge(values, low, middle);
        Merge(values, middle + 1, high);

        var temporary = new int[high - low + 1];
        var left = low;
        var right = middle + 1;
        var target = 0;

        // Select the smallest head from the two sorted halves.
        while (left <= middle && right <= high)
        {
            if (values[left] <= values[right])
                temporary[target++] = values[left++];
            else
                temporary[target++] = values[right++];
        }

        // Copy any remaining values from the left half.
        while (left <= middle)
            temporary[target++] = values[left++];

        // Copy any remaining values from the right half.
        while (right <= high)
            temporary[target++] = values[right++];

        // Replace the original range with its merged sorted representation.
        Array.Copy(temporary, 0, values, low, temporary.Length);
        _onStep?.Invoke(values);
    }

    private AlgorithmResult<int> SortQueue(DSQueue<int> data)
    {
        var values = Materialize(data);
        var result = SortCopy([.. values]);

        foreach (var value in values)
            data.Enqueue(value);

        return result;
    }

    private AlgorithmResult<int> SortStack(DSStack<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.Pop());

        var result = SortCopy([.. values]);

        for (var index = values.Count - 1; index >= 0; index--)
            data.Push(values[index]);

        return result;
    }

    private AlgorithmResult<int> SortDeque(DSDeque<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.RemoveFirst());

        var result = SortCopy([.. values]);

        foreach (var value in values)
            data.AddLast(value);

        return result;
    }

    private static List<int> Materialize(DSQueue<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.Dequeue());

        return values;
    }
}
