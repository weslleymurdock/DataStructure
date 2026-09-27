namespace DataStructure.Abstractions;

/// <summary>
/// Binary search tree that keeps values smaller than a node on the left
/// and values greater than or equal to it on the right.
/// </summary>
public sealed class DSBinarySearchTree<T> where T : IComparable<T>
{
    public DSBinarySearchTreeNode<T>? Root { get; private set; }

    public int Count { get; private set; }

    public void Insert(T value)
    {
        if (Root is null)
        {
            Root = new DSBinarySearchTreeNode<T>(value);
            Count++;
            return;
        }

        var current = Root;

        while (true)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                if (current.Left is null)
                {
                    current.Left = new DSBinarySearchTreeNode<T>(value);
                    Count++;
                    return;
                }

                current = current.Left;
            }
            else
            {
                if (current.Right is null)
                {
                    current.Right = new DSBinarySearchTreeNode<T>(value);
                    Count++;
                    return;
                }

                current = current.Right;
            }
        }
    }

    public bool Contains(T value)
    {
        var current = Root;

        while (current is not null)
        {
            var comparison = value.CompareTo(current.Value);

            if (comparison == 0)
                return true;

            current = comparison < 0
                ? current.Left
                : current.Right;
        }

        return false;
    }

    public IEnumerable<T> InOrder()
        => TraverseInOrder(Root);

    public IEnumerable<T> PreOrder()
        => TraversePreOrder(Root);

    public IEnumerable<T> PostOrder()
        => TraversePostOrder(Root);

    private static IEnumerable<T> TraverseInOrder(DSBinarySearchTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraverseInOrder(node.Left))
            yield return value;

        yield return node.Value;

        foreach (var value in TraverseInOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraversePreOrder(DSBinarySearchTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        yield return node.Value;

        foreach (var value in TraversePreOrder(node.Left))
            yield return value;

        foreach (var value in TraversePreOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraversePostOrder(DSBinarySearchTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraversePostOrder(node.Left))
            yield return value;

        foreach (var value in TraversePostOrder(node.Right))
            yield return value;

        yield return node.Value;
    }
}

public sealed class DSBinarySearchTreeNode<T>(T value)
{
    public T Value { get; set; } = value;
    public DSBinarySearchTreeNode<T>? Left { get; set; }
    public DSBinarySearchTreeNode<T>? Right { get; set; }
}
