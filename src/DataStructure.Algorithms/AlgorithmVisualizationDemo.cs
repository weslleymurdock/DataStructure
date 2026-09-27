using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Runs all sorting algorithms concurrently and renders their progress side by side.
/// Each round uses a different data structure, so every Execute&lt;TDataStructure&gt;
/// implementation is exercised by the visualization.
/// </summary>
public static class AlgorithmVisualizationDemo
{
    private static readonly string[] Algorithms =
    [
        "BubbleSort",
        "SelectionSort",
        "InsertionSort",
        "MergeSort",
        "QuickSort",
        "HeapSort"
    ];

    private static readonly string[] Structures =
    [
        "DSArray",
        "DSList",
        "DSLinkedList",
        "DSCollection",
        "DSQueue",
        "DSStack",
        "DSDeque"
    ];

    /// <summary>Runs one side-by-side race for every supported sorting structure.</summary>
    public static void Run(Action<Visualization.Console> render)
    {
        ArgumentNullException.ThrowIfNull(render);

        var values = CreateVisualizationValues();

        foreach (var structure in Structures)
        {
            RunRound(structure, values, render);
            Thread.Sleep(500);
        }
    }

    /// <summary>Executes all six algorithms concurrently against independent structure instances.</summary>
    private static void RunRound(
        string structure,
        IReadOnlyList<int> values,
        Action<Visualization.Console> render)
    {
        var panels = Algorithms
            .Select(name => new Visualization.PanelState(name, values))
            .ToArray();

        var completed = new List<string>();
        var synchronization = new object();

        Render(structure, panels, completed, render);

        var executions = CreateExecutions(
            structure,
            values,
            panels,
            completed,
            synchronization,
            render);

        // Each sort runs in its own Task. Task.WhenAll/WaitAll is only used to
        // join them after all six have been started.
        var tasks = executions
            .Select(execution => Task.Run(execution))
            .ToArray();

        Task.WaitAll(tasks);

        Render(structure, panels, completed, render);
    }

    private static IReadOnlyList<Action> CreateExecutions(
        string structure,
        IReadOnlyList<int> values,
        IReadOnlyList<Visualization.PanelState> panels,
        ICollection<string> completed,
        object synchronization,
        Action<Visualization.Console> render)
    {
        return structure switch
        {
            "DSArray" => CreateArrayExecutions(
                values, panels, completed, synchronization, render),

            "DSList" => CreateExecutionsUsingFactory(
                values, panels, completed, synchronization, render,
                () =>
                {
                    var data = new DSList<int>();

                    foreach (var value in values)
                        data.Add(value);

                    return data;
                }),

            "DSLinkedList" => CreateExecutionsUsingFactory(
                values, panels, completed, synchronization, render,
                () =>
                {
                    var data = new DSLinkedList<int>();

                    foreach (var value in values)
                        data.AddLast(value);

                    return data;
                }),

            "DSCollection" => CreateExecutionsUsingFactory(
                values, panels, completed, synchronization, render,
                () =>
                {
                    var data = new DSCollection<int>();

                    foreach (var value in values)
                        data.Add(value);

                    return data;
                }),

            "DSQueue" => CreateExecutionsUsingFactory(
                values, panels, completed, synchronization, render,
                () =>
                {
                    var data = new DSQueue<int>();

                    foreach (var value in values)
                        data.Enqueue(value);

                    return data;
                }),

            "DSStack" => CreateExecutionsUsingFactory(
                values, panels, completed, synchronization, render,
                () =>
                {
                    var data = new DSStack<int>();

                    foreach (var value in values)
                        data.Push(value);

                    return data;
                }),

            "DSDeque" => CreateExecutionsUsingFactory(
                values, panels, completed, synchronization, render,
                () =>
                {
                    var data = new DSDeque<int>();

                    foreach (var value in values)
                        data.AddLast(value);

                    return data;
                }),

            _ => throw new ArgumentOutOfRangeException(nameof(structure))
        };
    }

    private static IReadOnlyList<Action> CreateArrayExecutions(
        IReadOnlyList<int> values,
        IReadOnlyList<Visualization.PanelState> panels,
        ICollection<string> completed,
        object synchronization,
        Action<Visualization.Console> render)
    {
        return
        [
            CreateExecution("BubbleSort", panels, completed, synchronization, render,
                callback =>
                {
                    var algorithm = new BubbleSort(callback);
                    algorithm.ExecuteDSArray(new DSArray<int>(values));
                }),
            CreateExecution("SelectionSort", panels, completed, synchronization, render,
                callback =>
                {
                    var algorithm = new SelectionSort(callback);
                    algorithm.ExecuteDSArray(new DSArray<int>(values));
                }),
            CreateExecution("InsertionSort", panels, completed, synchronization, render,
                callback =>
                {
                    var algorithm = new InsertionSort(callback);
                    algorithm.ExecuteDSArray(new DSArray<int>(values));
                }),
            CreateExecution("MergeSort", panels, completed, synchronization, render,
                callback =>
                {
                    var algorithm = new MergeSort(callback);
                    algorithm.ExecuteDSArray(new DSArray<int>(values));
                }),
            CreateExecution("QuickSort", panels, completed, synchronization, render,
                callback =>
                {
                    var algorithm = new QuickSort(callback);
                    algorithm.ExecuteDSArray(new DSArray<int>(values));
                }),
            CreateExecution("HeapSort", panels, completed, synchronization, render,
                callback =>
                {
                    var algorithm = new HeapSort(callback);
                    algorithm.ExecuteDSArray(new DSArray<int>(values));
                })
        ];
    }

    private static IReadOnlyList<Action> CreateExecutionsUsingFactory(
        IReadOnlyList<int> values,
        IReadOnlyList<Visualization.PanelState> panels,
        ICollection<string> completed,
        object synchronization,
        Action<Visualization.Console> render,
        Func<object> createData)
    {
        return
        [
            CreateExecution("BubbleSort", panels, completed, synchronization, render,
                callback => ExecuteBubble(new BubbleSort(callback), createData())),
            CreateExecution("SelectionSort", panels, completed, synchronization, render,
                callback => ExecuteSelection(new SelectionSort(callback), createData())),
            CreateExecution("InsertionSort", panels, completed, synchronization, render,
                callback => ExecuteInsertion(new InsertionSort(callback), createData())),
            CreateExecution("MergeSort", panels, completed, synchronization, render,
                callback => ExecuteMerge(new MergeSort(callback), createData())),
            CreateExecution("QuickSort", panels, completed, synchronization, render,
                callback => ExecuteQuick(new QuickSort(callback), createData())),
            CreateExecution("HeapSort", panels, completed, synchronization, render,
                callback => ExecuteHeap(new HeapSort(callback), createData()))
        ];
    }

    private static Action CreateExecution(
        string algorithmName,
        IReadOnlyList<Visualization.PanelState> panels,
        ICollection<string> completed,
        object synchronization,
        Action<Visualization.Console> render,
        Action<Action<IReadOnlyList<int>>> execute)
    {
        return () =>
        {
            var panel = panels.Single(item => item.Algorithm == algorithmName);

            try
            {
                execute(snapshot =>
                {
                    lock (synchronization)
                    {
                        panel.Update(snapshot);
                        Render(panel.Structure, panels, [.. completed], render);
                    }

                    Thread.Sleep(45);
                });
            }
            finally
            {
                lock (synchronization)
                {
                    panel.Complete(completed.Count + 1);
                    completed.Add(algorithmName);
                    Render(panel.Structure, panels, [.. completed], render);
                }
            }
        };
    }

    private static void Render(
        string structure,
        IReadOnlyList<Visualization.PanelState> panels,
        IReadOnlyList<string> completed,
        Action<Visualization.Console> render)
    {
        render(new Visualization.Console(structure, panels, completed));
    }

    private static void ExecuteBubble(BubbleSort algorithm, object data)
    {
        switch (data)
        {
            case DSList<int> list:
                algorithm.ExecuteDSList(list);
                break;
            case DSLinkedList<int> linkedList:
                algorithm.ExecuteDSLinkedList(linkedList);
                break;
            case DSCollection<int> collection:
                algorithm.ExecuteDSCollection(collection);
                break;
            case DSQueue<int> queue:
                algorithm.ExecuteDSQueue(queue);
                break;
            case DSStack<int> stack:
                algorithm.ExecuteDSStack(stack);
                break;
            case DSDeque<int> deque:
                algorithm.ExecuteDSDeque(deque);
                break;
            default:
                throw new ArgumentException("Unsupported data structure.", nameof(data));
        }
    }

    private static void ExecuteSelection(SelectionSort algorithm, object data)
    {
        switch (data)
        {
            case DSList<int> list:
                algorithm.ExecuteDSList(list);
                break;
            case DSLinkedList<int> linkedList:
                algorithm.ExecuteDSLinkedList(linkedList);
                break;
            case DSCollection<int> collection:
                algorithm.ExecuteDSCollection(collection);
                break;
            case DSQueue<int> queue:
                algorithm.ExecuteDSQueue(queue);
                break;
            case DSStack<int> stack:
                algorithm.ExecuteDSStack(stack);
                break;
            case DSDeque<int> deque:
                algorithm.ExecuteDSDeque(deque);
                break;
            default:
                throw new ArgumentException("Unsupported data structure.", nameof(data));
        }
    }

    private static void ExecuteInsertion(InsertionSort algorithm, object data)
    {
        switch (data)
        {
            case DSList<int> list:
                algorithm.ExecuteDSList(list);
                break;
            case DSLinkedList<int> linkedList:
                algorithm.ExecuteDSLinkedList(linkedList);
                break;
            case DSCollection<int> collection:
                algorithm.ExecuteDSCollection(collection);
                break;
            case DSQueue<int> queue:
                algorithm.ExecuteDSQueue(queue);
                break;
            case DSStack<int> stack:
                algorithm.ExecuteDSStack(stack);
                break;
            case DSDeque<int> deque:
                algorithm.ExecuteDSDeque(deque);
                break;
            default:
                throw new ArgumentException("Unsupported data structure.", nameof(data));
        }
    }

    private static void ExecuteMerge(MergeSort algorithm, object data)
    {
        switch (data)
        {
            case DSList<int> list:
                algorithm.ExecuteDSList(list);
                break;
            case DSLinkedList<int> linkedList:
                algorithm.ExecuteDSLinkedList(linkedList);
                break;
            case DSCollection<int> collection:
                algorithm.ExecuteDSCollection(collection);
                break;
            case DSQueue<int> queue:
                algorithm.ExecuteDSQueue(queue);
                break;
            case DSStack<int> stack:
                algorithm.ExecuteDSStack(stack);
                break;
            case DSDeque<int> deque:
                algorithm.ExecuteDSDeque(deque);
                break;
            default:
                throw new ArgumentException("Unsupported data structure.", nameof(data));
        }
    }

    private static void ExecuteQuick(QuickSort algorithm, object data)
    {
        switch (data)
        {
            case DSList<int> list:
                algorithm.ExecuteDSList(list);
                break;
            case DSLinkedList<int> linkedList:
                algorithm.ExecuteDSLinkedList(linkedList);
                break;
            case DSCollection<int> collection:
                algorithm.ExecuteDSCollection(collection);
                break;
            case DSQueue<int> queue:
                algorithm.ExecuteDSQueue(queue);
                break;
            case DSStack<int> stack:
                algorithm.ExecuteDSStack(stack);
                break;
            case DSDeque<int> deque:
                algorithm.ExecuteDSDeque(deque);
                break;
            default:
                throw new ArgumentException("Unsupported data structure.", nameof(data));
        }
    }

    private static void ExecuteHeap(HeapSort algorithm, object data)
    {
        switch (data)
        {
            case DSList<int> list:
                algorithm.ExecuteDSList(list);
                break;
            case DSLinkedList<int> linkedList:
                algorithm.ExecuteDSLinkedList(linkedList);
                break;
            case DSCollection<int> collection:
                algorithm.ExecuteDSCollection(collection);
                break;
            case DSQueue<int> queue:
                algorithm.ExecuteDSQueue(queue);
                break;
            case DSStack<int> stack:
                algorithm.ExecuteDSStack(stack);
                break;
            case DSDeque<int> deque:
                algorithm.ExecuteDSDeque(deque);
                break;
            default:
                throw new ArgumentException("Unsupported data structure.", nameof(data));
        }
    }

    private static int[] CreateVisualizationValues()
    {
        return
        [
            8, 3, 12, 5, 1, 10, 7,
            14, 4, 11, 2, 13, 6, 9
        ];
    }
}
