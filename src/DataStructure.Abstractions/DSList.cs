using System;

namespace DataStructure.Abstractions;

public sealed class DSList<T>
{
    // O array interno que armazena os dados
    private T[] _items;
    
    // O contador real de elementos inseridos
    private int _count;

    public DSList()
    {
        // Inicializa com um array vazio para poupar memória até o primeiro Add
        _items = [];
        _count = 0;
    }

    public int Count => _count;

    // Acesso e atribuição O(1) reais
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Índice fora dos limites da lista.");
            
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Índice fora dos limites da lista.");
            
            _items[index] = value;
        }
    }

    // Adição com tempo Amortizado O(1)
    public void Add(T item)
    {
        // Se o array estiver cheio, dobramos a capacidade dele
        if (_count == _items.Length)
        {
            EnsureCapacity();
        }

        // Insere no primeiro espaço vazio disponível e incrementa o Count
        _items[_count] = item;
        _count++;
    }

    // Método interno para gerenciar a duplicação dinâmica do array
    private void EnsureCapacity()
    {
        // Começa com capacidade 4 se estiver vazio. Caso contrário, dobra o tamanho atual.
        int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;
        
        T[] newArray = new T[newCapacity];
        
        // Copia os dados do array antigo para o novo.
        // O Array.Copy em C# desce a nível de C++ (memmove) e é extremamente rápido.
        if (_count > 0)
        {
            Array.Copy(_items, newArray, _count);
        }
        
        _items = newArray;
    }

    // Remoção O(n)
    public bool Remove(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        
        for (int i = 0; i < _count; i++)
        {
            // Encontrou o item
            if (comparer.Equals(_items[i], item))
            {
                // Desloca todos os itens à frente dele uma posição para trás
                for (int j = i; j < _count - 1; j++)
                {
                    _items[j] = _items[j + 1];
                }
                
                _count--;
                
                // Limpa a última posição que ficou duplicada.
                // Isso é fundamental se T for uma classe, senão o Garbage Collector 
                // nunca limpará aquele objeto da memória (Memory Leak).
                _items[_count] = default!;
                
                return true;
            }
        }
        
        return false;
    }
}