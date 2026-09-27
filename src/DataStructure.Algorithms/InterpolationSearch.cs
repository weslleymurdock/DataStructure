using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>Interpolation search for sorted, approximately uniformly distributed integers. Average O(log log n), worst O(n); space O(1).</summary>
public sealed class InterpolationSearch
{
    public AlgorithmResult<int> ExecuteDSArray(DSArray<int> d, int target) => Run(d.Count, i => d[i], target);
    public AlgorithmResult<int> ExecuteDSList(DSList<int> d, int target) => Run(d.Count, i => d[i], target);
    public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int> d, int target) => Run(d.Count, i => d[i], target);
    public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int> d, int target) => Run(d.Count, i => d[i], target);

    private static AlgorithmResult<int> Run(int n, Func<int, int> get, int target) 
    {
        var s = System.Diagnostics.Stopwatch.StartNew(); 
        var lo = 0; 
        var hi = n - 1; 
        while (lo <= hi && target >= get(lo) && target <= get(hi)) 
        {
            var low = get(lo); 
            var high = get(hi);
            if (low == high) return new(low == target ? lo : -1, s.Elapsed);
            var pos = lo + (int)((long)(target - low) * (hi - lo) / (high - low)); 
            var value = get(pos); 
            if (value == target) return new(pos, s.Elapsed);
            if (value < target) lo = pos + 1; 
            else hi = pos - 1;
        }
        return new(-1, s.Elapsed);
    }
}