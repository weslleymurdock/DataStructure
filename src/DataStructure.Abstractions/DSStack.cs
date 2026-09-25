namespace DataStructure.Abstractions;

/// <summary>
/// LIFO stack implemented with a linked list.
/// </summary>
public sealed class DSStack<T>
{
    private DSNode<T>? _top;

    public int Count { get; private set; }

    public void Push(T item)
    {
        var node = new DSNode<T>(item)
        {
            Next = _top
        };

        _top = node;
        Count++;
    }

    public T Pop()
    {
        if (_top is null)
            throw new InvalidOperationException("The stack is empty.");

        var value = _top.Value;
        _top = _top.Next;
        Count--;
        return value;
    }

    public T Peek()
    {
        if (_top is null)
            throw new InvalidOperationException("The stack is empty.");

        return _top.Value;
    }
}
