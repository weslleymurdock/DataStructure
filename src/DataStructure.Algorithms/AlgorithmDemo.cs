using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>Instantiates algorithms and executes them against equivalent data structures.</summary>
public static class AlgorithmDemo
{
    public static void Run()
    {
        Console.WriteLine("=== ALGORITHM PERFORMANCE ===");
        var v = Enumerable.Range(0, 2_000).Reverse().ToArray();
        Bubble(v); 
        Selection(v); 
        Insertion(v);
        Merge(v);
        Quick(v);
        Heap(v);
        Search(v);
        Binary(v);
        Jump(v);
        Interpolation(v);
    }
    private static void Bubble(int[] v)
    {
        var x = new BubbleSort();
        P("BubbleSort", "DSArray", x.ExecuteDSArray(new DSArray<int>(v)));
        P("BubbleSort", "DSList", x.ExecuteDSList(L(v)));
        P("BubbleSort", "DSLinkedList", x.ExecuteDSLinkedList(LL(v)));
        P("BubbleSort", "DSCollection", x.ExecuteDSCollection(C(v))); 
        P("BubbleSort", "DSQueue", x.ExecuteDSQueue(Q(v))); 
        P("BubbleSort", "DSStack", x.ExecuteDSStack(S(v))); 
        P("BubbleSort", "DSDeque", x.ExecuteDSDeque(D(v)));
    }
    private static void Selection(int[] v) 
    { 
        var x = new SelectionSort(); 
        P("SelectionSort", "DSArray", x.ExecuteDSArray(new DSArray<int>(v))); 
        P("SelectionSort", "DSList", x.ExecuteDSList(L(v))); 
        P("SelectionSort", "DSLinkedList", x.ExecuteDSLinkedList(LL(v))); 
        P("SelectionSort", "DSCollection", x.ExecuteDSCollection(C(v))); 
        P("SelectionSort", "DSQueue", x.ExecuteDSQueue(Q(v))); 
        P("SelectionSort", "DSStack", x.ExecuteDSStack(S(v))); 
        P("SelectionSort", "DSDeque", x.ExecuteDSDeque(D(v)));
    }
    private static void Insertion(int[] v) 
    { 
        var x = new InsertionSort(); 
        P("InsertionSort", "DSArray", x.ExecuteDSArray(new DSArray<int>(v))); 
        P("InsertionSort", "DSList", x.ExecuteDSList(L(v))); 
        P("InsertionSort", "DSLinkedList", x.ExecuteDSLinkedList(LL(v))); 
        P("InsertionSort", "DSCollection", x.ExecuteDSCollection(C(v))); 
        P("InsertionSort", "DSQueue", x.ExecuteDSQueue(Q(v))); 
        P("InsertionSort", "DSStack", x.ExecuteDSStack(S(v))); 
        P("InsertionSort", "DSDeque", x.ExecuteDSDeque(D(v))); 
    }
    private static void Merge(int[] v) 
    { 
        var x = new MergeSort(); 
        P("MergeSort", "DSArray", x.ExecuteDSArray(new DSArray<int>(v))); 
        P("MergeSort", "DSList", x.ExecuteDSList(L(v))); 
        P("MergeSort", "DSLinkedList", x.ExecuteDSLinkedList(LL(v))); 
        P("MergeSort", "DSCollection", x.ExecuteDSCollection(C(v))); 
        P("MergeSort", "DSQueue", x.ExecuteDSQueue(Q(v))); 
        P("MergeSort", "DSStack", x.ExecuteDSStack(S(v))); 
        P("MergeSort", "DSDeque", x.ExecuteDSDeque(D(v))); 
    }
    private static void Quick(int[] v) 
    { 
        var x = new QuickSort(); 
        P("QuickSort", "DSArray", x.ExecuteDSArray(new DSArray<int>(v))); 
        P("QuickSort", "DSList", x.ExecuteDSList(L(v))); 
        P("QuickSort", "DSLinkedList", x.ExecuteDSLinkedList(LL(v))); 
        P("QuickSort", "DSCollection", x.ExecuteDSCollection(C(v))); 
        P("QuickSort", "DSQueue", x.ExecuteDSQueue(Q(v))); 
        P("QuickSort", "DSStack", x.ExecuteDSStack(S(v))); 
        P("QuickSort", "DSDeque", x.ExecuteDSDeque(D(v))); 
    }
    private static void Heap(int[] v) 
    { 
        var x = new HeapSort();
        P("HeapSort", "DSArray", x.ExecuteDSArray(new DSArray<int>(v))); 
        P("HeapSort", "DSList", x.ExecuteDSList(L(v))); 
        P("HeapSort", "DSLinkedList", x.ExecuteDSLinkedList(LL(v))); 
        P("HeapSort", "DSCollection", x.ExecuteDSCollection(C(v))); 
        P("HeapSort", "DSQueue", x.ExecuteDSQueue(Q(v))); 
        P("HeapSort", "DSStack", x.ExecuteDSStack(S(v))); 
        P("HeapSort", "DSDeque", x.ExecuteDSDeque(D(v)));
    }
    private static void Search(int[] v) 
    { 
        var x = new LinearSearch(); 
        P("LinearSearch", "DSArray", x.ExecuteDSArray(new DSArray<int>(v), 1)); 
        P("LinearSearch", "DSList", x.ExecuteDSList(L(v), 1)); 
        P("LinearSearch", "DSLinkedList", x.ExecuteDSLinkedList(LL(v), 1)); 
        P("LinearSearch", "DSCollection", x.ExecuteDSCollection(C(v), 1)); 
        P("LinearSearch", "DSQueue", x.ExecuteDSQueue(Q(v), 1)); 
        P("LinearSearch", "DSStack", x.ExecuteDSStack(S(v), 1)); 
        P("LinearSearch", "DSDeque", x.ExecuteDSDeque(D(v), 1)); 
        P("LinearSearch", "DSPriorityQueue", x.ExecuteDSPriorityQueue(PQ(v), 1)); 
    }
    private static void Binary(int[] v) 
    { 
        Array.Sort(v); 
        var x = new BinarySearch(); 
        P("BinarySearch", "DSArray", x.ExecuteDSArray(new DSArray<int>(v), 1000)); 
        P("BinarySearch", "DSList", x.ExecuteDSList(L(v), 1000)); 
        P("BinarySearch", "DSLinkedList", x.ExecuteDSLinkedList(LL(v), 1000)); 
        P("BinarySearch", "DSCollection", x.ExecuteDSCollection(C(v), 1000)); 
    }
    private static void Jump(int[] v) 
    { 
        Array.Sort(v); 
        var x = new JumpSearch(); 
        P("JumpSearch", "DSArray", x.ExecuteDSArray(new DSArray<int>(v), 1000)); 
        P("JumpSearch", "DSList", x.ExecuteDSList(L(v), 1000)); 
        P("JumpSearch", "DSLinkedList", x.ExecuteDSLinkedList(LL(v), 1000)); 
        P("JumpSearch", "DSCollection", x.ExecuteDSCollection(C(v), 1000)); 
    }
    private static void Interpolation(int[] v) 
    { 
        Array.Sort(v); 
        var x = new InterpolationSearch(); 
        P("InterpolationSearch", "DSArray", x.ExecuteDSArray(new DSArray<int>(v), 1000)); 
        P("InterpolationSearch", "DSList", x.ExecuteDSList(L(v), 1000)); 
        P("InterpolationSearch", "DSLinkedList", x.ExecuteDSLinkedList(LL(v), 1000)); 
        P("InterpolationSearch", "DSCollection", x.ExecuteDSCollection(C(v), 1000)); 
    }
    private static DSList<int> L(IEnumerable<int> x) 
    { 
        var d = new DSList<int>(); 
        foreach (var i in x) 
            d.Add(i); 
        return d; 
    }
    private static DSLinkedList<int> LL(IEnumerable<int> x) 
    { 
        var d = new DSLinkedList<int>(); 
        foreach (var i in x) 
            d.AddLast(i);
        return d; 
    }
    private static DSCollection<int> C(IEnumerable<int> x) 
    {
        var d = new DSCollection<int>(); 
        foreach (var i in x) 
            d.Add(i); 
        return d;
    }
    private static DSQueue<int> Q(IEnumerable<int> x)
    {
        var d = new DSQueue<int>(); 
        foreach (var i in x) 
            d.Enqueue(i);
        return d; 
    }
    private static DSStack<int> S(IEnumerable<int> x)
    { 
        var d = new DSStack<int>(); 
        foreach (var i in x) 
            d.Push(i); 
        return d; 
    }
    private static DSDeque<int> D(IEnumerable<int> x) 
    { 
        var d = new DSDeque<int>(); 
        foreach (var i in x) 
            d.AddLast(i); 
        return d; 
    }
    private static DSPriorityQueue<int> PQ(IEnumerable<int> x)
    { 
        var d = new DSPriorityQueue<int>();
        foreach (var i in x)
            d.Enqueue(i);
        return d;
    }
    private static void P<T>(string a, string s, AlgorithmResult<T> r) => Console.WriteLine($"{a,-20}{s,-20}{r.Elapsed.TotalMicroseconds,10:F2} µs");
}