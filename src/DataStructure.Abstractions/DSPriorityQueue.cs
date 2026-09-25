namespace DataStructure.Abstractions;

/// <summary>
/// Min-priority queue implemented as a binary heap.
/// Enqueue and dequeue are O(log n); peek is O(1).
/// </summary>
public sealed class DSPriorityQueue<T> where T : IComparable<T>
{
    private readonly DSList<T> _heap = new();

    public int Count => _heap.Count;

    public void Enqueue(T item)
    {
        _heap.Add(item);
        SiftUp(_heap.Count - 1);
    }

    public T Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        return _heap[0];
    }

    public T Dequeue()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        var result = _heap[0];
        var last = _heap[_heap.Count - 1];
        _heap.RemoveAt(_heap.Count - 1);

        if (_heap.Count > 0)
        {
            _heap[0] = last;
            SiftDown(0);
        }

        return result;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parent = (index - 1) / 2;
            if (_heap[parent].CompareTo(_heap[index]) <= 0)
                break;

            (_heap[parent], _heap[index]) = (_heap[index], _heap[parent]);
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

            if (left < _heap.Count && _heap[left].CompareTo(_heap[smallest]) < 0)
                smallest = left;

            if (right < _heap.Count && _heap[right].CompareTo(_heap[smallest]) < 0)
                smallest = right;

            if (smallest == index)
                return;

            (_heap[index], _heap[smallest]) = (_heap[smallest], _heap[index]);
            index = smallest;
        }
    }
}
