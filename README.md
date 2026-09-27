# DataStructure

Revisão prática de **estruturas de dados e algoritmos** com C#/.NET 10.

O repositório privilegia implementações pequenas e didáticas, para que a estrutura interna, as operações e a complexidade possam ser estudadas sem depender apenas das coleções prontas do .NET.

## Projetos

- **DataStructure.Abstractions** — implementações das estruturas.
- **DataStructure.Algorithms** — algoritmos e métodos `Execute<TEstruturaDeDado>`.
- **DataStructure** — console único que demonstra as estruturas, mede os algoritmos e executa a visualização ANSI das ordenações.
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
| `DSHeap<T>` | heap binário mínimo | Peek O(1), Add/Remove O(log n) | obter repetidamente o menor valor |
| `DSMaxHeap<T>` | heap binário máximo | Peek O(1), Add/Remove O(log n) | obter repetidamente o maior valor e implementar prioridades máximas |
| `DSHashTable<TKey,TValue>` | buckets com encadeamento | acesso médio O(1), pior caso O(n) | associações chave/valor, caches, índices e tabelas de símbolos |
| `DSNode<T>` | nó compartilhado | armazenamento de valor e referências O(1) | base para estruturas encadeadas |
| `DSNodeList<T>` | lista simplesmente encadeada | Add O(1), acesso por índice O(n) | demonstração simples de encadeamento |
| `DSDeck<T>` | deque legado duplamente encadeado | extremos O(1) | exemplo alternativo de deque |

Cada implementação possui comentários/XML docs explicando a lógica interna, as operações e a complexidade. O guia Blazor também mantém explicações independentes em pt-BR, incluindo utilidade, casos de uso e custos das operações, sem depender dos comentários do código. Os nós são compartilhados pelas estruturas baseadas em encadeamento.

### Complexidade

- **O(1)**: tempo constante; a quantidade de trabalho não cresce com `n`.
- **O(log n)**: crescimento logarítmico; comum em árvores balanceadas e heaps.
- **O(n)**: crescimento linear; em geral exige visitar os elementos.
- **O(n log n)**: comum em algoritmos de ordenação eficientes.
- **O(n²)**: crescimento quadrático; dois percursos dependentes de `n`, típico de ordenações introdutórias.

## Algoritmos

Cada algoritmo fica em uma classe com nome correspondente à técnica. Os métodos seguem o padrão `Execute<TEstruturaDeDado>` e o console executa cada implementação sobre estruturas equivalentes, medindo o tempo com `Stopwatch`. As classes de ordenação também aceitam opcionalmente um callback `Action<IReadOnlyList<int>>`, usado pelo demo visual sem acoplar os algoritmos ao terminal.

### Busca

| Algoritmo | Pré-condição | Melhor | Médio | Pior | Melhor use case |
|---|---|---:|---:|---:|---|
| `LinearSearch` | nenhuma | O(1) | O(n) | O(n) | busca isolada em dados pequenos ou não ordenados |
| `BinarySearch` | dados ordenados e acesso indexado eficiente | O(1) | O(log n) | O(log n) | muitas buscas em dados ordenados e indexáveis |
| `JumpSearch` | dados ordenados e acesso indexado | O(1) | O(sqrt(n)) | O(sqrt(n)) | busca ordenada por blocos |
| `InterpolationSearch` | dados ordenados, numéricos e aproximadamente uniformes | O(1) | O(log log n) | O(n) | grandes conjuntos numéricos uniformes |

### Ordenação

| Algoritmo | Melhor | Médio | Pior | Espaço | Melhor use case |
|---|---:|---:|---:|---:|---|
| `BubbleSort` | O(n) | O(n²) | O(n²) | O(1) | aprendizado e conjuntos muito pequenos/quase ordenados |
| `SelectionSort` | O(n²) | O(n²) | O(n²) | O(1) | aprendizado e cenários pequenos com poucas trocas |
| `InsertionSort` | O(n) | O(n²) | O(n²) | O(1) | dados pequenos ou quase ordenados |
| `MergeSort` | O(n log n) | O(n log n) | O(n log n) | O(n) | desempenho previsível |
| `QuickSort` | O(n log n) | O(n log n) | O(n²) | O(log n)* | ordenação geral quando o caso médio é prioritário |
| `HeapSort` | O(n log n) | O(n log n) | O(n log n) | O(1) | garantir O(n log n) sem memória auxiliar proporcional a n |

`*` O `QuickSort` usa o último elemento como pivô; a profundidade média é O(log n), mas pode chegar a O(n) no pior caso.

### Como implementar os algoritmos de busca

#### LinearSearch — O(n)

Percorra os elementos sequencialmente, compare cada valor com o alvo e retorne ao encontrar uma correspondência. Não exige ordenação.

**Use quando:** a coleção não está ordenada, a busca é pontual ou preparar os dados teria custo desnecessário.

#### BinarySearch — O(log n)

Mantenha os limites do intervalo, examine o elemento central e descarte metade dos candidatos a cada iteração. Requer dados ordenados.

**Use quando:** haverá muitas buscas sobre dados ordenados com acesso ao índice em O(1).

**Atenção:** em `DSLinkedList`, acessar um índice custa O(n), portanto a implementação baseada em índices não possui o mesmo custo efetivo de arrays e listas indexadas.

#### JumpSearch — O(sqrt(n))

Divida a sequência ordenada em blocos de aproximadamente `sqrt(n)` elementos. Avance bloco a bloco até encontrar o intervalo que pode conter o alvo e faça uma busca linear dentro dele.

**Use quando:** os dados estão ordenados e deseja-se combinar saltos com uma busca local.

#### InterpolationSearch — O(log log n) médio, O(n) pior caso

Estime a posição provável do alvo usando os valores mínimo e máximo, em vez de sempre escolher o meio. Funciona melhor quando os valores numéricos estão aproximadamente distribuídos de maneira uniforme.

**Use quando:** os dados são numéricos, ordenados, grandes e aproximadamente uniformes.

**Atenção:** dados concentrados ou irregulares podem degradar o algoritmo para O(n).

### Como implementar os algoritmos de ordenação

#### BubbleSort — O(n²)

Compare elementos adjacentes e troque-os quando estiverem fora de ordem. Após cada passagem, o maior elemento restante fica no final da região não ordenada. O early exit permite O(n) quando nenhuma troca é necessária.

**Use quando:** o objetivo é estudar ordenação por trocas ou os dados são muito pequenos/quase ordenados.

#### SelectionSort — O(n²)

Para cada posição, encontre o menor elemento na região restante e faça uma troca. O número de comparações permanece O(n²), mas a quantidade de trocas é limitada.

**Use quando:** a prioridade didática é entender seleção do mínimo ou reduzir trocas.

#### InsertionSort — O(n²)

Considere a primeira parte como ordenada. Retire o próximo elemento e mova os elementos maiores uma posição para a direita até encontrar sua posição correta.

**Use quando:** os dados são pequenos ou chegam quase ordenados. O melhor caso é O(n).

#### MergeSort — O(n log n)

Divida a sequência aproximadamente ao meio até obter partes unitárias. Ordene as partes recursivamente e faça o merge das partes já ordenadas.

**Use quando:** é desejado desempenho O(n log n) previsível.

**Trade-off:** necessita memória auxiliar O(n).

#### QuickSort — O(n log n) médio

Escolha um pivô, particione os elementos entre valores menores/iguais e maiores e aplique o mesmo processo recursivamente às duas partes.

**Use quando:** deseja-se uma ordenação geral eficiente e o caso médio O(n log n) é adequado.

**Trade-off:** a escolha de pivô influencia o resultado. Nesta implementação, uma entrada já ordenada pode produzir O(n²).

#### HeapSort — O(n log n)

Construa um max-heap, troque a raiz com o último elemento da região não ordenada e restaure a propriedade do heap. Repita até ordenar toda a sequência.

**Use quando:** é importante garantir O(n log n) no melhor, médio e pior caso sem memória auxiliar proporcional a n.

### Comparação por cenário

| Cenário | Algoritmo(s) | Motivo |
|---|---|---|
| Busca única em dados não ordenados | `LinearSearch` | não exige preparação |
| Muitas buscas em dados ordenados e indexáveis | `BinarySearch` | reduz o espaço de busca pela metade |
| Busca ordenada por blocos | `JumpSearch` | combina saltos com busca linear local |
| Dados numéricos aproximadamente uniformes | `InterpolationSearch` | estima a posição provável |
| Dados pequenos/quase ordenados | `InsertionSort` | aproveita a ordenação existente |
| Estudo de trocas adjacentes | `BubbleSort` | simples e visual |
| Estudo de seleção e redução de trocas | `SelectionSort` | poucas trocas, embora O(n²) |
| Ordenação com O(n log n) previsível | `MergeSort` ou `HeapSort` | não dependem de um caso médio |
| Ordenação geral com bom caso médio | `QuickSort` | O(n log n) médio |
| Ordenação sem memória auxiliar proporcional a n | `HeapSort` | espaço auxiliar O(1) |

### Algoritmo × estrutura de dados

- `DSArray<T>` e `DSList<T>`: acesso por índice O(1), adequados para buscas indexadas e ordenações in-place.
- `DSCollection<T>`: acesso por índice O(1), adequado para algoritmos indexados.
- `DSLinkedList<T>`: acesso por índice O(n); buscas por índice perdem parte da vantagem teórica.
- `DSQueue<T>`, `DSStack<T>` e `DSDeque<T>`: as demonstrações materializam os valores, executam a ordenação e restauram a estrutura.
- `DSPriorityQueue<T>`: é orientada à prioridade, não a uma sequência indexável; por isso não é usada nas novas buscas/ordenações indexadas.

Assim, a complexidade do algoritmo deve ser analisada junto com o custo das operações da estrutura.

### Laboratório visual

O laboratório permite dois modos de comparação:

- **Comparar tudo** por algoritmo: um algoritmo é executado sobre todas as estruturas disponíveis.
- **Comparar tudo** por estrutura: todos os algoritmos são executados sobre uma única estrutura.
- **Comparação personalizada**: cada gráfico é um par explícito algoritmo × estrutura. Depois do primeiro gráfico, os próximos precisam compartilhar o mesmo algoritmo ou a mesma estrutura.

Isso evita comparar, por exemplo, um algoritmo A × estrutura X com algoritmo B × estrutura Y e tratar essa combinação como uma única comparação.

Os painéis são instâncias independentes e exibem a evolução dos valores, índice atualmente processado, número de passos e ordem de conclusão. A ordem de conclusão representa o tempo da animação, não um benchmark científico.

Estruturas não sequenciais são linearizadas apenas para a visualização. Por exemplo, a Hash Table é materializada na ordem de seus buckets e o heap é exibido na sua representação em níveis. Isso permite visualizar algoritmos sobre os dados sem afirmar que essas estruturas possuem operações nativas de ordenação equivalentes a um array.

A DSHashTable<TKey,TValue> possui uma demonstração própria na página de estudo e pode ser selecionada no laboratório. Nessa visualização, suas associações são convertidas em uma sequência independente apenas para a animação dos algoritmos.

### Complexidade em termos simples

- **O(1)** — custo constante.
- **O(log n)** — reduz o espaço de busca em fatores sucessivos.
- **O(log log n)** — cresce ainda mais lentamente que O(log n), mas depende de uma distribuição favorável.
- **O(sqrt(n))** — cresce com a raiz quadrada de n.
- **O(n)** — custo linear; normalmente visita os elementos.
- **O(n log n)** — classe comum de ordenações eficientes.
- **O(n²)** — custo quadrático; cresce rapidamente com n.


## Executando

```bash
dotnet run --project src/DataStructure/DataStructure.csproj
dotnet test
```

O console `DataStructure` demonstra as estruturas, instancia cada classe de algoritmo e chama os métodos `Execute<TEstruturaDeDado>`, exibindo o tempo medido por estrutura. Ao final, `AlgorithmVisualizationDemo` executa os sorts sobre uma permutação de 1..N e desenha cada mutação em um gráfico ANSI N×N.

## Objetivo didático

A intenção não é substituir `Array`, `List<T>`, `Queue<T>`, `LinkedList<T>`, `Stack<T>` ou `PriorityQueue<TElement,TPriority>` do .NET em aplicações reais. O objetivo é tornar explícitos os mecanismos que essas estruturas e algoritmos utilizam e relacioná-los às suas complexidades.


### Hash Table

DSHashTable<TKey, TValue> usa um vetor de buckets e resolve colisões por encadeamento. A posição inicial é calculada a partir de GetHashCode() da chave. Quando a chave já existe, Set atualiza o valor; Add rejeita duplicatas.

A tabela mantém fator de carga máximo de 0,75. Ao ultrapassá-lo, os buckets são ampliados e todas as entradas são redistribuídas de acordo com a nova capacidade.

Operações principais:

- Add — insere uma nova associação.
- Set — insere ou atualiza.
- TryGetValue — consulta sem lançar exceção.
- ContainsKey — verifica a existência da chave.
- Remove — remove uma associação.
- Clear — recria os buckets vazios.
- Enumerate — percorre as associações em ordem de bucket.

A página Estudar da Hash Table apresenta a representação dos buckets, o tratamento de colisões, o redimensionamento e o fluxo individual de cada operação. O laboratório também materializa as associações em uma sequência independente para permitir a visualização dos algoritmos sobre a estrutura, deixando explícito que essa visualização é uma linearização didática e não uma propriedade nativa da Hash Table.
