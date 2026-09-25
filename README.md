# DataStructure

Revisão prática de **estruturas de dados e algoritmos** com C#/.NET 10.

O repositório privilegia implementações pequenas e didáticas, para que a estrutura interna, as operações e a complexidade possam ser estudadas sem depender apenas das coleções prontas do .NET.

## Projetos

- **DataStructure.Abstractions** — implementações das estruturas.
- **DataStructure** — console com exemplos de uso das estruturas.
- **DataStructure.Algorithms** — algoritmos e métodos `Execute<TEstruturaDeDado>`.
- **DataStructure.AlgorithmsConsole** — segundo console que executa os algoritmos e mede o tempo.
- **DataStructure.Tests** — testes das operações fundamentais.

## Estruturas

| Estrutura | Organização | Operações características | Melhor caso de uso |
|---|---|---|---|
| `DSArray<T>` | memória contígua, tamanho fixo | acesso por índice O(1) | dados de tamanho conhecido e acesso aleatório intenso |
| `DSCollection<T>` | array dinâmico | Add amortizado O(1), acesso O(1), busca O(n) | coleção genérica que cresce dinamicamente |
| `DSList<T>` | array dinâmico | índice O(1), Add amortizado O(1), Insert/Remove O(n) | listas com muita leitura por índice |
| `DSLinkedList<T>` | nós duplamente ligados | extremidades O(1), índice O(n) | inserções/remoções frequentes nas extremidades |
| `DSStack<T>` | LIFO | Push/Pop/Peek O(1) | desfazer ações, parsing e DFS |
| `DSQueue<T>` | FIFO | Enqueue/Dequeue/Peek O(1) | processamento por ordem de chegada e BFS |
| `DSDeque<T>` | duas extremidades | Add/Remove em ambos os lados O(1) | buffers, histórico e filas com prioridade nas extremidades |
| `DSPriorityQueue<T>` | heap binário mínimo | Peek O(1), Enqueue/Dequeue O(log n) | tarefas que precisam ser processadas por prioridade |

Cada implementação possui comentários/XML docs sobre sua organização e complexidade. Os nós são compartilhados pelas estruturas baseadas em encadeamento.

### Complexidade

- **O(1)**: tempo constante; a quantidade de trabalho não cresce com `n`.
- **O(log n)**: crescimento logarítmico; comum em árvores balanceadas e heaps.
- **O(n)**: crescimento linear; em geral exige visitar os elementos.
- **O(n log n)**: comum em algoritmos de ordenação eficientes.
- **O(n²)**: crescimento quadrático; dois percursos dependentes de `n`, típico de ordenações introdutórias.

## Algoritmos

Cada algoritmo fica em uma classe com nome correspondente à técnica:

- `LinearSearch` — busca sequencial, O(n).
- `BinarySearch` — busca binária em dados ordenados; O(log n) quando o acesso indexado é O(1).
- `BubbleSort` — ordenação por trocas adjacentes, O(n²) médio/pior caso e O(n) no melhor caso com saída antecipada.
- `SelectionSort` — seleção do menor elemento restante, O(n²) em todos os casos.

Os métodos seguem o padrão `Execute<TEstruturaDeDado>`, por exemplo `ExecuteDSArray`, `ExecuteDSList`, `ExecuteDSLinkedList`, `ExecuteDSCollection`, `ExecuteDSQueue`, `ExecuteDSStack` e `ExecuteDSDeque`. `LinearSearch` também demonstra `DSPriorityQueue`; `BinarySearch` é demonstrada nas estruturas com acesso indexado.

O tempo é medido com `Stopwatch`. Para estruturas cuja API principal é destrutiva (fila, pilha, deque e fila de prioridade), o exemplo preserva o estado lógico ao terminar.

> A medição é didática, não um benchmark científico. JIT, GC, CPU, tamanho dos dados e estado do processo influenciam os valores. Para comparar algoritmos, observe principalmente a ordem de complexidade e use entradas equivalentes.

## Executando

```bash
dotnet run --project src/DataStructure/DataStructure.csproj
dotnet run --project src/DataStructure.AlgorithmsConsole/DataStructure.AlgorithmsConsole.csproj
dotnet test
```

O primeiro console demonstra as estruturas. O segundo instancia cada classe de algoritmo e chama os métodos `Execute<TEstruturaDeDado>`, exibindo o tempo medido por estrutura.

## Objetivo didático

A intenção não é substituir `Array`, `List<T>`, `Queue<T>`, `LinkedList<T>`, `Stack<T>` ou `PriorityQueue<TElement,TPriority>` do .NET em aplicações reais. O objetivo é tornar explícitos os mecanismos que essas estruturas e algoritmos utilizam e relacioná-los às suas complexidades.
