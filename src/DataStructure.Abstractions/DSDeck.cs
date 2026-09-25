namespace DataStructure.Abstractions;

// ==========================================
// 2. DECK / DEQUE - Fila de Duas Pontas O(1)
// ==========================================
public sealed class DSDeck<T>
{
    private DSNode<T>? _head; // Topo/Início
    private DSNode<T>? _tail; // Base/Fim

    public int Count { get; private set; }

    // Insere no Início O(1)
    public void AddFirst(T item)
    {
        var newNode = new DSNode<T>(item);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode; // O head antigo agora tem o novo como anterior
            _head = newNode;
        }
        Count++;
    }

    // Insere no Final O(1)
    public void AddLast(T item)
    {
        var newNode = new DSNode<T>(item);

        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail; // O novo aponta para o tail antigo
            _tail = newNode;
        }
        Count++;
    }

    // Remove do Início O(1)
    public T RemoveFirst()
    {
        if (_head == null)
            throw new InvalidOperationException("O deque está vazio.");

        T value = _head.Value;
        _head = _head.Next; // O segundo vira o primeiro

        if (_head == null)
            _tail = null; // Esvaziou
        else
            _head.Previous = null; // Corta a ligação para trás (ajuda o Garbage Collector)

        Count--;
        return value;
    }

    // Remove do Final O(1)
    public T RemoveLast()
    {
        if (_tail == null)
            throw new InvalidOperationException("O deque está vazio.");

        T value = _tail.Value;
        _tail = _tail.Previous; // O penúltimo vira o último

        if (_tail == null)
            _head = null; // Esvaziou
        else
            _tail.Next = null; // Corta a ligação para a frente

        Count--;
        return value;
    }
}