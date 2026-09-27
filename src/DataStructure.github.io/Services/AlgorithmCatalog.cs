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
            "flowchart TD
A[Início] --> B[Percorrer vizinhos]
B --> C{Esquerda > direita?}
C -- Sim --> D[Trocar]
C -- Não --> E[Avançar]
D --> E
E --> F{Fim do passe?}
F -- Não --> B
F -- Sim --> G{Houve troca?}
G -- Sim --> B
G -- Não --> H[Fim]"),
        new("selection-sort","sort","SelectionSort","Procura o menor elemento da região restante e o coloca na próxima posição.","O(n²)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Escolha a primeira posição ainda não ordenada.","Procure o menor valor no restante.","Troque o menor com a posição escolhida.","Avance a fronteira da região ordenada."],
            "flowchart TD
A[Início] --> B[Escolher posição]
B --> C[Procurar menor valor]
C --> D[Trocar com a posição]
D --> E{Restam elementos?}
E -- Sim --> B
E -- Não --> F[Fim]"),
        new("insertion-sort","sort","InsertionSort","Constrói uma região ordenada inserindo cada novo valor no ponto correto.","Melhor O(n); médio/pior O(n²)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Considere o primeiro elemento ordenado.","Escolha o próximo valor.","Desloque valores maiores para a direita.","Insira o valor no espaço criado.","Repita até o final."],
            "flowchart TD
A[Início] --> B[Escolher próximo]
B --> C{Valor anterior > chave?}
C -- Sim --> D[Deslocar para direita]
D --> C
C -- Não --> E[Inserir chave]
E --> F{Fim?}
F -- Não --> B
F -- Sim --> G[Fim]"),
        new("merge-sort","sort","MergeSort","Divide a sequência em partes menores e depois intercala as partes já ordenadas.","O(n log n)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Divida a sequência ao meio.","Ordene recursivamente cada metade.","Compare as duas primeiras posições disponíveis.","Copie a menor para o resultado.","Continue até as duas metades serem consumidas."],
            "flowchart TD
A[Sequência] --> B[Dividir]
B --> C[Metade esquerda]
B --> D[Metade direita]
C --> E[Ordenar recursivamente]
D --> F[Ordenar recursivamente]
E --> G[Intercalar]
F --> G
G --> H[Sequência ordenada]"),
        new("quick-sort","sort","QuickSort","Escolhe um pivô, particiona os valores ao redor dele e repete nos subintervalos.","Médio O(n log n); pior O(n²)","Nenhuma.","Sequências lineares e representações lineares.",
            ["Escolha um pivô.","Percorra os valores da região.","Mova os menores ou iguais para antes do pivô.","Posicione o pivô.","Aplique o processo às duas regiões resultantes."],
            "flowchart TD
A[Região] --> B[Escolher pivô]
B --> C[Particionar]
C --> D[Posicionar pivô]
D --> E[Sub-região esquerda]
D --> F[Sub-região direita]
E --> G{Tamanho > 1?}
F --> H{Tamanho > 1?}
G -- Sim --> B
H -- Sim --> B"),
        new("heap-sort","sort","HeapSort","Organiza os valores com a propriedade de heap e extrai repetidamente o maior elemento.","O(n log n)","Nenhuma.","Sequências lineares; heaps podem ser demonstrados diretamente por sua representação em vetor.",
            ["Construa um max-heap.","Troque a raiz com o último elemento da região.","Reduza a região do heap.","Restaure a propriedade do heap.","Repita até restar um elemento."],
            "flowchart TD
A[Entrada] --> B[Construir max-heap]
B --> C[Trocar raiz com último]
C --> D[Reduzir heap]
D --> E[Sift down]
E --> F{Restam elementos?}
F -- Sim --> C
F -- Não --> G[Fim]"),
        new("linear-search","search","LinearSearch","Visita os elementos na ordem de travessia até encontrar o alvo.","O(n)","Nenhuma.","Todas as estruturas: usa índice, enumeração ou travessia apropriada.",
            ["Comece no primeiro elemento da sequência ou travessia.","Compare o valor atual com o alvo.","Retorne ao encontrar igualdade.","Caso contrário, avance para o próximo elemento.","Termine sem resultado quando a sequência acabar."],
            "flowchart TD
A[Início] --> B[Próximo elemento]
B --> C{É o alvo?}
C -- Sim --> D[Retornar posição]
C -- Não --> E{Há próximo?}
E -- Sim --> B
E -- Não --> F[Não encontrado]"),
        new("binary-search","search","BinarySearch","Divide repetidamente um intervalo ordenado ao meio para eliminar metade das possibilidades.","O(log n) com acesso indexado","Dados ordenados; acesso indexado eficiente.","Arrays e listas indexadas. Em árvore de busca, a operação equivalente é a busca pela propriedade da BST.",
            ["Defina início e fim do intervalo.","Calcule o meio.","Compare o meio com o alvo.","Elimine a metade impossível.","Repita até encontrar ou esvaziar o intervalo."],
            "flowchart TD
A[Intervalo ordenado] --> B[Calcular meio]
B --> C{Meio = alvo?}
C -- Sim --> D[Encontrado]
C -- Não --> E{Meio < alvo?}
E -- Sim --> F[Descartar esquerda]
E -- Não --> G[Descartar direita]
F --> H{Intervalo vazio?}
G --> H
H -- Não --> B
H -- Sim --> I[Não encontrado]"),
        new("jump-search","search","JumpSearch","Avança em blocos e realiza uma busca linear somente no bloco candidato.","O(√n)","Dados ordenados e acesso indexado.","Arrays e listas indexadas.",
            ["Escolha um tamanho de salto próximo de √n.","Salte enquanto o último valor do bloco for menor que o alvo.","Identifique o bloco candidato.","Percorra esse bloco linearmente.","Pare ao encontrar ou ultrapassar o alvo."],
            "flowchart TD
A[Dados ordenados] --> B[Definir salto √n]
B --> C[Saltar bloco]
C --> D{Bloco pode conter alvo?}
D -- Não --> C
D -- Sim --> E[Busca linear no bloco]
E --> F{Encontrou?}
F -- Sim --> G[Retornar]
F -- Não --> H[Não encontrado]"),
        new("interpolation-search","search","InterpolationSearch","Estima a posição do alvo usando a distribuição numérica entre os extremos.","Médio O(log log n); pior O(n)","Dados numéricos ordenados e distribuição relativamente uniforme.","Arrays e listas indexadas numéricas.",
            ["Observe os limites e seus valores.","Estime a posição proporcionalmente ao alvo.","Compare o valor estimado.","Restrinja o intervalo para o lado possível.","Repita até encontrar ou invalidar o intervalo."],
            "flowchart TD
A[Intervalo ordenado] --> B[Estimar posição]
B --> C{Valor = alvo?}
C -- Sim --> D[Encontrado]
C -- Não --> E{Valor < alvo?}
E -- Sim --> F[Mover limite inferior]
E -- Não --> G[Mover limite superior]
F --> B
G --> B")
    ];

    public static IReadOnlyList<AlgorithmGuide> Sorts =>
        All.Where(item => item.Category == "sort").ToArray();

    public static IReadOnlyList<AlgorithmGuide> Searches =>
        All.Where(item => item.Category == "search").ToArray();

    public static AlgorithmGuide Get(string key) =>
        All.First(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
}
