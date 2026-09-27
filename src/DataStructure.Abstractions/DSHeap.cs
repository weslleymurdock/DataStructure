namespace DataStructure.Abstractions;

/// <summary>
/// Min-heap storing the smallest value at the root.
/// </summary>
public sealed class DSHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = [];

    public int Count => _items.Count;

    public void Add(T value)
    {
        _items.Add(value);
        SiftUp(_items.Count - 1);
    }

    public T Peek()
        => _items.Count == 0
            ? throw new InvalidOperationException("The heap is empty.")
            : _items[0];

    public T Remove()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("The heap is empty.");

        var result = _items[0];
        var last = _items[^1];
        _items.RemoveAt(_items.Count - 1);

        if (_items.Count > 0)
        {
            _items[0] = last;
            SiftDown(0);
        }

        return result;
    }

    public IReadOnlyList<T> AsArray()
        => _items;

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parent = (index - 1) / 2;

            if (_items[parent].CompareTo(_items[index]) <= 0)
                return;

            (_items[parent], _items[index]) =
                (_items[index], _items[parent]);

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

            if (left < _items.Count &&
                _items[left].CompareTo(_items[smallest]) < 0)
                smallest = left;

            if (right < _items.Count &&
                _items[right].CompareTo(_items[smallest]) < 0)
                smallest = right;

            if (smallest == index)
                return;

            (_items[index], _items[smallest]) =
                (_items[smallest], _items[index]);

            index = smallest;
        }
    }
}
