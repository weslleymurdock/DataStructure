namespace DataStructure.Abstractions;

/// <summary>
/// Min-priority queue implemented with a binary heap.
/// Enqueue and dequeue are O(log n); peek is O(1).
/// </summary>
public sealed class DSPriorityQueue<T> where T : IComparable<T>
{
    private readonly DSList<T> _heap = [];

    /// <summary>Gets the number of values in the heap.</summary>
    public int Count => _heap.Count;

    /// <summary>Adds a value and moves it upward until the heap property is restored.</summary>
    public void Enqueue(T item)
    {
        // Appending keeps the heap complete before the value is moved upward.
        _heap.Add(item);

        SiftUp(_heap.Count - 1);
    }

    /// <summary>Returns the smallest value without removing it.</summary>
    public T Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        // In a min-heap the root is always the smallest value.
        return _heap[0];
    }

    /// <summary>Removes and returns the smallest value.</summary>
    public T Dequeue()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        var result = _heap[0];
        var last = _heap[^1];

        // Remove the last node first, preserving the complete-tree shape.
        _heap.RemoveAt(_heap.Count - 1);

        if (_heap.Count > 0)
        {
            // Move the last value to the root and repair the heap downward.
            _heap[0] = last;
            SiftDown(0);
        }

        return result;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            // Every node is stored at an array index derived from its parent.
            var parent = (index - 1) / 2;

            // The heap property is already valid when parent <= child.
            if (_heap[parent].CompareTo(_heap[index]) <= 0)
                break;

            // Otherwise exchange the child with its parent and continue upward.
            (_heap[parent], _heap[index]) =
                (_heap[index], _heap[parent]);

            index = parent;
        }
    }

    private void SiftDown(int index)
    {
        while (true)
        {
            var left = index * 2 + 1;
            var right = left + 1;
            var smallest = index;

            // Select the smallest existing child.
            if (_heap.Count > left &&
                _heap[left].CompareTo(_heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (_heap.Count > right &&
                _heap[right].CompareTo(_heap[smallest]) < 0)
            {
                smallest = right;
            }

            // No child is smaller, so the heap property is restored.
            if (smallest == index)
                return;

            (_heap[index], _heap[smallest]) =
                (_heap[smallest], _heap[index]);

            index = smallest;
        }
    }
}
