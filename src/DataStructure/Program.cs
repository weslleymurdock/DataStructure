using DataStructure.Abstractions;

Console.WriteLine("=== DATA STRUCTURES ===");
DemonstrateArray();
DemonstrateCollection();
DemonstrateList();
DemonstrateLinkedList();
DemonstrateStack();
DemonstrateQueue();
DemonstrateDeque();
DemonstratePriorityQueue();

static void DemonstrateArray()
{
    Console.WriteLine("\n[Array] Fixed-size, O(1) indexed access");
    var months = new DSArray<string>(["January", "February", "March"]);
    Console.WriteLine($"Item at index 1: {months[1]}");
}

static void DemonstrateCollection()
{
    Console.WriteLine("\n[Collection] Dynamic contiguous storage");
    var collection = new DSCollection<string>();
    collection.Add("Notebook");
    collection.Add("Mouse");
    collection.Add("Keyboard");
    collection.Remove("Mouse");
    Console.WriteLine($"Count: {collection.Count}; items: {string.Join(", ", collection)}");
}

static void DemonstrateList()
{
    Console.WriteLine("\n[List] Fast indexed access with dynamic size");
    var list = new DSList<string>();
    list.Add("A");
    list.Add("C");
    list.Insert(1, "B");
    Console.WriteLine(string.Join(" -> ", list));
}

static void DemonstrateLinkedList()
{
    Console.WriteLine("\n[Linked List] Efficient insertion/removal at known nodes/ends");
    var list = new DSLinkedList<string>();
    list.AddLast("B");
    list.AddFirst("A");
    list.AddLast("C");
    list.Remove("B");
    Console.WriteLine(string.Join(" -> ", list));
}

static void DemonstrateStack()
{
    Console.WriteLine("\n[Stack] LIFO");
    var stack = new DSStack<string>();
    stack.Push("First");
    stack.Push("Second");
    Console.WriteLine($"Pop: {stack.Pop()}");
}

static void DemonstrateQueue()
{
    Console.WriteLine("\n[Queue] FIFO");
    var queue = new DSQueue<string>();
    queue.Enqueue("Client A");
    queue.Enqueue("Client B");
    Console.WriteLine($"Dequeue: {queue.Dequeue()}");
}

static void DemonstrateDeque()
{
    Console.WriteLine("\n[Deque] Insert/remove at both ends");
    var deque = new DSDeque<string>();
    deque.AddLast("Normal");
    deque.AddFirst("Urgent");
    Console.WriteLine($"First: {deque.RemoveFirst()}");
}

static void DemonstratePriorityQueue()
{
    Console.WriteLine("\n[Priority Queue] Lowest comparable value has priority");
    var queue = new DSPriorityQueue<int>();
    queue.Enqueue(30);
    queue.Enqueue(10);
    queue.Enqueue(20);
    Console.WriteLine($"Next priority: {queue.Dequeue()}");
}
