using DataStructure.Abstractions;
using DataStructure.Algorithms;

Console.WriteLine("=== DATA STRUCTURES ===");

DemonstrateArray();
DemonstrateCollection();
DemonstrateList();
DemonstrateLinkedList();
DemonstrateStack();
DemonstrateQueue();
DemonstrateDeque();
DemonstratePriorityQueue();

Console.WriteLine();
Console.WriteLine("=== ALGORITHMS ===");
AlgorithmDemo.Run();

Console.WriteLine();
Console.WriteLine("=== SORTING VISUALIZATION ===");
Console.WriteLine("Press Ctrl+C to stop the animation.");
Thread.Sleep(500);

AlgorithmVisualizationDemo.Run(frame =>
{
    frame.Render();

    // The delay is intentionally outside the algorithm.
    // This keeps the algorithms independent from terminal I/O.
    Thread.Sleep(80);
});

static void DemonstrateArray()
{
    Console.WriteLine();
    Console.WriteLine("[Array] Fixed-size, O(1) indexed access");

    DSArray<string> months = ["January", "February", "March"];

    Console.WriteLine($"Item at index 1: {months[1]}");
}

static void DemonstrateCollection()
{
    Console.WriteLine();
    Console.WriteLine("[Collection] Dynamic contiguous storage");

    DSCollection<string> collection = [];

    collection.Add("Notebook");
    collection.Add("Mouse");
    collection.Add("Keyboard");
    collection.Remove("Mouse");

    Console.WriteLine(
        $"Count: {collection.Count}; items: {string.Join(", ", collection)}");
}

static void DemonstrateList()
{
    Console.WriteLine();
    Console.WriteLine("[List] Fast indexed access with dynamic size");

    var list = new DSList<string>();

    list.Add("A");
    list.Add("C");
    list.Insert(1, "B");

    Console.WriteLine(string.Join(" -> ", list));
}

static void DemonstrateLinkedList()
{
    Console.WriteLine();
    Console.WriteLine("[Linked List] Efficient insertion/removal at known nodes/ends");

    var list = new DSLinkedList<string>();

    list.AddLast("B");
    list.AddFirst("A");
    list.AddLast("C");
    list.Remove("B");

    Console.WriteLine(string.Join(" -> ", list));
}

static void DemonstrateStack()
{
    Console.WriteLine();
    Console.WriteLine("[Stack] LIFO");

    var stack = new DSStack<string>();

    stack.Push("First");
    stack.Push("Second");

    Console.WriteLine($"Pop: {stack.Pop()}");
}

static void DemonstrateQueue()
{
    Console.WriteLine();
    Console.WriteLine("[Queue] FIFO");

    var queue = new DSQueue<string>();

    queue.Enqueue("Client A");
    queue.Enqueue("Client B");

    Console.WriteLine($"Dequeue: {queue.Dequeue()}");
}

static void DemonstrateDeque()
{
    Console.WriteLine();
    Console.WriteLine("[Deque] Insert/remove at both ends");

    var deque = new DSDeque<string>();

    deque.AddLast("Normal");
    deque.AddFirst("Urgent");

    Console.WriteLine($"First: {deque.RemoveFirst()}");
}

static void DemonstratePriorityQueue()
{
    Console.WriteLine();
    Console.WriteLine("[Priority Queue] Lowest comparable value has priority");

    var queue = new DSPriorityQueue<int>();

    queue.Enqueue(30);
    queue.Enqueue(10);
    queue.Enqueue(20);

    Console.WriteLine($"Next priority: {queue.Dequeue()}");
}
