using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Jump search requires sorted indexed data. It jumps by approximately sqrt(n)
/// positions and then performs a linear scan inside the candidate block.
/// Average and worst cases are O(sqrt(n)).
/// </summary>
public sealed class JumpSearch
{
    public AlgorithmResult<int> ExecuteDSArray(
        DSArray<int> data,
        int target)
        => Run(data.Count, index => data[index], target);

    public AlgorithmResult<int> ExecuteDSList(
        DSList<int> data,
        int target)
        => Run(data.Count, index => data[index], target);

    public AlgorithmResult<int> ExecuteDSLinkedList(
        DSLinkedList<int> data,
        int target)
        => Run(data.Count, index => data[index], target);

    public AlgorithmResult<int> ExecuteDSCollection(
        DSCollection<int> data,
        int target)
        => Run(data.Count, index => data[index], target);

    private static AlgorithmResult<int> Run(
        int count,
        Func<int, int> get,
        int target)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        if (count == 0)
            return new(-1, stopwatch.Elapsed);

        // sqrt(n) balances the number of jumps with the size of the final scan.
        var step = Math.Max(1, (int)Math.Sqrt(count));
        var previous = 0;
        var next = step;

        // Jump forward while the last value of the current block is too small.
        while (previous < count &&
               get(Math.Min(next, count) - 1) < target)
        {
            previous = next;
            next += step;

            if (previous >= count)
                return new(-1, stopwatch.Elapsed);
        }

        // Once the target's block is identified, inspect only that block.
        for (var index = previous; index < Math.Min(next, count); index++)
        {
            var value = get(index);

            if (value == target)
                return new(index, stopwatch.Elapsed);

            if (value > target)
                break;
        }

        return new(-1, stopwatch.Elapsed);
    }
}
