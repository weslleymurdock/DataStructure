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


## Guia de algoritmos

Esta seção relaciona cada algoritmo ao problema que resolve, às estruturas sobre as quais opera e ao seu custo assintótico. Big-O descreve como o custo cresce conforme `n` aumenta; não representa diretamente tempo em segundos.

### Comparação

| Algoritmo | Problema | Pré-condição | Melhor | Médio | Pior | Espaço | Melhor use case |
|---|---|---|---:|---:|---:|---:|---|
| `LinearSearch` | encontrar elemento | nenhuma | O(1) | O(n) | O(n) | O(1) | dados pequenos ou não ordenados |
| `BinarySearch` | encontrar elemento | dados ordenados e acesso eficiente ao meio | O(1) | O(log n) | O(log n) | O(1) | muitas buscas em dados ordenados e indexáveis |
| `BubbleSort` | ordenar | nenhuma | O(n)* | O(n²) | O(n²) | O(1) | conjuntos pequenos/quase ordenados e estudo didático |
| `SelectionSort` | ordenar | nenhuma | O(n²) | O(n²) | O(n²) | O(1) | conjuntos pequenos e estudo de seleção/trocas |

`*` O `BubbleSort` possui early exit e pode terminar em O(n) quando uma passagem não produz trocas.

### Como implementar

#### LinearSearch — O(n)

Percorra os elementos do primeiro ao último, compare cada valor com o alvo e retorne imediatamente ao encontrar uma correspondência. Se o fim for alcançado, o elemento não foi encontrado. Não exige dados ordenados.

**Melhor use case:** uma busca isolada em dados pequenos ou não ordenados, quando preparar ou ordenar os dados custaria mais do que percorrê-los.

#### BinarySearch — O(log n)

Exige uma sequência ordenada. Mantenha os limites `low` e `high`, examine `mid`, compare com o alvo e descarte metade do intervalo a cada iteração.

**Melhor use case:** buscas repetidas em dados já ordenados e com acesso indexado O(1), como arrays e listas indexáveis.

**Atenção:** em uma lista ligada, acessar `data[mid]` pode custar O(n), portanto a complexidade efetiva não é a mesma de uma estrutura com acesso aleatório O(1).

#### BubbleSort — O(n²)

Compare elementos adjacentes e troque-os quando estiverem fora de ordem. Ao final de cada passagem, o maior elemento restante chega ao final da região não ordenada. Reduza essa região e encerre quando nenhuma troca ocorrer.

**Melhor use case:** aprendizado, demonstrações e conjuntos muito pequenos ou quase ordenados. Para grandes volumes, prefira algoritmos de ordenação O(n log n).

#### SelectionSort — O(n²)

Para cada posição da região não ordenada, procure o menor elemento restante e troque-o com o elemento daquela posição. O número de comparações continua O(n²), inclusive quando os dados já estão ordenados.

**Melhor use case:** estudo da técnica de seleção e cenários pequenos em que reduzir a quantidade de trocas seja relevante.

### Comparação por cenário

| Cenário | Algoritmo | Motivo |
|---|---|---|
| Uma busca em dados não ordenados | `LinearSearch` | não exige preparação |
| Muitas buscas em dados ordenados e indexáveis | `BinarySearch` | elimina aproximadamente metade dos candidatos por iteração |
| Dados pequenos e quase ordenados | `BubbleSort` | early exit pode reduzir o trabalho para O(n) |
| Aprender comparações e trocas adjacentes | `BubbleSort` | implementação simples e visual |
| Aprender seleção do menor elemento | `SelectionSort` | separa busca do mínimo e a troca |
| Reduzir trocas em algoritmo introdutório | `SelectionSort` | no máximo uma troca por posição |
| Grandes volumes em produção | algoritmos O(n log n) | os algoritmos de ordenação deste projeto são introdutórios |

### Algoritmo × estrutura de dados

- `DSArray<T>`: acesso por índice O(1), adequado para `BinarySearch`.
- `DSList<T>`: acesso por índice O(1), também adequado para `BinarySearch`.
- `DSLinkedList<T>`: acesso por índice O(n); uma busca binária baseada em índices perde a vantagem prática do acesso aleatório.
- `DSQueue<T>`: FIFO; adequada quando a ordem de chegada deve ser preservada.
- `DSStack<T>`: LIFO; adequada para processamento reverso, parsing e exploração em profundidade.
- `DSDeque<T>`: permite operações nas duas extremidades.
- `DSPriorityQueue<T>`: determina o próximo elemento pela prioridade, adequada para escalonamento e processamento prioritário.

Assim, a complexidade do algoritmo e a complexidade das operações da estrutura devem ser consideradas juntas.

### Complexidade em termos simples

- **O(1)** — custo constante.
- **O(log n)** — custo logarítmico; o espaço de busca é reduzido sucessivamente.
- **O(n)** — custo linear; normalmente é necessário visitar os elementos.
- **O(n log n)** — comum em algoritmos eficientes de ordenação e divisão/conquista.
- **O(n²)** — custo quadrático; cresce rapidamente quando `n` aumenta.
## Executando

```bash
dotnet run --project src/DataStructure/DataStructure.csproj
dotnet run --project src/DataStructure.AlgorithmsConsole/DataStructure.AlgorithmsConsole.csproj
dotnet test
```

O primeiro console demonstra as estruturas. O segundo instancia cada classe de algoritmo e chama os métodos `Execute<TEstruturaDeDado>`, exibindo o tempo medido por estrutura.

## Objetivo didático

A intenção não é substituir `Array`, `List<T>`, `Queue<T>`, `LinkedList<T>`, `Stack<T>` ou `PriorityQueue<TElement,TPriority>` do .NET em aplicações reais. O objetivo é tornar explícitos os mecanismos que essas estruturas e algoritmos utilizam e relacioná-los às suas complexidades.
