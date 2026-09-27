namespace DataStructure.github.io.Services;

public sealed record AlgorithmGuide(
    string Key,
    string Category,
    string Name,
    string Summary,
    string Complexity,
    string Preconditions,
    string StructureScope,
    IReadOnlyList<string> Steps,
    string Mermaid);

public static class AlgorithmCatalog
{
    public static IReadOnlyList<AlgorithmGuide> All { get; } =
    [
        new("bubble-sort","sort","BubbleSort","Compara vizinhos e leva os maiores valores para o fim da região ainda não ordenada.","Melhor O(n); médio/pior O(n²)","Nenhuma.","Sequências lineares e representações lineares de árvores; grafos são linearizados apenas para demonstração.",
            ["Percorra a região não ordenada.","Compare dois elementos adjacentes.","Troque-os se estiverem invertidos.","Repita os passes até que nenhum elemento seja trocado."],
            @"flowchart TD
    A[""end = n - 1""] --> B[""changed = false""]
    B --> C[""index = 0""]
    C --> D{""index < end?""}
    D -- ""yes"" --> E{""data[index] > data[index + 1]?""}
    E -- ""yes"" --> F[""swap adjacent values""]
    F --> G[""changed = true""]
    G --> H[""index++""]
    E -- ""no"" --> H
    H --> D
    D -- ""no"" --> I{""changed?""}
    I -- ""yes"" --> J[""end--""]
    J --> B
    I -- ""no"" --> K[""sorted / stop""]"),
        new("selection-sort","sort","SelectionSort","Procura o menor elemento da região restante e o coloca na próxima posição.","O(n²)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Escolha a primeira posição ainda não ordenada.","Procure o menor valor no restante.","Troque o menor com a posição escolhida.","Avance a fronteira da região ordenada."],
            @"flowchart TD
    A[""position = 0""] --> B{""position < n - 1?""}
    B -- ""yes"" --> C[""minimum = position""]
    C --> D[""scan index = position + 1""]
    D --> E{""index < n?""}
    E -- ""yes"" --> F{""data[index] < data[minimum]?""}
    F -- ""yes"" --> G[""minimum = index""]
    G --> H[""index++""]
    F -- ""no"" --> H
    H --> E
    E -- ""no"" --> I{""minimum != position?""}
    I -- ""yes"" --> J[""swap position and minimum""]
    I -- ""no"" --> K[""position++""]
    J --> K
    K --> B
    B -- ""no"" --> L[""sorted""]"),
        new("insertion-sort","sort","InsertionSort","Constrói uma região ordenada inserindo cada novo valor no ponto correto.","Melhor O(n); médio/pior O(n²)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Considere o primeiro elemento ordenado.","Escolha o próximo valor.","Desloque valores maiores para a direita.","Insira o valor no espaço criado.","Repita até o final."],
            @"flowchart TD
    A[""index = 1""] --> B{""index < n?""}
    B -- ""yes"" --> C[""value = data[index]""]
    C --> D[""position = index - 1""]
    D --> E{""position >= 0 && data[position] > value?""}
    E -- ""yes"" --> F[""data[position + 1] = data[position]""]
    F --> G[""position--""]
    G --> E
    E -- ""no"" --> H[""data[position + 1] = value""]
    H --> I[""index++""]
    I --> B
    B -- ""no"" --> J[""sorted""]"),
        new("merge-sort","sort","MergeSort","Divide a sequência em partes menores e depois intercala as partes já ordenadas.","O(n log n)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Divida a sequência ao meio.","Ordene recursivamente cada metade.","Compare as duas primeiras posições disponíveis.","Copie a menor para o resultado.","Continue até as duas metades serem consumidas."],
            @"flowchart TD
    A[""Merge(values, low, high)""] --> B{""low >= high?""}
    B -- ""yes"" --> C[""return""]
    B -- ""no"" --> D[""middle = low + (high-low)/2""]
    D --> E[""Merge(left half)""]
    E --> F[""Merge(right half)""]
    F --> G[""left = low; right = middle + 1""]
    G --> H{""both halves have values?""}
    H -- ""yes"" --> I{""left value <= right value?""}
    I -- ""yes"" --> J[""copy left; left++""]
    I -- ""no"" --> K[""copy right; right++""]
    J --> H
    K --> H
    H -- ""no"" --> L[""copy remaining left""]
    L --> M[""copy remaining right""]
    M --> N[""Array.Copy temporary range back""]
    N --> O[""return""]"),
        new("quick-sort","sort","QuickSort","Escolhe um pivô, particiona os valores ao redor dele e repete nos subintervalos.","Médio O(n log n); pior O(n²)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Escolha um pivô.","Percorra os valores da região.","Mova os menores ou iguais para antes do pivô.","Posicione o pivô.","Aplique o processo às duas regiões resultantes."],
            @"flowchart TD
    A[""Quick(values, low, high)""] --> B{""low >= high?""}
    B -- ""yes"" --> C[""return""]
    B -- ""no"" --> D[""pivot = values[high]""]
    D --> E[""smaller = low""]
    E --> F[""scan index from low to high-1""]
    F --> G{""values[index] <= pivot?""}
    G -- ""yes"" --> H[""swap values[smaller], values[index]""]
    H --> I[""smaller++""]
    I --> F
    G -- ""no"" --> F
    F --> J[""swap pivot into values[smaller]""]
    J --> K[""Quick(left partition)""]
    K --> L[""Quick(right partition)""]
    L --> M[""return""]"),
        new("heap-sort","sort","HeapSort","Organiza os valores com a propriedade de heap e extrai repetidamente o maior elemento.","O(n log n)","Nenhuma.","Sequências lineares; heaps podem ser demonstrados diretamente por sua representação em vetor.",
            ["Construa um max-heap.","Troque a raiz com o último elemento da região.","Reduza a região do heap.","Restaure a propriedade do heap.","Repita até restar um elemento."],
            @"flowchart TD
    A[""Heap(values)""] --> B[""for each internal node bottom-up""]
    B --> C[""SiftDown(root, heapLength)""]
    C --> D[""max-heap established""]
    D --> E[""end = n - 1""]
    E --> F{""end > 0?""}
    F -- ""yes"" --> G[""swap values[0] with values[end]""]
    G --> H[""SiftDown(root, end)""]
    H --> I[""end--""]
    I --> F
    F -- ""no"" --> J[""ascending order""]"),
        new("linear-search","search","LinearSearch","Visita os elementos na ordem de travessia até encontrar o alvo.","O(n)","Nenhuma.","Todas as estruturas: usa índice, enumeração ou travessia apropriada.",
            ["Comece no primeiro elemento da sequência ou travessia.","Compare o valor atual com o alvo.","Retorne ao encontrar igualdade.","Caso contrário, avance para o próximo elemento.","Termine sem resultado quando a sequência acabar."],
            @"flowchart TD
    A[""select structure traversal""] --> B[""index = 0""]
    B --> C{""next element exists?""}
    C -- ""yes"" --> D{""element == target?""}
    D -- ""yes"" --> E[""return index""]
    D -- ""no"" --> F[""index++""]
    F --> C
    C -- ""no"" --> G[""return -1""]
    H[""Queue / Deque""] -. ""rotate while inspecting"" .-> A
    I[""Stack""] -. ""pop to buffer, then restore"" .-> A
    J[""PriorityQueue""] -. ""dequeue all, then re-enqueue"" .-> A"),
        new("binary-search","search","BinarySearch","Divide repetidamente um intervalo ordenado ao meio para eliminar metade das possibilidades.","O(log n) com acesso indexado","Dados ordenados; acesso indexado eficiente.","Arrays e listas indexadas. Em árvore de busca, a operação equivalente é a busca pela propriedade da BST.",
            ["Defina início e fim do intervalo.","Calcule o meio.","Compare o meio com o alvo.","Elimine a metade impossível.","Repita até encontrar ou esvaziar o intervalo."],
            @"flowchart TD
    A[""low = 0; high = n - 1""] --> B{""low <= high?""}
    B -- ""yes"" --> C[""middle = low + (high-low)/2""]
    C --> D[""value = data[middle]""]
    D --> E{""value == target?""}
    E -- ""yes"" --> F[""return middle""]
    E -- ""no"" --> G{""value < target?""}
    G -- ""yes"" --> H[""low = middle + 1""]
    G -- ""no"" --> I[""high = middle - 1""]
    H --> B
    I --> B
    B -- ""no"" --> J[""return -1""]
    K[""BST overload""] --> L[""compare node and choose Left / Right""]"),
        new("jump-search","search","JumpSearch","Avança em blocos e realiza uma busca linear somente no bloco candidato.","O(√n)","Dados ordenados e acesso indexado.","Arrays e listas indexadas.",
            ["Escolha um tamanho de salto próximo de √n.","Salte enquanto o último valor do bloco for menor que o alvo.","Identifique o bloco candidato.","Percorra esse bloco linearmente.","Pare ao encontrar ou ultrapassar o alvo."],
            @"flowchart TD
    A[""step = max(1, floor(sqrt(n)))""] --> B[""previous = 0; next = step""]
    B --> C{""block last value < target?""}
    C -- ""yes"" --> D[""previous = next; next += step""]
    D --> E{""previous >= n?""}
    E -- ""yes"" --> F[""return -1""]
    E -- ""no"" --> C
    C -- ""no"" --> G[""scan candidate block""]
    G --> H{""value == target?""}
    H -- ""yes"" --> I[""return index""]
    H -- ""no"" --> J{""value > target?""}
    J -- ""yes"" --> K[""break""]
    J -- ""no"" --> G
    K --> L[""return -1""]
    G --> L"),
        new("interpolation-search","search","InterpolationSearch","Estima a posição do alvo usando a distribuição numérica entre os extremos.","Médio O(log log n); pior O(n)","Dados numéricos ordenados e distribuição relativamente uniforme.","Arrays e listas indexadas numéricas.",
            ["Observe os limites e seus valores.","Estime a posição proporcionalmente ao alvo.","Compare o valor estimado.","Restrinja o intervalo para o lado possível.","Repita até encontrar ou invalidar o intervalo."],
            @"flowchart TD
    A[""low = 0; high = n - 1""] --> B{""target inside endpoint values?""}
    B -- ""no"" --> C[""return -1""]
    B -- ""yes"" --> D{""lowValue == highValue?""}
    D -- ""yes"" --> E{""lowValue == target?""}
    E -- ""yes"" --> F[""return low""]
    E -- ""no"" --> C
    D -- ""no"" --> G[""estimate position from value range""]
    G --> H[""value = data[position]""]
    H --> I{""value == target?""}
    I -- ""yes"" --> J[""return position""]
    I -- ""no"" --> K{""value < target?""}
    K -- ""yes"" --> L[""low = position + 1""]
    K -- ""no"" --> M[""high = position - 1""]
    L --> B
    M --> B"),
    ];

    public static IReadOnlyList<AlgorithmGuide> Sorts =>
        All.Where(item => item.Category == "sort").ToArray();

    public static IReadOnlyList<AlgorithmGuide> Searches =>
        All.Where(item => item.Category == "search").ToArray();

    public static AlgorithmGuide Get(string key) =>
        All.First(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
}
