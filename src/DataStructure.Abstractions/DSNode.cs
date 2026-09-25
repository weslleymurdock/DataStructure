namespace DataStructure.Abstractions;

public class DSNode<T>(T value)
{
    public T Value { get; set; } = value;
    public DSNode<T>? Next { get; set; }
    public DSNode<T>? Previous { get; set; }
}