using System;

namespace DataStructure.Abstractions;

// ==========================================
// 1. QUEUE (FILA) - Comportamento FIFO O(1)
// ==========================================
public sealed class DSQueue<T>
{
    private DSNode<T>? _head; // Onde os elementos saem (Início da fila)
    private DSNode<T>? _tail; // Onde os elementos entram (Fim da fila)

    public int Count { get; private set; }

    // Enqueue: Adiciona sempre no final da fila O(1)
    public void Enqueue(T item)
    {
        var newNode = new DSNode<T>(item);

        if (_tail == null)
        {
            // Fila estava vazia
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // O antigo último aponta para o novo, e o novo vira o último
            _tail.Next = newNode;
            _tail = newNode;
        }
        Count++;
    }

    // Dequeue: Remove sempre do início da fila O(1)
    public T Dequeue()
    {
        if (_head == null)
            throw new InvalidOperationException("A fila está vazia.");

        T value = _head.Value;
        _head = _head.Next; // O segundo da fila passa a ser o primeiro

        if (_head == null)
            _tail = null; // A fila esvaziou totalmente

        Count--;
        return value;
    }

    // Apenas espia quem é o próximo a ser atendido, sem remover
    public T Peek()
    {
        if (_head == null)
            throw new InvalidOperationException("A fila está vazia.");
        
        return _head.Value;
    }
}