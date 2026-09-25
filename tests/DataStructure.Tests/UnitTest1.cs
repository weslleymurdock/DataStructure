using DataStructure.Abstractions;

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
}
