using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Insertion sort grows a sorted prefix by shifting larger values to the right
/// and inserting the next value into the resulting gap.
/// Best case is O(n); average and worst cases are O(n²).
/// </summary>
/// <remarks>Creates the algorithm and optionally enables step notifications.</remarks>
public sealed class InsertionSort(Action<IReadOnlyList<int>>? onStep = null)
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

    private AlgorithmResult<int> Sort(
        int count,
        Func<int, int> get,
        Action<int, int> set)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // Index zero is already a sorted prefix of one element.
        for (var index = 1; index < count; index++)
        {
            var value = get(index);
            var position = index - 1;

            // Shift every larger prefix value one position to the right.
            while (position >= 0 && get(position) > value)
            {
                set(position + 1, get(position));
                position--;

                Notify(count, get);
            }

            // The gap is now the correct position for the extracted value.
            set(position + 1, value);
            Notify(count, get);
        }

        return new(count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortCopy(int[] values)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        for (var index = 1; index < values.Length; index++)
        {
            var value = values[index];
            var position = index - 1;

            while (position >= 0 && values[position] > value)
            {
                values[position + 1] = values[position];
                position--;

                _onStep?.Invoke(values);
            }

            values[position + 1] = value;
            _onStep?.Invoke(values);
        }

        return new(values.Length, stopwatch.Elapsed);
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

    /// <summary>Publishes a snapshot after a shift or insertion.</summary>
    private void Notify(int count, Func<int, int> get)
    {
        if (_onStep is null)
            return;

        var snapshot = new int[count];

        for (var index = 0; index < count; index++)
            snapshot[index] = get(index);

        _onStep(snapshot);
    }
}
