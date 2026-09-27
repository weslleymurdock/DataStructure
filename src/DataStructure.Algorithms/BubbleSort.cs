using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Bubble sort repeatedly compares adjacent values and moves the largest
/// remaining value toward the end of the unsorted region.
/// Best case is O(n) with early exit; average and worst cases are O(n²).
/// </summary>
public sealed class BubbleSort
{
    private readonly Action<IReadOnlyList<int>>? _onStep;

    /// <summary>Creates the algorithm and optionally enables step notifications.</summary>
    public BubbleSort(Action<IReadOnlyList<int>>? onStep = null)
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

        // Each pass places the largest value of the remaining region at its end.
        for (var end = count - 1; end > 0; end--)
        {
            var changed = false;

            for (var index = 0; index < end; index++)
            {
                // Adjacent values are exchanged whenever they are inverted.
                if (get(index) <= get(index + 1))
                    continue;

                var left = get(index);
                var right = get(index + 1);

                set(index, right);
                set(index + 1, left);

                changed = true;
                Notify(count, get);
            }

            // If a complete pass made no exchange, the sequence is already sorted.
            if (!changed)
                break;
        }

        return new(count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortCopy(int[] values)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        for (var end = values.Length - 1; end > 0; end--)
        {
            var changed = false;

            for (var index = 0; index < end; index++)
            {
                if (values[index] <= values[index + 1])
                    continue;

                (values[index], values[index + 1]) =
                    (values[index + 1], values[index]);

                changed = true;
                _onStep?.Invoke(values);
            }

            if (!changed)
                break;
        }

        return new(values.Length, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortQueue(DSQueue<int> data)
    {
        var values = new List<int>();

        // A queue has no indexed write operation, so its values are materialized first.
        while (data.Count > 0)
            values.Add(data.Dequeue());

        var result = SortCopy([.. values]);

        // Restore the queue using the sorted sequence.
        foreach (var value in values)
            data.Enqueue(value);

        return result;
    }

    private AlgorithmResult<int> SortStack(DSStack<int> data)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var values = new List<int>();

        // Pop exposes the stack from top to bottom.
        while (data.Count > 0)
            values.Add(data.Pop());

        for (var end = values.Count - 1; end > 0; end--)
        {
            for (var index = 0; index < end; index++)
            {
                if (values[index] <= values[index + 1])
                    continue;

                (values[index], values[index + 1]) =
                    (values[index + 1], values[index]);

                _onStep?.Invoke(values);
            }
        }

        // Push in reverse so the stack exposes the sorted sequence from its top.
        for (var index = values.Count - 1; index >= 0; index--)
            data.Push(values[index]);

        return new(values.Count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortDeque(DSDeque<int> data)
    {
        var values = new List<int>();

        // Materialize the deque from its front because indexed sorting is unavailable.
        while (data.Count > 0)
            values.Add(data.RemoveFirst());

        var result = SortCopy([.. values]);

        foreach (var value in values)
            data.AddLast(value);

        return result;
    }

    /// <summary>Publishes a snapshot after an in-place mutation.</summary>
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
