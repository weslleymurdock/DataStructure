namespace DataStructure.Abstractions;
public sealed class DSNodeList<T> where T : notnull
{
    private DSNode<T>? head; // O primeiro item da lista
    private DSNode<T>? tail; // O último item (otimização para o Add)

    public int Count { get; private set; }

    // Adiciona ao final sem recriar nenhum array
    public void Add(T item)
    {
        var newNode = new DSNode<T>(item);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail!.Next = newNode;
            tail = newNode;
        }
        Count++;
    }

    // Acesso por índice exige percorrer a lista (Não é O(1) como no array)
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            var current = head;
            for (int i = 0; i < index; i++)
            {
                current = current!.Next;
            }
            return current!.Value;
        }
    }

    // Remove apenas religando os ponteiros
    public bool Remove(T item)
    {
        if (head == null) return false;

        if (head.Value.Equals(item))
        {
            head = head.Next;
            if (head == null) tail = null; // A lista esvaziou
            Count--;
            return true;
        }

        var current = head;
        while (current.Next != null)
        {
            if (current.Next.Value.Equals(item))
            {
                // Pula o item removido, ligando o atual direto ao "próximo do próximo"
                current.Next = current.Next.Next;
                
                // Se removemos o último, precisamos atualizar o tail
                if (current.Next == null) tail = current;
                
                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }
}