namespace DataStructure.Abstractions;

/// <summary>
/// Binary tree node structure with explicit left and right children.
/// </summary>
public sealed class DSBinaryTree<T>
{
    public DSBinaryTree(T value)
    {
        Root = new DSBinaryTreeNode<T>(value);
    }

    public DSBinaryTreeNode<T> Root { get; }

    public IEnumerable<T> PreOrder()
        => TraversePreOrder(Root);

    public IEnumerable<T> InOrder()
        => TraverseInOrder(Root);

    public IEnumerable<T> PostOrder()
        => TraversePostOrder(Root);

    private static IEnumerable<T> TraversePreOrder(DSBinaryTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        yield return node.Value;

        foreach (var value in TraversePreOrder(node.Left))
            yield return value;

        foreach (var value in TraversePreOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraverseInOrder(DSBinaryTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraverseInOrder(node.Left))
            yield return value;

        yield return node.Value;

        foreach (var value in TraverseInOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraversePostOrder(DSBinaryTreeNode<T>? node)
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

public sealed class DSBinaryTreeNode<T>(T value)
{
    public T Value { get; set; } = value;
    public DSBinaryTreeNode<T>? Left { get; set; }
    public DSBinaryTreeNode<T>? Right { get; set; }
}
