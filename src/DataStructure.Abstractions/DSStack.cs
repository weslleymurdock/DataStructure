namespace DataStructure.Abstractions;

/// <summary>
/// LIFO stack implemented with a singly linked chain.
/// Push, pop and peek are O(1) because the top is stored explicitly.
/// </summary>
public sealed class DSStack<T>
{
    private DSNode<T>? _top;

    /// <summary>Gets the number of values currently stored.</summary>
    public int Count { get; private set; }

    /// <summary>Places a value on the top of the stack in O(1).</summary>
    public void Push(T item)
    {
        // The new node points to the previous top.
        var node = new DSNode<T>(item)
        {
            Next = _top
        };

        // The new node becomes the only entry point to the chain.
        _top = node;
        Count++;
    }

    /// <summary>Removes and returns the top value in O(1).</summary>
    public T Pop()
    {
        if (_top is null)
            throw new InvalidOperationException("The stack is empty.");

        var value = _top.Value;

        // The second node becomes the new top.
        _top = _top.Next;
        Count--;

        return value;
    }

    /// <summary>Returns the top value without removing it in O(1).</summary>
    public T Peek()
    {
        if (_top is null)
            throw new InvalidOperationException("The stack is empty.");

        return _top.Value;
    }
}
