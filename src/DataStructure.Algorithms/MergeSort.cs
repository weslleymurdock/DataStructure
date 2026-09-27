using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>Merge sort. Time O(n log n) in best, average and worst cases; space O(n).</summary>
public sealed class MergeSort
{
    public AlgorithmResult<int> ExecuteDSArray(DSArray<int> d) => Sort(d.Count, i => d[i], (i, v) => d[i] = v);
    public AlgorithmResult<int> ExecuteDSList(DSList<int> d) => Sort(d.Count, i => d[i], (i, v) => d[i] = v);
    public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int> d) => SortCopy([.. d]);
    public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int> d) => SortCopy([.. d]);
    public AlgorithmResult<int> ExecuteDSQueue(DSQueue<int> d) => SortQueue(d);
    public AlgorithmResult<int> ExecuteDSStack(DSStack<int> d) => SortStack(d);
    public AlgorithmResult<int> ExecuteDSDeque(DSDeque<int> d) => SortDeque(d);

    private static AlgorithmResult<int> Sort(int n, Func<int, int> get, Action<int, int> set)
    {
        var s = System.Diagnostics.Stopwatch.StartNew();
        var a = Enumerable.Range(0, n).Select(get).ToArray();
        Merge(a, 0, a.Length - 1);
        for (var i = 0; i < n; i++) set(i, a[i]);
        return new(n, s.Elapsed);
    }
    private static AlgorithmResult<int> SortCopy(int[] a)
    {
        var s = System.Diagnostics.Stopwatch.StartNew();
        Merge(a, 0, a.Length - 1); 
        return new(a.Length, s.Elapsed);
    }
    private static void Merge(int[] a, int lo, int hi) 
    { 
        if (lo >= hi) return; 
        var mid = lo + (hi - lo) / 2; 
        Merge(a, lo, mid); 
        Merge(a, mid + 1, hi); 
        var tmp = new int[hi - lo + 1];
        var i = lo; 
        var j = mid + 1; 
        var k = 0; 
        while (i <= mid && j <= hi) tmp[k++] = a[i] <= a[j] ? a[i++] : a[j++]; 
        while (i <= mid) tmp[k++] = a[i++]; 
        while (j <= hi) tmp[k++] = a[j++]; 
        Array.Copy(tmp, 0, a, lo, tmp.Length); 
    }
    private static AlgorithmResult<int> SortQueue(DSQueue<int> d) 
    {
        var a = new List<int>(); 
        while (d.Count > 0) a.Add(d.Dequeue()); 
        var r = SortCopy([.. a]); 
        foreach (var x in a.Order()) d.Enqueue(x); 
        return r;
    }
    private static AlgorithmResult<int> SortStack(DSStack<int> d) 
    { 
        var a = new List<int>(); 
        while (d.Count > 0) a.Add(d.Pop()); 
        var r = SortCopy([.. a]); 
        foreach (var x in a.OrderDescending()) d.Push(x);
        return r;
    }
    private static AlgorithmResult<int> SortDeque(DSDeque<int> d)
    {
        var a = new List<int>();
        while (d.Count > 0) a.Add(d.RemoveFirst());
        var r = SortCopy([.. a]);
        foreach (var x in a.Order()) d.AddLast(x);
        return r;
    }
}