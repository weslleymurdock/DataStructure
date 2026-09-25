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

### Complexidade

- **O(1)**: tempo constante; a quantidade de trabalho não cresce com `n`.
- **O(log n)**: crescimento logarítmico; comum em árvores balanceadas e heaps.
- **O(n)**: crescimento linear; em geral exige visitar os elementos.
- **O(n log n)**: comum em algoritmos de ordenação eficientes.
- **O(n²)**: crescimento quadrático; dois percursos dependentes de `n`, típico de ordenações introdutórias.

## Algoritmos

O projeto de algoritmos mantém cada algoritmo em uma classe cujo nome identifica a técnica:

- `LinearSearch` — busca sequencial, O(n).
- `BubbleSort` — ordenação por trocas adjacentes, O(n²) médio/pior caso.
- `SelectionSort` — seleção do menor elemento restante, O(n²).

Cada classe expõe métodos no padrão:

`ExecuteDSArray`, `ExecuteDSList`, `ExecuteDSLinkedList`, `ExecuteDSQueue`, `ExecuteDSStack` e `ExecuteDSDeque`.

O método mede o tempo de execução com `Stopwatch`. As estruturas destrutivas (fila, pilha e deque) são restauradas ao estado lógico original depois da operação quando necessário.

> A medição é didática, não um benchmark científico. JIT, GC, CPU, tamanho dos dados e estado do processo influenciam os valores. Para comparar algoritmos, observe principalmente a ordem de complexidade e use entradas equivalentes.

## Executando

```bash
dotnet run --project src/DataStructure/DataStructure.csproj
dotnet run --project src/DataStructure.AlgorithmsConsole/DataStructure.AlgorithmsConsole.csproj
dotnet test
```

O primeiro console demonstra as estruturas. O segundo cria as estruturas, instancia os algoritmos e chama os métodos `Execute<TEstruturaDeDado>`, exibindo o tempo medido para cada combinação.

## Objetivo didático

A intenção não é substituir `Array`, `List<T>`, `Queue<T>`, `LinkedList<T>`, `Stack<T>` ou `PriorityQueue<TElement,TPriority>` do .NET em aplicações reais. O objetivo é tornar explícitos os mecanismos que essas estruturas e algoritmos utilizam e relacioná-los às suas complexidades.
