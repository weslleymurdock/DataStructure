using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Selection sort scans the remaining unsorted region, selects its minimum,
/// and places that minimum at the next output position.
/// Best, average and worst cases are O(n²).
/// </summary>
public sealed class SelectionSort
{
    private readonly Action<IReadOnlyList<int>>? _onStep;

    /// <summary>Creates the algorithm and optionally enables step notifications.</summary>
    public SelectionSort(Action<IReadOnlyList<int>>? onStep = null)
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

        for (var position = 0; position < count - 1; position++)
        {
            // Assume the first remaining element is the minimum.
            var minimum = position;

            // Scan the rest of the unsorted region for a smaller value.
            for (var index = position + 1; index < count; index++)
            {
                if (get(index) < get(minimum))
                    minimum = index;
            }

            // Move the selected minimum to its final position.
            if (minimum == position)
                continue;

            var current = get(position);
            set(position, get(minimum));
            set(minimum, current);

            Notify(count, get);
        }

        return new(count, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortCopy(int[] values)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        for (var position = 0; position < values.Length - 1; position++)
        {
            var minimum = position;

            for (var index = position + 1; index < values.Length; index++)
            {
                if (values[index] < values[minimum])
                    minimum = index;
            }

            if (minimum == position)
                continue;

            (values[position], values[minimum]) =
                (values[minimum], values[position]);

            _onStep?.Invoke(values);
        }

        return new(values.Length, stopwatch.Elapsed);
    }

    private AlgorithmResult<int> SortQueue(DSQueue<int> data)
    {
        var values = Materialize(data);
        var sorted = values.ToArray();
        var result = SortCopy(sorted);

        foreach (var value in sorted)
            data.Enqueue(value);

        return result;
    }

    private AlgorithmResult<int> SortStack(DSStack<int> data)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.Pop());

        for (var position = 0; position < values.Count - 1; position++)
        {
            var minimum = position;

            for (var index = position + 1; index < values.Count; index++)
            {
                if (values[index] < values[minimum])
                    minimum = index;
            }

            if (minimum == position)
                continue;

            (values[position], values[minimum]) =
                (values[minimum], values[position]);

            _onStep?.Invoke(values);
        }

        for (var index = values.Count - 1; index >= 0; index--)
            data.Push(values[index]);

        return new(values.Count, stopwatch.Elapsed);
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

    private static List<int> Materialize(DSQueue<int> data)
    {
        var values = new List<int>();

        while (data.Count > 0)
            values.Add(data.Dequeue());

        return values;
    }

    /// <summary>Publishes a snapshot after a selection swap.</summary>
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
