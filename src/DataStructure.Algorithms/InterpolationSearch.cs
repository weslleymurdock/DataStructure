using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Interpolation search estimates the target position from the numeric range.
/// It can reach O(log log n) average time for approximately uniform data,
/// but degrades to O(n) with unfavorable distributions.
/// </summary>
public sealed class InterpolationSearch
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
        var low = 0;
        var high = count - 1;

        while (low <= high &&
               target >= get(low) &&
               target <= get(high))
        {
            var lowValue = get(low);
            var highValue = get(high);

            // If both endpoints are equal, every value in the range is equal.
            if (lowValue == highValue)
                return new(
                    lowValue == target ? low : -1,
                    stopwatch.Elapsed);

            // Estimate where the target should lie proportionally between endpoints.
            var position = low +
                (int)((long)(target - lowValue) *
                      (high - low) /
                      (highValue - lowValue));

            var value = get(position);

            if (value == target)
                return new(position, stopwatch.Elapsed);

            if (value < target)
            {
                // The target must be after the estimated position.
                low = position + 1;
            }
            else
            {
                // The target must be before the estimated position.
                high = position - 1;
            }
        }

        return new(-1, stopwatch.Elapsed);
    }
}
