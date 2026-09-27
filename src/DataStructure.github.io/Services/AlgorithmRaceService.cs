using DataStructure.Abstractions;

namespace DataStructure.github.io.Services;

public sealed class RacePanel
{
    public RacePanel(string algorithm, string structureKey, IReadOnlyList<int> values)
    {
        Algorithm = algorithm;
        StructureKey = structureKey;
        Structure = StructureCatalog.Get(structureKey).Name;
        Values = [.. values];
    }

    public string Algorithm { get; }
    public string StructureKey { get; }
    public string Structure { get; }
    public string Title => $"{Algorithm} × {Structure}";
    public int[] Values { get; private set; }
    public int Iterations { get; private set; }
    public int CurrentIndex { get; private set; } = -1;
    public int Comparisons { get; private set; }
    public bool Completed { get; private set; }
    public int CompletionOrder { get; private set; }

    public void Update(int[] values, int currentIndex = -1, bool comparison = false)
    {
        Values = [.. values];
        CurrentIndex = currentIndex;
        if (comparison)
            Comparisons++;

        Iterations++;
    }

    public void Complete(int order)
    {
        Completed = true;
        CompletionOrder = order;
        CurrentIndex = -1;
    }
}

public sealed class AlgorithmRaceService
{
    public static readonly string[] SortAlgorithms =
        ["BubbleSort", "SelectionSort", "InsertionSort", "MergeSort", "QuickSort", "HeapSort"];

    public static readonly string[] SearchAlgorithms =
        ["LinearSearch", "BinarySearch", "JumpSearch", "InterpolationSearch"];

    public static readonly string[] Structures =
        ["array", "collection", "list", "linkedlist", "nodelist",
         "circularlinkedlist", "queue", "stack", "deque", "deck",
         "priorityqueue", "binarytree", "binarysearchtree", "heap", "maxheap", "graph"];

    private static readonly int[] InitialValues =
        [8, 3, 12, 5, 1, 10, 7, 14, 4, 11, 2, 13, 6, 9];

    public IReadOnlyList<RacePanel> CreatePanels(
        string algorithm,
        IEnumerable<string> structures)
        => structures
            .Select(key => new RacePanel(
                algorithm,
                StructureCatalog.Get(key).Name,
                GetInitialValues(key)))
            .ToArray();

    public IReadOnlyList<RacePanel> CreatePanels(
        IEnumerable<string> algorithms,
        string structure)
        => algorithms
            .Select(algorithm => new RacePanel(
                algorithm,
                StructureCatalog.Get(structure).Name,
                GetInitialValues(structure)))
            .ToArray();

    public IReadOnlyList<RacePanel> CreatePanels(
        IEnumerable<(string Algorithm, string Structure)> comparisons)
        => comparisons
            .Select(comparison => new RacePanel(
                comparison.Algorithm,
                StructureCatalog.Get(comparison.Structure).Name,
                GetInitialValues(comparison.Structure)))
            .ToArray();

    public async Task RunSortAsync(
        IReadOnlyList<RacePanel> panels,
        int delay,
        Action<RacePanel> update,
        CancellationToken cancellationToken = default)
    {
        var completion = 0;

        var tasks = panels.Select(async panel =>
        {
            int[] values = [.. panel.Values];

            async Task Step(int current = -1)
            {
                panel.Update(values, current);
                update(panel);
                await Task.Delay(delay, cancellationToken);
            }

            switch (panel.Algorithm)
            {
                case "BubbleSort":
                    await BubbleSort(values, Step, cancellationToken);
                    break;
                case "SelectionSort":
                    await SelectionSort(values, Step, cancellationToken);
                    break;
                case "InsertionSort":
                    await InsertionSort(values, Step, cancellationToken);
                    break;
                case "MergeSort":
                    await MergeSort(values, Step, cancellationToken);
                    break;
                case "QuickSort":
                    await QuickSort(values, Step, cancellationToken);
                    break;
                case "HeapSort":
                    await HeapSort(values, Step, cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm));
            }

            panel.Update(values);
            panel.Complete(Interlocked.Increment(ref completion));
            update(panel);
        });

        await Task.WhenAll(tasks);
    }

    public async Task RunSearchAsync(
        IReadOnlyList<RacePanel> panels,
        int target,
        int delay,
        Action<RacePanel> update,
        CancellationToken cancellationToken = default)
    {
        var completion = 0;

        var tasks = panels.Select(async panel =>
        {
            int[] values = [.. panel.Values];

            async Task Step(int index, bool comparison = true)
            {
                panel.Update(values, index, comparison);
                update(panel);
                await Task.Delay(delay, cancellationToken);
            }

            switch (panel.Algorithm)
            {
                case "LinearSearch":
                    await LinearSearch(values, target, Step, cancellationToken);
                    break;
                case "BinarySearch":
                    Array.Sort(values);
                    await BinarySearch(values, target, Step, cancellationToken);
                    break;
                case "JumpSearch":
                    Array.Sort(values);
                    await JumpSearch(values, target, Step, cancellationToken);
                    break;
                case "InterpolationSearch":
                    Array.Sort(values);
                    await InterpolationSearch(values, target, Step, cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm));
            }

            panel.Complete(Interlocked.Increment(ref completion));
            update(panel);
        });

        await Task.WhenAll(tasks);
    }

    private static async Task BubbleSort(int[] values, Func<int, Task> step, CancellationToken token)
    {
        for (var end = values.Length - 1; end > 0; end--)
        {
            var changed = false;

            for (var index = 0; index < end; index++)
            {
                token.ThrowIfCancellationRequested();

                if (values[index] <= values[index + 1])
                    continue;

                (values[index], values[index + 1]) = (values[index + 1], values[index]);
                changed = true;
                await step(index);
            }

            if (!changed)
                break;
        }
    }

    private static async Task SelectionSort(int[] values, Func<int, Task> step, CancellationToken token)
    {
        for (var start = 0; start < values.Length - 1; start++)
        {
            var smallest = start;

            for (var index = start + 1; index < values.Length; index++)
            {
                token.ThrowIfCancellationRequested();

                if (values[index] < values[smallest])
                    smallest = index;

                await step(index);
            }

            if (smallest != start)
                (values[start], values[smallest]) = (values[smallest], values[start]);
        }
    }

    private static async Task InsertionSort(int[] values, Func<int, Task> step, CancellationToken token)
    {
        for (var index = 1; index < values.Length; index++)
        {
            var value = values[index];
            var position = index - 1;

            while (position >= 0 && values[position] > value)
            {
                token.ThrowIfCancellationRequested();
                values[position + 1] = values[position];
                position--;
                await step(position + 1);
            }

            values[position + 1] = value;
            await step(position + 1);
        }
    }

    private static Task MergeSort(int[] values, Func<int, Task> step, CancellationToken token)
        => MergeSort(values, 0, values.Length - 1, step, token);

    private static async Task MergeSort(
        int[] values, int left, int right,
        Func<int, Task> step, CancellationToken token)
    {
        if (left >= right)
            return;

        var middle = left + (right - left) / 2;

        await MergeSort(values, left, middle, step, token);
        await MergeSort(values, middle + 1, right, step, token);

        var buffer = new int[right - left + 1];
        var i = left;
        var j = middle + 1;
        var position = 0;

        while (i <= middle && j <= right)
        {
            token.ThrowIfCancellationRequested();
            buffer[position++] = values[i] <= values[j] ? values[i++] : values[j++];
        }

        while (i <= middle)
            buffer[position++] = values[i++];

        while (j <= right)
            buffer[position++] = values[j++];

        for (var index = 0; index < buffer.Length; index++)
        {
            values[left + index] = buffer[index];
            await step(left + index);
        }
    }

    private static Task QuickSort(int[] values, Func<int, Task> step, CancellationToken token)
        => QuickSort(values, 0, values.Length - 1, step, token);

    private static async Task QuickSort(
        int[] values, int low, int high,
        Func<int, Task> step, CancellationToken token)
    {
        if (low >= high)
            return;

        var pivot = values[high];
        var store = low;

        for (var index = low; index < high; index++)
        {
            token.ThrowIfCancellationRequested();

            if (values[index] <= pivot)
            {
                (values[store], values[index]) = (values[index], values[store]);
                await step(store);
                store++;
            }
        }

        (values[store], values[high]) = (values[high], values[store]);
        await step(store);

        await QuickSort(values, low, store - 1, step, token);
        await QuickSort(values, store + 1, high, step, token);
    }

    private static async Task HeapSort(int[] values, Func<int, Task> step, CancellationToken token)
    {
        for (var index = values.Length / 2 - 1; index >= 0; index--)
            await SiftDown(values, values.Length, index, step, token);

        for (var end = values.Length - 1; end > 0; end--)
        {
            token.ThrowIfCancellationRequested();
            (values[0], values[end]) = (values[end], values[0]);
            await step(end);
            await SiftDown(values, end, 0, step, token);
        }
    }

    private static async Task SiftDown(
        int[] values, int count, int root,
        Func<int, Task> step, CancellationToken token)
    {
        while (true)
        {
            token.ThrowIfCancellationRequested();

            var left = root * 2 + 1;
            var right = left + 1;
            var largest = root;

            if (left < count && values[left] > values[largest])
                largest = left;

            if (right < count && values[right] > values[largest])
                largest = right;

            if (largest == root)
                return;

            (values[root], values[largest]) = (values[largest], values[root]);
            await step(largest);
            root = largest;
        }
    }

    private static async Task LinearSearch(
        int[] values, int target,
        Func<int, bool, Task> step, CancellationToken token)
    {
        for (var index = 0; index < values.Length; index++)
        {
            token.ThrowIfCancellationRequested();
            await step(index, true);

            if (values[index] == target)
                return;
        }
    }

    private static async Task BinarySearch(
        int[] values, int target,
        Func<int, bool, Task> step, CancellationToken token)
    {
        var low = 0;
        var high = values.Length - 1;

        while (low <= high)
        {
            token.ThrowIfCancellationRequested();

            var middle = low + (high - low) / 2;
            await step(middle, true);

            if (values[middle] == target)
                return;

            if (values[middle] < target)
                low = middle + 1;
            else
                high = middle - 1;
        }
    }

    private static async Task JumpSearch(
        int[] values, int target,
        Func<int, bool, Task> step, CancellationToken token)
    {
        if (values.Length == 0)
            return;

        var jump = Math.Max(1, (int)Math.Sqrt(values.Length));
        var previous = 0;
        var next = jump;

        while (previous < values.Length &&
               values[Math.Min(next, values.Length) - 1] < target)
        {
            await step(Math.Min(next, values.Length) - 1, true);
            previous = next;
            next += jump;
            token.ThrowIfCancellationRequested();

            if (previous >= values.Length)
                return;
        }

        for (var index = previous; index < Math.Min(next, values.Length); index++)
        {
            token.ThrowIfCancellationRequested();
            await step(index, true);

            if (values[index] == target || values[index] > target)
                return;
        }
    }

    private static async Task InterpolationSearch(
        int[] values, int target,
        Func<int, bool, Task> step, CancellationToken token)
    {
        var low = 0;
        var high = values.Length - 1;

        while (low <= high &&
               target >= values[low] &&
               target <= values[high])
        {
            token.ThrowIfCancellationRequested();

            if (values[low] == values[high])
            {
                await step(low, true);
                return;
            }

            var position = low +
                (int)((long)(target - values[low]) *
                      (high - low) /
                      (values[high] - values[low]));

            await step(position, true);

            if (values[position] == target)
                return;

            if (values[position] < target)
                low = position + 1;
            else
                high = position - 1;
        }
    }

    private static int[] GetInitialValues(string structure)
    {
        int[] values = [.. InitialValues];

        if (structure == "stack")
            Array.Reverse(values);

        if (structure == "priorityqueue")
        {
            var queue = new DSPriorityQueue<int>();

            foreach (var value in values)
                queue.Enqueue(value);

            var result = new int[queue.Count];

            for (var index = 0; index < result.Length; index++)
                result[index] = queue.Dequeue();

            return result;
        }

        if (structure == "circularlinkedlist")
        {
            var list = new DSCircularLinkedList<int>();

            foreach (var value in values)
                list.AddLast(value);

            return [.. list.Enumerate()];
        }

        if (structure == "binarytree")
        {
            var tree = new DSBinaryTree<int>(values[0]);
            tree.Root.Left = new DSBinaryTreeNode<int>(values[1]);
            tree.Root.Right = new DSBinaryTreeNode<int>(values[2]);
            tree.Root.Left.Left = new DSBinaryTreeNode<int>(values[3]);
            tree.Root.Left.Right = new DSBinaryTreeNode<int>(values[4]);
            tree.Root.Right.Left = new DSBinaryTreeNode<int>(values[5]);
            tree.Root.Right.Right = new DSBinaryTreeNode<int>(values[6]);

            return [.. tree.PreOrder()];
        }

        if (structure == "binarysearchtree")
        {
            var tree = new DSBinarySearchTree<int>();

            foreach (var value in values)
                tree.Insert(value);

            return [.. tree.PreOrder()];
        }

        if (structure == "heap")
        {
            var heap = new DSHeap<int>();

            foreach (var value in values)
                heap.Add(value);

            return [.. heap.AsArray()];
        }

        if (structure == "maxheap")
        {
            var heap = new DSMaxHeap<int>();

            foreach (var value in values)
                heap.Add(value);

            return [.. heap.AsArray()];
        }

        if (structure == "graph")
        {
            var graph = new DSGraph<int>();

            foreach (var value in values)
                graph.AddVertex(value);

            for (var index = 0; index < values.Length - 1; index++)
                graph.AddEdge(values[index], values[index + 1]);

            graph.AddEdge(values[^1], values[0]);

            return [.. graph.BreadthFirst(values[0])];
        }

        return values;
    }
}
