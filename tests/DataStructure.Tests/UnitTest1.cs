using DataStructure.Abstractions;
using DataStructure.Algorithms;

namespace DataStructure.Tests;

public class DataStructureTests
{
    [Fact]
    public void Array_ProvidesIndexedAccess()
    {
        var array = new DSArray<int>([1, 2, 3]);
        Assert.Equal(2, array[1]);
    }

    [Fact]
    public void List_ResizesAndInserts()
    {
        var list = new DSList<int>();
        list.Add(1);
        list.Add(3);
        list.Insert(1, 2);
        Assert.Equal([1, 2, 3], list);
    }

    [Fact]
    public void LinkedList_WorksAtBothEnds()
    {
        var list = new DSLinkedList<int>();
        list.AddFirst(2);
        list.AddFirst(1);
        list.AddLast(3);
        Assert.Equal([1, 2, 3], list);
        Assert.Equal(1, list.RemoveFirst());
        Assert.Equal(3, list.RemoveLast());
    }

    [Fact]
    public void Stack_IsLifo()
    {
        var stack = new DSStack<int>();
        stack.Push(1);
        stack.Push(2);
        Assert.Equal(2, stack.Pop());
        Assert.Equal(1, stack.Pop());
    }

    [Fact]
    public void Queue_IsFifo()
    {
        var queue = new DSQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        Assert.Equal(1, queue.Dequeue());
    }

    [Fact]
    public void Deque_WorksAtBothEnds()
    {
        var deque = new DSDeque<int>();
        deque.AddFirst(2);
        deque.AddFirst(1);
        deque.AddLast(3);
        Assert.Equal(1, deque.RemoveFirst());
        Assert.Equal(3, deque.RemoveLast());
    }

    [Fact]
    public void PriorityQueue_ReturnsMinimum()
    {
        var queue = new DSPriorityQueue<int>();
        queue.Enqueue(3);
        queue.Enqueue(1);
        queue.Enqueue(2);
        Assert.Equal(1, queue.Dequeue());
    }
    [Fact]
    public void HashSearch_FindsExistingKey()
    {
        var table = new DSHashTable<int, string>();
        table.Add(10, "ten");

        var result = new HashSearch().Execute(table, 10);

        Assert.True(result.Value);
    }

    [Fact]
    public void BreadthFirstSearch_VisitsByLevel()
    {
        var graph = new DSGraph<int>();
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 5);

        var result = new BreadthFirstSearch().Execute(graph, 1);

        Assert.Equal([1, 2, 3, 4, 5], result.Value);
    }

    [Fact]
    public void DepthFirstSearch_TraversesDepthFirst()
    {
        var graph = new DSGraph<int>();
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);

        var result = new DepthFirstSearch().Execute(graph, 1);

        Assert.Equal([1, 2, 4, 3], result.Value);
    }

    [Fact]
    public void Dijkstra_FindsShortestDistances()
    {
        var graph = new DSWeightedGraph<int>();
        graph.AddEdge(1, 2, 2);
        graph.AddEdge(1, 3, 5);
        graph.AddEdge(2, 3, 1);

        var result = new Dijkstra().Execute(graph, 1);

        Assert.Equal(3, result.Value[3]);
    }

    [Fact]
    public void AStar_FindsPath()
    {
        var graph = new DSWeightedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);

        var result = new AStar().Execute(graph, 1, 3, (_, _) => 0);

        Assert.Equal([1, 2, 3], result.Value);
    }

    [Fact]
    public void GreedyBestFirstSearch_FindsAPath()
    {
        var graph = new DSWeightedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);

        var result = new GreedyBestFirstSearch().Execute(graph, 1, 3, (_, _) => 0);

        Assert.Equal([1, 2, 3], result.Value);
    }

    [Fact]
    public void SimilaritySearch_RanksSimilarTextFirst()
    {
        var result = new SimilaritySearch().ExecuteText(
            "data structures",
            ["data structures", "chocolate cake"]);

        Assert.Equal("data structures", result[0].Value);
    }
}
