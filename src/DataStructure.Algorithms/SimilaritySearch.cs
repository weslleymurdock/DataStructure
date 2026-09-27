namespace DataStructure.Algorithms;

/// <summary>
/// Ranks text or numeric feature vectors by similarity to a query.
/// </summary>
/// <remarks>
/// Text similarity uses Jaccard similarity over normalized tokens. Numeric vectors
/// use cosine similarity. Both return values from 0 to 1 for the supported inputs.
/// </remarks>
public sealed class SimilaritySearch
{
    /// <summary>
    /// Ranks candidate texts by Jaccard token similarity.
    /// </summary>
    public IReadOnlyList<SimilarityResult<string>> ExecuteText(
        string query,
        IEnumerable<string> candidates)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(candidates);

        var queryTokens = Tokenize(query);

        return candidates
            .Select(candidate =>
                new SimilarityResult<string>(
                    candidate,
                    Jaccard(queryTokens, Tokenize(candidate))))
            .OrderByDescending(result => result.Score)
            .ToArray();
    }

    /// <summary>
    /// Ranks feature vectors by cosine similarity.
    /// </summary>
    public IReadOnlyList<SimilarityResult<IReadOnlyList<double>>> ExecuteVectors(
        IReadOnlyList<double> query,
        IEnumerable<IReadOnlyList<double>> candidates)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(candidates);

        return candidates
            .Select(candidate =>
                new SimilarityResult<IReadOnlyList<double>>(
                    candidate,
                    Cosine(query, candidate)))
            .OrderByDescending(result => result.Score)
            .ToArray();
    }

    private static HashSet<string> Tokenize(string value)
        => value.Split(
                [' ', '\t', '\r', '\n', '.', ',', ';', ':', '!', '?'],
                StringSplitOptions.RemoveEmptyEntries)
            .Select(token => token.Trim().ToLowerInvariant())
            .ToHashSet();

    private static double Jaccard(
        IReadOnlySet<string> left,
        IReadOnlySet<string> right)
    {
        if (left.Count == 0 && right.Count == 0)
            return 1;

        if (left.Count == 0 || right.Count == 0)
            return 0;

        var intersection = left.Intersect(right).Count();
        var union = left.Union(right).Count();
        return (double)intersection / union;
    }

    private static double Cosine(
        IReadOnlyList<double> left,
        IReadOnlyList<double> right)
    {
        if (left.Count != right.Count || left.Count == 0)
            return 0;

        double dot = 0;
        double leftNorm = 0;
        double rightNorm = 0;

        for (var index = 0; index < left.Count; index++)
        {
            dot += left[index] * right[index];
            leftNorm += left[index] * left[index];
            rightNorm += right[index] * right[index];
        }

        if (leftNorm == 0 || rightNorm == 0)
            return 0;

        return Math.Clamp(dot / (Math.Sqrt(leftNorm) * Math.Sqrt(rightNorm)), 0, 1);
    }
}

/// <summary>
/// Represents a candidate and its similarity score.
/// </summary>
public readonly record struct SimilarityResult<T>(T Value, double Score);
