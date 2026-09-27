namespace DataStructure.Algorithms;

/// <summary>
/// Contains the value produced by an algorithm and the elapsed execution time.
/// </summary>
public readonly record struct AlgorithmResult<T>(
    T Value,
    TimeSpan Elapsed);
