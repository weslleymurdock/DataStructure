namespace DataStructure.github.io.Services;

/// <summary>
/// Builds faithful tree-shaped visual models for data structures whose internal
/// organization is naturally hierarchical or node-based.
/// </summary>
public static class StructureTreeCatalog
{
    /// <summary>
    /// Gets the visual representation for a structure.
    /// </summary>
    public static AlgorithmTreeDataNode<string>? For(string key) =>
        key switch
        {
            "binarytree" => BinaryTree(),
            "binarysearchtree" => BinarySearchTree(),
            "heap" => Heap(false),
            "maxheap" => Heap(true),
            "priorityqueue" => Heap(false, "PriorityQueue"),
            "linkedlist" => DoublyLinked("DSLinkedList<T>"),
            "nodelist" => SinglyLinked("DSNodeList<T>"),
            "circularlinkedlist" => CircularLinked(),
            "queue" => SinglyLinked("DSQueue<T>", "FIFO"),
            "stack" => Stack(),
            "deque" => DoublyLinked("DSDeque<T>", "Deque"),
            "deck" => DoublyLinked("DSDeck<T>", "Deck"),
            _ => null
        };

    /// <summary>
    /// Indicates whether a structure has a dedicated structural visualization.
    /// </summary>
    public static bool HasVisualization(string key) => For(key) is not null;

    private static AlgorithmTreeDataNode<string> BinaryTree() =>
        new("Root: 8",
        [
            new("Left: 4",
            [
                new("Left: 2", []),
                new("Right: 6", [])
            ]),
            new("Right: 12",
            [
                new("Left: 10", []),
                new("Right: 14", [])
            ])
        ]);

    private static AlgorithmTreeDataNode<string> BinarySearchTree() =>
        new("Root: 8",
        [
            new("Left: 4  (< 8)",
            [
                new("Left: 2  (< 4)", []),
                new("Right: 6  (>= 4)", [])
            ]),
            new("Right: 12  (>= 8)",
            [
                new("Left: 10  (< 12)", []),
                new("Right: 14  (>= 12)", [])
            ])
        ]);

    private static AlgorithmTreeDataNode<string> Heap(bool max, string? name = null)
    {
        var title = name ?? (max ? "DSMaxHeap<T>" : "DSHeap<T>");
        var root = max ? "Root: 90 (maximum)" : "Root: 10 (minimum)";
        var left = max ? "Left: 70" : "Left: 30";
        var right = max ? "Right: 80" : "Right: 50";

        return new($"{title}\n{root}",
        [
            new(left,
            [
                new(max ? "40" : "40", []),
                new(max ? "60" : "20", [])
            ]),
            new(right,
            [
                new(max ? "30" : "60", []),
                new(max ? "50" : "70", [])
            ])
        ]);
    }

    private static AlgorithmTreeDataNode<string> DoublyLinked(string name, string? role = null) =>
        new($"{name}{(role is null ? string.Empty : $" — {role}")}",
        [
            new("Head → Node 1\nValue: A\nPrevious: null\nNext: Node 2", []),
            new("Node 2\nValue: B\nPrevious: Node 1\nNext: Node 3", []),
            new("Node 3\nValue: C\nPrevious: Node 2\nNext: null\n← Tail", [])
        ]);

    private static AlgorithmTreeDataNode<string> SinglyLinked(string name, string? role = null) =>
        new($"{name}{(role is null ? string.Empty : $" — {role}")}",
        [
            new("Head → Node 1\nValue: A\nNext: Node 2", []),
            new("Node 2\nValue: B\nNext: Node 3", []),
            new("Node 3\nValue: C\nNext: null\n← Tail", [])
        ]);

    private static AlgorithmTreeDataNode<string> CircularLinked() =>
        new("DSCircularLinkedList<T>",
        [
            new("Head → Node 1\nValue: A\nNext: Node 2", []),
            new("Node 2\nValue: B\nNext: Node 3", []),
            new("Tail → Node 3\nValue: C\nNext: Head", [])
        ]);

    private static AlgorithmTreeDataNode<string> Stack() =>
        new("DSStack<T>\nTop",
        [
            new("Node 1\nValue: C\nNext: Node 2", []),
            new("Node 2\nValue: B\nNext: Node 3", []),
            new("Node 3\nValue: A\nNext: null\nBase", [])
        ]);
}
