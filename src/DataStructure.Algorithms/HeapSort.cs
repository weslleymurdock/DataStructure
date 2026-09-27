using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>Heap sort using a max heap. Time O(n log n) in best, average and worst cases; space O(1) for the array itself.</summary>
public sealed class HeapSort
{
    public AlgorithmResult<int> ExecuteDSArray(DSArray<int> d) => Sort(d.Count, i => d[i], (i, v) => d[i] = v);
    public AlgorithmResult<int> ExecuteDSList(DSList<int> d) => Sort(d.Count, i => d[i], (i, v) => d[i] = v);
    public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int> d) => SortCopy([.. d]);
    public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int> d) => SortCopy([.. d]);
    public AlgorithmResult<int> ExecuteDSQueue(DSQueue<int> d) => SortQueue(d);
    public AlgorithmResult<int> ExecuteDSStack(DSStack<int> d) => SortStack(d);
    public AlgorithmResult<int> ExecuteDSDeque(DSDeque<int> d) => SortDeque(d);

    private static AlgorithmResult<int> Sort(int count, Func<int, int> get, Action<int, int> set) 
    { 
        var s = System.Diagnostics.Stopwatch.StartNew(); 
        var a = Enumerable.Range(0, count).Select(get).ToArray(); 
        Heap(a); 
        for (var i = 0; i < count; i++) set(i, a[i]); 
        return new(count, s.Elapsed); 
    }
    private static AlgorithmResult<int> SortCopy(int[] a) { var s = System.Diagnostics.Stopwatch.StartNew(); Heap(a); return new(a.Length, s.Elapsed); }
    private static void Heap(int[] a) 
    { 
        for (var i = a.Length / 2 - 1; i >= 0; i--) Down(a, i, a.Length); 
        for (var end = a.Length - 1; end > 0; end--) 
        { 
            (a[0], a[end]) = (a[end], a[0]); 
            Down(a, 0, end); 
        } 
    }
    private static void Down(int[] a, int i, int n)
    { 
        while (true) 
        { 
            var l = i * 2 + 1; 
            if (l >= n) return; 
            var r = l + 1; 
            var c = r < n && a[r] > a[l] ? r : l; 
            if (a[i] >= a[c]) return; 
            (a[i], a[c]) = (a[c], a[i]); 
            i = c; 
        } 
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