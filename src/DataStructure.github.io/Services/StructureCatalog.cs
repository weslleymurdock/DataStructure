namespace DataStructure.github.io.Services;

public sealed record MethodGuide(string Name,string Signature,string Explanation,string Complexity);

public sealed record StructureGuide(
    string Key,
    string Category,
    string TypeName,
    string Name,
    string Description,
    string UseCase,
    string Source,
    IReadOnlyList<MethodGuide> Methods)
{
    public string Utility =>
        Key switch
        {
            "maxheap" =>
                "Mantém o maior elemento na raiz, permitindo consultas imediatas ao máximo e remoções em tempo logarítmico.",
            _ => UseCase
        };
}

public static class StructureCatalog
{
    public static IReadOnlyList<StructureGuide> All { get; } =
    [
        new("array","linear","DSArray","Array","Armazenamento contíguo de tamanho fixo com acesso indexado.","Quando o tamanho é conhecido e o acesso direto por índice é importante.",@"namespace DataStructure.Abstractions;

public sealed class DSArray<T> : IReadOnlyList<T>
{
    private readonly T[] _items;

    public DSArray(int length)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        _items = new T[length];
    }

    public DSArray(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        _items = [.. items];
    }

    public int Count => _items.Length;

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public IEnumerator<T> GetEnumerator()
        => ((IEnumerable<T>)_items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}",
            [
                new("Construtor","public DSArray(int length)","Aloca o armazenamento inicial da estrutura.","O(n)"),
                new("Count","public int Count","Informa quantos elementos ou posições a estrutura possui.","O(1)"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
            ]),
        new("collection","linear","DSCollection","Coleção dinâmica","Coleção contígua de tamanho variável com crescimento automático.","Como coleção mutável geral quando append e acesso indexado são frequentes.",@"namespace DataStructure.Abstractions;

public class DSCollection<T> : ICollection<T>
{
    private T[] _items;
    private int _count;

    public DSCollection(int capacity = 4)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _items = capacity == 0 ? [] : new T[capacity];
    }

    public int Count => _count;

    public bool IsReadOnly => false;

    public virtual void Add(T item)
    {

        EnsureCapacity(_count + 1);

        _items[_count++] = item;
    }

    public void Clear()
    {

        Array.Clear(_items, 0, _count);
        _count = 0;
    }

    public bool Contains(T item)
        => IndexOf(item) >= 0;

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 ||
            arrayIndex > array.Length ||
            array.Length - arrayIndex < _count)
        {
            throw new ArgumentException(
                ""The destination array is too small."",
                nameof(array));
        }

        Array.Copy(_items, 0, array, arrayIndex, _count);
    }

    public bool Remove(T item)
    {

        var index = IndexOf(item);

        if (index < 0)
            return false;

        RemoveAt(index);
        return true;
    }

    public T this[int index]
    {
        get
        {
            ValidateIndex(index);
            return _items[index];
        }
        set
        {
            ValidateIndex(index);
            _items[index] = value;
        }
    }

    protected int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < _count; index++)
        {
            if (comparer.Equals(_items[index], item))
                return index;
        }

        return -1;
    }

    protected void InsertAt(int index, T item)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        EnsureCapacity(_count + 1);

        Array.Copy(
            _items,
            index,
            _items,
            index + 1,
            _count - index);

        _items[index] = item;
        _count++;
    }

    protected T RemoveAt(int index)
    {
        ValidateIndex(index);

        var value = _items[index];

        Array.Copy(
            _items,
            index + 1,
            _items,
            index,
            _count - index - 1);

        _items[--_count] = default!;

        return value;
    }

    protected void EnsureCapacity(int required)
    {
        if (required <= _items.Length)
            return;

        var capacity = _items.Length == 0 ? 4 : _items.Length;

        while (capacity < required)
            capacity *= 2;

        Array.Resize(ref _items, capacity);
    }

    private void ValidateIndex(int index)
    {

        if ((uint)index >= (uint)_count)
            throw new ArgumentOutOfRangeException(nameof(index));
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var index = 0; index < _count; index++)
            yield return _items[index];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}",
            [
                new("Construtor","public DSArray(int length)","Aloca o armazenamento inicial da estrutura.","O(n)"),
                new("Add","public void Add(T item)","Adiciona um valor ao final da estrutura.","O(1) amortizado"),
                new("Clear","public void Clear()","Remove logicamente todos os valores armazenados.","O(n)"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
                new("CopyTo","public void CopyTo(T[] array, int arrayIndex)","Copia os valores lógicos para um vetor de destino.","O(n)"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
                new("IndexOf","public int IndexOf(T item)","Procura linearmente a primeira ocorrência.","O(n)"),
                new("InsertAt","protected void InsertAt(int index, T item)","Abre espaço deslocando o trecho à direita.","O(n)"),
                new("RemoveAt","protected T RemoveAt(int index)","Remove uma posição e desloca o trecho seguinte.","O(n)"),
                new("EnsureCapacity","protected void EnsureCapacity(int required)","Amplia o armazenamento quando a capacidade atual não é suficiente.","O(n) no redimensionamento"),
            ]),
        new("list","linear","DSList","Lista dinâmica","Especializa a coleção dinâmica para expor inserção e remoção por índice.","Quando é necessário combinar acesso por índice com inserção e remoção.",@"namespace DataStructure.Abstractions;

public sealed class DSList<T>(int capacity = 4) : DSCollection<T>(capacity), IReadOnlyList<T>
{

    public void Insert(int index, T item)
        => InsertAt(index, item);

    public new T RemoveAt(int index)
        => base.RemoveAt(index);

    public new int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < Count; index++)
        {
            if (comparer.Equals(this[index], item))
                return index;
        }

        return -1;
    }

    public new bool Contains(T item)
        => IndexOf(item) >= 0;
}",
            [
                new("Insert","public void Insert(int index, T item)","Localiza a posição e conecta ou desloca os elementos necessários.","O(n)"),
                new("RemoveAt","protected T RemoveAt(int index)","Remove uma posição e desloca o trecho seguinte.","O(n)"),
                new("IndexOf","public int IndexOf(T item)","Procura linearmente a primeira ocorrência.","O(n)"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
            ]),
        new("linkedlist","linear","DSLinkedList","Lista duplamente ligada","Cada nó aponta para o anterior e o próximo.","Quando inserções e remoções nas extremidades ou após localizar um nó são importantes.",@"namespace DataStructure.Abstractions;

public sealed class DSLinkedList<T> : IReadOnlyList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public T this[int index]
        => GetNode(index).Value;

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {

            _head = _tail = node;
        }
        else
        {

            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {

            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The linked list is empty."");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The linked list is empty."");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index == 0)
        {
            AddFirst(item);
            return;
        }

        if (index == Count)
        {
            AddLast(item);
            return;
        }

        var current = GetNode(index);
        var node = new DSNode<T>(item)
        {
            Previous = current.Previous,
            Next = current
        };

        current.Previous!.Next = node;
        current.Previous = node;
        Count++;
    }

    public bool Remove(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        var current = _head;

        while (current is not null)
        {
            if (comparer.Equals(current.Value, item))
            {
                if (current.Previous is null)
                    RemoveFirst();
                else if (current.Next is null)
                    RemoveLast();
                else
                {

                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    Count--;
                }

                return true;
            }

            current = current.Next;
        }

        return false;
    }

    private DSNode<T> GetNode(int index)
    {
        if ((uint)index >= (uint)Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index < Count / 2)
        {
            var current = _head!;

            for (var position = 0; position < index; position++)
                current = current.Next!;

            return current;
        }

        var reverse = _tail!;

        for (var position = Count - 1; position > index; position--)
            reverse = reverse.Previous!;

        return reverse;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;

        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
                new("Insert","public void Insert(int index, T item)","Localiza a posição e conecta ou desloca os elementos necessários.","O(n)"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
            ]),
        new("nodelist","linear","DSNodeList","Lista simplesmente ligada","Cadeia unidirecional de nós com referência ao primeiro e último nó.","Para estudar encadeamento simples e o custo de percorrer uma lista.",@"namespace DataStructure.Abstractions;

public sealed class DSNodeList<T> where T : notnull
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void Add(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {

            _head = _tail = node;
        }
        else
        {

            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T this[int index]
    {
        get
        {
            if ((uint)index >= (uint)Count)
                throw new IndexOutOfRangeException();

            var current = _head;

            for (var position = 0; position < index; position++)
                current = current!.Next;

            return current!.Value;
        }
    }

    public bool Remove(T item)
    {
        if (_head is null)
            return false;

        if (_head.Value.Equals(item))
        {

            _head = _head.Next;

            if (_head is null)
                _tail = null;

            Count--;
            return true;
        }

        var current = _head;

        while (current.Next is not null)
        {
            if (!current.Next.Value.Equals(item))
            {
                current = current.Next;
                continue;
            }

            current.Next = current.Next.Next;

            if (current.Next is null)
                _tail = current;

            Count--;
            return true;
        }

        return false;
    }
}",
            [
                new("Add","public void Add(T item)","Adiciona um valor ao final da estrutura.","O(1) amortizado"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
            ]),
        new("circularlinkedlist","linear","DSCircularLinkedList","Lista circular","O último nó aponta novamente para o primeiro, formando um ciclo.","Em percursos cíclicos, escalas, turnos e algoritmos que precisam retornar ao início.",@"namespace DataStructure.Abstractions;

public sealed class DSCircularLinkedList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
            node.Next = node;
        }
        else
        {
            node.Next = _head;
            _head = node;
            _tail!.Next = _head;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
            node.Next = node;
        }
        else
        {
            node.Next = _head;
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The circular list is empty."");

        var value = _head.Value;

        if (Count == 1)
        {
            _head = _tail = null;
        }
        else
        {
            _head = _head.Next;
            _tail!.Next = _head;
        }

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The circular list is empty."");

        var value = _tail.Value;

        if (Count == 1)
        {
            _head = _tail = null;
        }
        else
        {
            var current = _head!;

            while (current.Next != _tail)
                current = current.Next!;

            current.Next = _head;
            _tail = current;
        }

        Count--;
        return value;
    }

    public bool Contains(T item)
    {
        if (_head is null)
            return false;

        var comparer = EqualityComparer<T>.Default;
        var current = _head;

        do
        {
            if (comparer.Equals(current.Value, item))
                return true;

            current = current.Next!;
        }
        while (current != _head);

        return false;
    }

    public IEnumerable<T> Enumerate()
    {
        if (_head is null)
            yield break;

        var current = _head;

        do
        {
            yield return current.Value;
            current = current.Next!;
        }
        while (current != _head);
    }
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
                new("Enumerate","public IEnumerable<T> Enumerate()","Percorre exatamente uma volta pelo ciclo, evitando repetição infinita.","O(n)"),
            ]),
        new("queue","linear","DSQueue","Fila","Estrutura FIFO: o primeiro elemento inserido é o primeiro removido.","Processamento por ordem de chegada e filas de tarefas.",@"namespace DataStructure.Abstractions;

public sealed class DSQueue<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {

            _head = _tail = node;
        }
        else
        {

            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T Dequeue()
    {
        if (_head is null)
            throw new InvalidOperationException(""The queue is empty."");

        var value = _head.Value;

        _head = _head.Next;

        if (_head is null)
            _tail = null;

        Count--;
        return value;
    }

    public T Peek()
    {
        if (_head is null)
            throw new InvalidOperationException(""The queue is empty."");

        return _head.Value;
    }
}",
            [
                new("Enqueue","public void Enqueue(T item)","Insere um item respeitando a extremidade de entrada da fila.","O(1)"),
                new("Dequeue","public T Dequeue()","Remove o próximo item segundo a política FIFO.","O(1)"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
            ]),
        new("stack","linear","DSStack","Pilha","Estrutura LIFO: o último elemento inserido é o primeiro removido.","Desfazer ações, chamadas aninhadas e processamento reverso.",@"namespace DataStructure.Abstractions;

public sealed class DSStack<T>
{
    private DSNode<T>? _top;

    public int Count { get; private set; }

    public void Push(T item)
    {

        var node = new DSNode<T>(item)
        {
            Next = _top
        };

        _top = node;
        Count++;
    }

    public T Pop()
    {
        if (_top is null)
            throw new InvalidOperationException(""The stack is empty."");

        var value = _top.Value;

        _top = _top.Next;
        Count--;

        return value;
    }

    public T Peek()
    {
        if (_top is null)
            throw new InvalidOperationException(""The stack is empty."");

        return _top.Value;
    }
}",
            [
                new("Push","public void Push(T item)","Coloca um valor no topo da pilha.","O(1)"),
                new("Pop","public T Pop()","Remove e retorna o valor no topo.","O(1)"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
            ]),
        new("deque","linear","DSDeque","Deque","Permite inserir, remover e consultar os dois extremos.","Quando ambos os lados precisam de operações eficientes.",@"namespace DataStructure.Abstractions;

public sealed class DSDeque<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
        }
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {
            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The deque is empty."");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The deque is empty."");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }

    public T PeekFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The deque is empty."");

        return _head.Value;
    }

    public T PeekLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The deque is empty."");

        return _tail.Value;
    }
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
                new("PeekFirst","public T PeekFirst()","Consulta o valor da primeira extremidade do deque.","O(1)"),
                new("PeekLast","public T PeekLast()","Consulta o valor da última extremidade do deque.","O(1)"),
            ]),
        new("deck","linear","DSDeck","Deque por nós","Variação de deque baseada diretamente em nós bidirecionais.","Como exemplo didático de manipulação de duas extremidades.",@"namespace DataStructure.Abstractions;

public sealed class DSDeck<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
            _head = _tail = node;
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
            _head = _tail = node;
        else
        {
            _tail.Next = node;
            node.Previous = _tail;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The deck is empty."");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The deck is empty."");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
            ]),
        new("priorityqueue","nonlinear","DSPriorityQueue","Fila de prioridade","Heap mínimo que sempre expõe o menor valor como próxima prioridade.","Escalonamento e processamento em ordem de prioridade.",@"namespace DataStructure.Abstractions;

public sealed class DSPriorityQueue<T> where T : IComparable<T>
{
    private readonly DSList<T> _heap = [];

    public int Count => _heap.Count;

    public void Enqueue(T item)
    {

        _heap.Add(item);

        SiftUp(_heap.Count - 1);
    }

    public T Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException(""The priority queue is empty."");

        return _heap[0];
    }

    public T Dequeue()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException(""The priority queue is empty."");

        var result = _heap[0];
        var last = _heap[^1];

        _heap.RemoveAt(_heap.Count - 1);

        if (_heap.Count > 0)
        {

            _heap[0] = last;
            SiftDown(0);
        }

        return result;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {

            var parent = (index - 1) / 2;

            if (_heap[parent].CompareTo(_heap[index]) <= 0)
                break;

            (_heap[parent], _heap[index]) =
                (_heap[index], _heap[parent]);

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

            if (_heap.Count > left &&
                _heap[left].CompareTo(_heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (_heap.Count > right &&
                _heap[right].CompareTo(_heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest == index)
                return;

            (_heap[index], _heap[smallest]) =
                (_heap[smallest], _heap[index]);

            index = smallest;
        }
    }
}",
            [
                new("Enqueue","public void Enqueue(T item)","Insere um item respeitando a extremidade de entrada da fila.","O(1)"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
                new("Dequeue","public T Dequeue()","Remove o próximo item segundo a política FIFO.","O(1)"),
                new("SiftUp","private void SiftUp(int index)","Sobe um elemento enquanto ele viola a propriedade do heap com seu pai.","O(log n)"),
                new("SiftDown","private void SiftDown(int index)","Desce um elemento enquanto um dos filhos possui prioridade maior.","O(log n)"),
            ]),
        new("binarytree","nonlinear","DSBinaryTree","Árvore binária","Cada nó possui no máximo dois filhos e a estrutura oferece três percursos clássicos.","Representação hierárquica e estudo de percursos em árvores.",@"namespace DataStructure.Abstractions;

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
}",
            [
                new("Construtor","public DSArray(int length)","Aloca o armazenamento inicial da estrutura.","O(n)"),
                new("Root","public DSBinaryTreeNode<T> Root","Expõe o nó raiz a partir do qual a árvore é percorrida.","O(1)"),
                new("PreOrder","public IEnumerable<T> PreOrder()","Visita raiz, subárvore esquerda e subárvore direita.","O(n)"),
                new("InOrder","public IEnumerable<T> InOrder()","Visita esquerda, raiz e direita; em uma árvore de busca produz ordem crescente.","O(n)"),
                new("PostOrder","public IEnumerable<T> PostOrder()","Visita as duas subárvores antes da raiz.","O(n)"),
            ]),
        new("binarysearchtree","nonlinear","DSBinarySearchTree","Árvore binária de busca","Valores menores seguem para a esquerda e valores maiores ou iguais para a direita.","Busca ordenada, inserção hierárquica e estudo de árvores de busca.",@"namespace DataStructure.Abstractions;

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
}",
            [
                new("Insert","public void Insert(int index, T item)","Localiza a posição e conecta ou desloca os elementos necessários.","O(n)"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
                new("InOrder","public IEnumerable<T> InOrder()","Visita esquerda, raiz e direita; em uma árvore de busca produz ordem crescente.","O(n)"),
                new("PreOrder","public IEnumerable<T> PreOrder()","Visita raiz, subárvore esquerda e subárvore direita.","O(n)"),
                new("PostOrder","public IEnumerable<T> PostOrder()","Visita as duas subárvores antes da raiz.","O(n)"),
            ]),
        new("heap","nonlinear","DSHeap","Heap mínimo","Árvore implícita em vetor onde o menor valor permanece na raiz.","Filas de prioridade e algoritmos que precisam do menor elemento repetidamente.",@"namespace DataStructure.Abstractions;

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
            ? throw new InvalidOperationException(""The heap is empty."")
            : _items[0];

    public T Remove()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException(""The heap is empty."");

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
}",
            [
                new("Add","public void Add(T item)","Adiciona um valor ao final da estrutura.","O(1) amortizado"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
                new("AsArray","public IReadOnlyList<T> AsArray()","Expõe a representação em vetor usada pelo heap.","O(1)"),
            ]),
        new("maxheap","nonlinear","DSMaxHeap","Max Heap","Heap binário máximo que mantém o maior elemento na raiz.","Quando é necessário obter repetidamente o maior valor com acesso à raiz em O(1), como em filas de prioridade máximas e seleção dos maiores elementos.",@"namespace DataStructure.Abstractions;

public sealed class DSMaxHeap<T> where T : IComparable<T>
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
            ? throw new InvalidOperationException(""The heap is empty."")
            : _items[0];

    public T Remove()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException(""The heap is empty."");

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
}",
            [
                new("Add","public void Add(T value)","Insere o valor no final da representação e sobe enquanto ele for maior que o pai.","O(log n)"),
                new("Peek","public T Peek()","Consulta o maior valor, armazenado na raiz.","O(1)"),
                new("Remove","public T Remove()","Remove a raiz, promove o último elemento e restaura a propriedade de max-heap.","O(log n)"),
                new("AsArray","public IReadOnlyList<T> AsArray()","Expõe a representação em níveis usada para visualizar o heap.","O(1)"),
            ]),
        new("graph","nonlinear","DSGraph","Grafo direcionado","Vértices conectados por arestas orientadas, representados por listas de adjacência.","Modelar relações, dependências, redes e caminhos.",@"namespace DataStructure.Abstractions;

public sealed class DSGraph<T> where T : notnull
{
    private readonly Dictionary<T, HashSet<T>> _adjacency = [];

    public IReadOnlyCollection<T> Vertices => _adjacency.Keys;

    public void AddVertex(T vertex)
        => _adjacency.TryAdd(vertex, []);

    public void AddEdge(T from, T to)
    {
        AddVertex(from);
        AddVertex(to);
        _adjacency[from].Add(to);
    }

    public bool HasEdge(T from, T to)
        => _adjacency.TryGetValue(from, out var neighbors)
            && neighbors.Contains(to);

    public IReadOnlyCollection<T> Neighbors(T vertex)
        => _adjacency.TryGetValue(vertex, out var neighbors)
            ? neighbors
            : [];

    public IEnumerable<T> BreadthFirst(T start)
    {
        if (!_adjacency.ContainsKey(start))
            yield break;

        var visited = new HashSet<T> { start };
        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            yield return vertex;

            foreach (var neighbor in _adjacency[vertex])
            {
                if (visited.Add(neighbor))
                    queue.Enqueue(neighbor);
            }
        }
    }

    public IEnumerable<T> DepthFirst(T start)
    {
        if (!_adjacency.ContainsKey(start))
            yield break;

        var visited = new HashSet<T>();

        foreach (var vertex in DepthFirstCore(start, visited))
            yield return vertex;
    }

    private IEnumerable<T> DepthFirstCore(T vertex, HashSet<T> visited)
    {
        if (!visited.Add(vertex))
            yield break;

        yield return vertex;

        foreach (var neighbor in _adjacency[vertex])
        {
            foreach (var next in DepthFirstCore(neighbor, visited))
                yield return next;
        }
    }
}",
            [
                new("AddVertex","public void AddVertex(T vertex)","Cria um vértice sem arestas caso ele ainda não exista.","O(1) amortizado"),
                new("AddEdge","public void AddEdge(T from, T to)","Garante os vértices e registra uma aresta direcionada.","O(1) amortizado"),
                new("HasEdge","public bool HasEdge(T from, T to)","Consulta se a lista de adjacência possui a conexão indicada.","O(1) médio"),
                new("Neighbors","public IReadOnlyCollection<T> Neighbors(T vertex)","Retorna os vizinhos diretamente conectados ao vértice.","O(1) para obter a coleção"),
                new("BreadthFirst","public IEnumerable<T> BreadthFirst(T start)","Usa uma fila e visita primeiro os vértices mais próximos da origem.","O(V + E)"),
                new("DepthFirst","public IEnumerable<T> DepthFirst(T start)","Explora cada caminho até o limite antes de voltar para outro ramo.","O(V + E)"),
            ]),
    ];

    public static IReadOnlyList<StructureGuide> Linear =>
        All.Where(item => item.Category == "linear").ToArray();

    public static IReadOnlyList<StructureGuide> NonLinear =>
        All.Where(item => item.Category == "nonlinear").ToArray();

    public static StructureGuide Get(string key) =>
        All.FirstOrDefault(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase)) ?? All[0];

    public static string Normalize(string? key) => Get(key ?? All[0].Key).Key;
}
