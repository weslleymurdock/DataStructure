namespace DataStructure.Abstractions;

/// <summary>
/// Max-heap storing the largest value at the root.
/// </summary>
/// <typeparam name="T">The comparable value type stored by the heap.</typeparam>
/// <remarks>
/// A max-heap is a complete binary tree represented compactly by an array.
/// Every parent is greater than or equal to its children, so the maximum
/// element is always available at the root.
/// </remarks>
public sealed class DSMaxHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = [];

    /// <summary>
    /// Gets the number of values currently stored in the heap.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Adds a value and restores the max-heap property by moving it upward.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public void Add(T value)
    {
        _items.Add(value);
        SiftUp(_items.Count - 1);
    }

    /// <summary>
    /// Returns the largest value without removing it.
    /// </summary>
    /// <returns>The value at the root.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the heap is empty.</exception>
    public T Peek()
        => _items.Count == 0
            ? throw new InvalidOperationException("The heap is empty.")
            : _items[0];

    /// <summary>
    /// Removes and returns the largest value, then restores the max-heap property.
    /// </summary>
    /// <returns>The largest value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the heap is empty.</exception>
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

    /// <summary>
    /// Exposes the internal level-order representation without copying it.
    /// </summary>
    /// <returns>The heap values in array representation.</returns>
    public IReadOnlyList<T> AsArray()
        => _items;

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parent = (index - 1) / 2;

            if (_items[parent].CompareTo(_items[index]) >= 0)
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
            var largest = index;

            if (left < _items.Count &&
                _items[left].CompareTo(_items[largest]) > 0)
                largest = left;

            if (right < _items.Count &&
                _items[right].CompareTo(_items[largest]) > 0)
                largest = right;

            if (largest == index)
                return;

            (_items[index], _items[largest]) =
                (_items[largest], _items[index]);

            index = largest;
        }
    }
}
