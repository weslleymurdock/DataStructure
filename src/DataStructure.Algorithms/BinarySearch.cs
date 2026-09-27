using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Binary search on sorted data. Each comparison discards half of the remaining
/// range, producing O(log n) comparisons when indexed access is O(1).
/// </summary>
public sealed class BinarySearch
{
    public AlgorithmResult<int> ExecuteDSArray(DSArray<int> data, int target)
        => Run(data.Count, index => data[index], target);

    public AlgorithmResult<int> ExecuteDSList(DSList<int> data, int target)
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
        var low = 0;
        var high = count - 1;

        while (low <= high)
        {
            // The midpoint avoids scanning either half of the range.
            var middle = low + (high - low) / 2;
            var value = get(middle);

            if (value == target)
                return new(middle, stopwatch.Elapsed);

            if (value < target)
            {
                // The target, if present, must be to the right of the midpoint.
                low = middle + 1;
            }
            else
            {
                // The target, if present, must be to the left of the midpoint.
                high = middle - 1;
            }
        }

        return new(-1, stopwatch.Elapsed);
    }
}
