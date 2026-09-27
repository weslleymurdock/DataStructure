using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Searches a hash table by key.
/// </summary>
/// <remarks>
/// The lookup delegates to the hash table's bucket calculation and collision-chain
/// traversal. Average time is O(1) when keys are distributed evenly; the worst case
/// is O(n) when many keys collide.
/// </remarks>
public sealed class HashSearch
{
    /// <summary>
    /// Determines whether a key exists in the table.
    /// </summary>
    public AlgorithmResult<bool> Execute<TKey, TValue>(
        DSHashTable<TKey, TValue> data,
        TKey key)
        where TKey : notnull
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var found = data.ContainsKey(key);
        return new(found, stopwatch.Elapsed);
    }
}
