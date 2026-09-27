namespace DataStructure.Abstractions;

/// <summary>
/// Hash table that stores key/value pairs using separate chaining for collisions.
/// </summary>
/// <typeparam name="TKey">The key type used to calculate hash codes.</typeparam>
/// <typeparam name="TValue">The value associated with each key.</typeparam>
/// <remarks>
/// Average lookup, insertion, and removal are O(1) when the hash function
/// distributes keys evenly. Resizing is O(n) because all entries are rehashed.
/// </remarks>
public sealed class DSHashTable<TKey, TValue> where TKey : notnull
{
    private const double MaxLoadFactor = 0.75;
    private List<Entry>[] _buckets;
    private int _count;

    /// <summary>
    /// Creates an empty hash table.
    /// </summary>
    /// <param name="capacity">The initial number of buckets.</param>
    public DSHashTable(int capacity = 8)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _buckets = CreateBuckets(capacity);
    }

    /// <summary>
    /// Gets the number of key/value pairs stored in the table.
    /// </summary>
    public int Count => _count;

    /// <summary>
    /// Gets or sets the value associated with a key.
    /// </summary>
    /// <param name="key">The key to locate.</param>
    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var value))
                return value;

            throw new KeyNotFoundException($"The key '{key}' was not found.");
        }
        set => Set(key, value);
    }

    /// <summary>
    /// Adds a new key/value pair and rejects duplicate keys.
    /// </summary>
    public void Add(TKey key, TValue value)
    {
        var bucket = GetBucket(key);

        foreach (var entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
                throw new ArgumentException("A value with the same key already exists.", nameof(key));
        }

        EnsureCapacity(_count + 1);
        bucket = GetBucket(key);
        bucket.Add(new Entry(key, value));
        _count++;
    }

    /// <summary>
    /// Adds a key or updates the value when the key already exists.
    /// </summary>
    public void Set(TKey key, TValue value)
    {
        var bucket = GetBucket(key);

        for (var index = 0; index < bucket.Count; index++)
        {
            if (!EqualityComparer<TKey>.Default.Equals(bucket[index].Key, key))
                continue;

            bucket[index] = new Entry(key, value);
            return;
        }

        EnsureCapacity(_count + 1);
        GetBucket(key).Add(new Entry(key, value));
        _count++;
    }

    /// <summary>
    /// Tries to retrieve a value by key.
    /// </summary>
    public bool TryGetValue(TKey key, out TValue value)
    {
        var bucket = GetBucket(key);

        foreach (var entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
            {
                value = entry.Value;
                return true;
            }
        }

        value = default!;
        return false;
    }

    /// <summary>
    /// Determines whether the table contains the specified key.
    /// </summary>
    public bool ContainsKey(TKey key)
        => TryGetValue(key, out _);

    /// <summary>
    /// Removes the key/value pair associated with a key.
    /// </summary>
    public bool Remove(TKey key)
    {
        var bucket = GetBucket(key);

        for (var index = 0; index < bucket.Count; index++)
        {
            if (!EqualityComparer<TKey>.Default.Equals(bucket[index].Key, key))
                continue;

            bucket.RemoveAt(index);
            _count--;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes all key/value pairs from the table.
    /// </summary>
    public void Clear()
    {
        _buckets = CreateBuckets(_buckets.Length);
        _count = 0;
    }

    /// <summary>
    /// Enumerates the stored key/value pairs in bucket order.
    /// </summary>
    public IEnumerable<KeyValuePair<TKey, TValue>> Enumerate()
    {
        foreach (var bucket in _buckets)
        {
            foreach (var entry in bucket)
                yield return new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
        }
    }

    private List<Entry> GetBucket(TKey key)
        => _buckets[GetBucketIndex(key, _buckets.Length)];

    private static int GetBucketIndex(TKey key, int bucketCount)
        => (key.GetHashCode() & 0x7fffffff) % bucketCount;

    private void EnsureCapacity(int required)
    {
        if (required <= _buckets.Length * MaxLoadFactor)
            return;

        var oldEntries = Enumerate().ToArray();
        _buckets = CreateBuckets(_buckets.Length * 2);

        foreach (var entry in oldEntries)
            GetBucket(entry.Key).Add(new Entry(entry.Key, entry.Value));
    }

    private static List<Entry>[] CreateBuckets(int count)
    {
        var buckets = new List<Entry>[count];

        for (var index = 0; index < count; index++)
            buckets[index] = [];

        return buckets;
    }

    private readonly record struct Entry(TKey Key, TValue Value);
}
