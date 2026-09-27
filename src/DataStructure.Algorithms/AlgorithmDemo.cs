using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Runs the non-visual algorithm demonstrations and reports elapsed time.
/// </summary>
public static class AlgorithmDemo
{
    /// <summary>Executes all implemented search and sorting algorithms.</summary>
    public static void Run()
    {
        System.Console.WriteLine("=== ALGORITHM PERFORMANCE ===");

        // A reversed sequence gives the sorting algorithms a non-trivial workload.
        var values = Enumerable.Range(0, 2_000).Reverse().ToArray();

        Bubble(values);
        Selection(values);
        Insertion(values);
        Merge(values);
        Quick(values);
        Heap(values);

        Linear(values);
        Binary(values);
        Jump(values);
        Interpolation(values);
        Advanced();
    }

    private static void Bubble(int[] values)
    {
        var algorithm = new BubbleSort();

        Print("BubbleSort", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values)));

        Print("BubbleSort", "DSList",
            algorithm.ExecuteDSList(CreateList(values)));

        Print("BubbleSort", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values)));

        Print("BubbleSort", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values)));

        Print("BubbleSort", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values)));

        Print("BubbleSort", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values)));

        Print("BubbleSort", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values)));
    }

    private static void Selection(int[] values)
    {
        var algorithm = new SelectionSort();

        Print("SelectionSort", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values)));

        Print("SelectionSort", "DSList",
            algorithm.ExecuteDSList(CreateList(values)));

        Print("SelectionSort", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values)));

        Print("SelectionSort", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values)));

        Print("SelectionSort", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values)));

        Print("SelectionSort", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values)));

        Print("SelectionSort", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values)));
    }

    private static void Insertion(int[] values)
    {
        var algorithm = new InsertionSort();

        Print("InsertionSort", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values)));

        Print("InsertionSort", "DSList",
            algorithm.ExecuteDSList(CreateList(values)));

        Print("InsertionSort", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values)));

        Print("InsertionSort", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values)));

        Print("InsertionSort", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values)));

        Print("InsertionSort", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values)));

        Print("InsertionSort", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values)));
    }

    private static void Merge(int[] values)
    {
        var algorithm = new MergeSort();

        Print("MergeSort", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values)));

        Print("MergeSort", "DSList",
            algorithm.ExecuteDSList(CreateList(values)));

        Print("MergeSort", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values)));

        Print("MergeSort", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values)));

        Print("MergeSort", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values)));

        Print("MergeSort", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values)));

        Print("MergeSort", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values)));
    }

    private static void Quick(int[] values)
    {
        var algorithm = new QuickSort();

        Print("QuickSort", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values)));

        Print("QuickSort", "DSList",
            algorithm.ExecuteDSList(CreateList(values)));

        Print("QuickSort", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values)));

        Print("QuickSort", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values)));

        Print("QuickSort", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values)));

        Print("QuickSort", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values)));

        Print("QuickSort", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values)));
    }

    private static void Heap(int[] values)
    {
        var algorithm = new HeapSort();

        Print("HeapSort", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values)));

        Print("HeapSort", "DSList",
            algorithm.ExecuteDSList(CreateList(values)));

        Print("HeapSort", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values)));

        Print("HeapSort", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values)));

        Print("HeapSort", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values)));

        Print("HeapSort", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values)));

        Print("HeapSort", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values)));
    }

    private static void Linear(int[] values)
    {
        var algorithm = new LinearSearch();

        Print("LinearSearch", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(values), 1));

        Print("LinearSearch", "DSList",
            algorithm.ExecuteDSList(CreateList(values), 1));

        Print("LinearSearch", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(values), 1));

        Print("LinearSearch", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(values), 1));

        Print("LinearSearch", "DSQueue",
            algorithm.ExecuteDSQueue(CreateQueue(values), 1));

        Print("LinearSearch", "DSStack",
            algorithm.ExecuteDSStack(CreateStack(values), 1));

        Print("LinearSearch", "DSDeque",
            algorithm.ExecuteDSDeque(CreateDeque(values), 1));

        Print("LinearSearch", "DSPriorityQueue",
            algorithm.ExecuteDSPriorityQueue(CreatePriorityQueue(values), 1));
    }

    private static void Binary(int[] values)
    {
        var sorted = values.Order().ToArray();
        var algorithm = new BinarySearch();

        Print("BinarySearch", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(sorted), 1_000));

        Print("BinarySearch", "DSList",
            algorithm.ExecuteDSList(CreateList(sorted), 1_000));

        Print("BinarySearch", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(sorted), 1_000));

        Print("BinarySearch", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(sorted), 1_000));
    }

    private static void Jump(int[] values)
    {
        var sorted = values.Order().ToArray();
        var algorithm = new JumpSearch();

        Print("JumpSearch", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(sorted), 1_000));

        Print("JumpSearch", "DSList",
            algorithm.ExecuteDSList(CreateList(sorted), 1_000));

        Print("JumpSearch", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(sorted), 1_000));

        Print("JumpSearch", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(sorted), 1_000));
    }

    private static void Advanced()
    {
        var hashTable = new DSHashTable<int, string>();
        hashTable.Add(10, "A");
        hashTable.Add(20, "B");

        var hashResult = new HashSearch().Execute(hashTable, 20);
        Print("HashSearch", "DSHashTable", hashResult);

        var graph = new DSGraph<int>();
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 4);

        var bfs = new BreadthFirstSearch().Execute(graph, 1);
        Print("BreadthFirstSearch", "DSGraph", bfs);

        var dfs = new DepthFirstSearch().Execute(graph, 1);
        Print("DepthFirstSearch", "DSGraph", dfs);

        var weighted = new DSWeightedGraph<int>();
        weighted.AddEdge(1, 2, 2);
        weighted.AddEdge(1, 3, 5);
        weighted.AddEdge(2, 4, 2);
        weighted.AddEdge(3, 4, 1);

        var dijkstra = new Dijkstra().Execute(weighted, 1);
        Print("Dijkstra", "DSWeightedGraph", dijkstra);

        var astar = new AStar().Execute(weighted, 1, 4, (_, _) => 0);
        Print("AStar", "DSWeightedGraph", astar);

        var greedy = new GreedyBestFirstSearch().Execute(weighted, 1, 4, (_, _) => 0);
        Print("GreedyBestFirstSearch", "DSWeightedGraph", greedy);

        var similarity = new SimilaritySearch().ExecuteText(
            "data structures and algorithms",
            ["data structures", "graph algorithms", "chocolate cake"]);

        Print("SimilaritySearch", "Text", similarity[0]);
    }

    private static void Interpolation(int[] values)
    {
        var sorted = values.Order().ToArray();
        var algorithm = new InterpolationSearch();

        Print("InterpolationSearch", "DSArray",
            algorithm.ExecuteDSArray(new DSArray<int>(sorted), 1_000));

        Print("InterpolationSearch", "DSList",
            algorithm.ExecuteDSList(CreateList(sorted), 1_000));

        Print("InterpolationSearch", "DSLinkedList",
            algorithm.ExecuteDSLinkedList(CreateLinkedList(sorted), 1_000));

        Print("InterpolationSearch", "DSCollection",
            algorithm.ExecuteDSCollection(CreateCollection(sorted), 1_000));
    }

    private static DSList<int> CreateList(IEnumerable<int> values)
    {
        var data = new DSList<int>();

        foreach (var value in values)
            data.Add(value);

        return data;
    }

    private static DSLinkedList<int> CreateLinkedList(IEnumerable<int> values)
    {
        var data = new DSLinkedList<int>();

        foreach (var value in values)
            data.AddLast(value);

        return data;
    }

    private static DSCollection<int> CreateCollection(IEnumerable<int> values)
    {
        var data = new DSCollection<int>();

        foreach (var value in values)
            data.Add(value);

        return data;
    }

    private static DSQueue<int> CreateQueue(IEnumerable<int> values)
    {
        var data = new DSQueue<int>();

        foreach (var value in values)
            data.Enqueue(value);

        return data;
    }

    private static DSStack<int> CreateStack(IEnumerable<int> values)
    {
        var data = new DSStack<int>();

        foreach (var value in values)
            data.Push(value);

        return data;
    }

    private static DSDeque<int> CreateDeque(IEnumerable<int> values)
    {
        var data = new DSDeque<int>();

        foreach (var value in values)
            data.AddLast(value);

        return data;
    }

    private static DSPriorityQueue<int> CreatePriorityQueue(IEnumerable<int> values)
    {
        var data = new DSPriorityQueue<int>();

        foreach (var value in values)
            data.Enqueue(value);

        return data;
    }

    /// <summary>Writes one measured result using aligned columns.</summary>
    private static void Print<T>(
        string algorithm,
        string structure,
        AlgorithmResult<T> result)
    {
        System.Console.WriteLine(
            $"{algorithm,-22}{structure,-20}{result.Elapsed.TotalMicroseconds,10:F2} µs");
    }
}
