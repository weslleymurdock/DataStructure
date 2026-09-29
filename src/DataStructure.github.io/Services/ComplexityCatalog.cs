namespace DataStructure.github.io.Services;

public sealed record ComplexityGuide(
    string Notation,
    string Performance,
    string Meaning,
    string Calculation,
    string Reading,
    MudBlazor.Color Color);

public static class ComplexityCatalog
{
    public static ComplexityGuide For(string? complexity) 
    {
        var value = complexity ?? "Não informado";

        if (value.Contains("O(n²)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Crescimento quadrático", "O trabalho cresce aproximadamente com o quadrado do tamanho da entrada.", "Conte os loops dependentes de n; dois níveis completos de repetição produzem n × n.", "Lê-se: ordem de n ao quadrado.", MudBlazor.Color.Error);

        if (value.Contains("O(n log n)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Crescimento quase linear", "Combina uma quantidade linear de trabalho com uma redução logarítmica por nível.", "Frequentemente aparece quando a entrada é dividida em partes e cada nível processa aproximadamente n elementos.", "Lê-se: ordem de n vezes log de n.", MudBlazor.Color.Warning);

        if (value.Contains("O(n · d)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Linear em n e d", "O custo cresce proporcionalmente ao número de candidatos e à dimensão processada de cada candidato.", "Multiplique o número de itens n pela dimensão d examinada em cada comparação.", "Lê-se: ordem de n vezes d.", MudBlazor.Color.Warning);

        if (value.Contains("O(V + E)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Linear no grafo", "O trabalho acompanha os vértices V e as arestas E que precisam ser examinados.", "Some o número de vértices visitados ao número de arestas examinadas.", "Lê-se: ordem de V mais E.", MudBlazor.Color.Success);

        if (value.Contains("O(log log n)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Crescimento muito lento", "A quantidade de trabalho aumenta muito lentamente à medida que n cresce.", "A cada etapa, a posição é estimada e o intervalo útil é reduzido de forma muito agressiva.", "Lê-se: ordem de log de log de n.", MudBlazor.Color.Success);

        if (value.Contains("O(log n)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Crescimento logarítmico", "O trabalho cresce lentamente porque cada etapa reduz substancialmente o espaço de busca.", "Se o espaço é dividido aproximadamente pela metade, são necessárias cerca de log₂(n) etapas.", "Lê-se: ordem de log de n.", MudBlazor.Color.Success);

        if (value.Contains("O(√n)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Crescimento sublinear", "O algoritmo visita uma fração da entrada, mas mais que uma quantidade logarítmica.", "Um salto de tamanho aproximadamente √n produz cerca de √n posições ou blocos relevantes.", "Lê-se: ordem da raiz quadrada de n.", MudBlazor.Color.Warning);

        if (value.Contains("O(n)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Crescimento linear", "O trabalho cresce proporcionalmente ao tamanho da entrada.", "Um percurso completo normalmente executa uma operação constante para cada elemento: n × O(1).", "Lê-se: ordem de n.", MudBlazor.Color.Warning);

        if (value.Contains("O(1)", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Tempo constante", "A quantidade de trabalho não depende do tamanho da entrada.", "A operação executa uma quantidade limitada de passos independentemente de n.", "Lê-se: ordem constante.", MudBlazor.Color.Success);

        if (value.Contains("Depende", StringComparison.OrdinalIgnoreCase) ||
            value.Contains("não determinado", StringComparison.OrdinalIgnoreCase))
            return Guide(value, "Dependente do problema", "A complexidade varia conforme a estrutura, os dados ou a heurística utilizada.", "É necessário analisar V, E, n, distribuição dos dados ou a heurística específica.", "Lê-se conforme as variáveis do problema.", MudBlazor.Color.Warning);

        return Guide(value, "Complexidade variável", "A expressão contém mais de um cenário ou não se enquadra nas categorias simplificadas deste glossário.", "Use o termo dominante do crescimento para comparar entradas grandes.", "Leia a expressão respeitando cada variável apresentada.", MudBlazor.Color.Warning);
    }

    public static ComplexityGuide ForStructure(string key) => key switch
    {
        "array" => Guide("O(1) acesso", "Acesso constante", "Acesso por índice é direto; operações que percorrem ou deslocam elementos podem ser O(n).", "O endereço lógico é calculado a partir do índice.", "Lê-se: acesso em ordem constante.", MudBlazor.Color.Success),
        "collection" or "list" => Guide("O(1) acesso; O(n) inserção/remoção", "Misto", "Acesso indexado é constante, enquanto deslocamentos podem exigir tempo linear.", "Analise separadamente a operação: leitura direta ou movimentação de um sufixo.", "Lê-se: depende da operação.", MudBlazor.Color.Warning),
        "linkedlist" or "nodelist" => Guide("O(1) extremidades; O(n) busca", "Misto", "Operações nas extremidades podem ser constantes, mas localizar uma posição exige percorrer nós.", "Conte os nós visitados até encontrar a posição ou valor.", "Lê-se: constante nas extremidades e linear na travessia.", MudBlazor.Color.Warning),
        "circularlinkedlist" => Guide("O(1) extremidades; O(n) busca", "Misto", "A circularidade mantém a ligação final-inicial, mas não elimina a necessidade de percorrer nós para localizar valores.", "A busca pode visitar até n nós antes de retornar ao ponto inicial.", "Lê-se: constante nas extremidades e linear na busca.", MudBlazor.Color.Warning),
        "queue" or "stack" => Guide("O(1) operações principais", "Tempo constante", "Enfileirar/desenfileirar ou empilhar/desempilhar usam as extremidades da estrutura.", "Cada operação altera apenas um número fixo de referências.", "Lê-se: ordem constante.", MudBlazor.Color.Success),
        "deque" or "deck" => Guide("O(1) operações nas extremidades", "Tempo constante", "As duas extremidades são mantidas por referências diretas.", "Cada operação modifica apenas as referências próximas a Head ou Tail.", "Lê-se: ordem constante nas extremidades.", MudBlazor.Color.Success),
        "priorityqueue" or "heap" or "maxheap" => Guide("O(log n) inserção/remoção; O(1) consulta da raiz", "Crescimento logarítmico", "A raiz é acessível diretamente e ajustes do heap percorrem no máximo a altura da árvore.", "Uma árvore de heap completa tem altura aproximadamente log₂(n).", "Lê-se: consulta constante e reajuste logarítmico.", MudBlazor.Color.Success),
        "binarytree" => Guide("O(n) travessia; O(h) busca estrutural", "Linear ou dependente da altura", "Percorrer todos os nós custa n; operações guiadas pela altura custam O(h).", "Conte os nós visitados ou a altura h percorrida.", "Lê-se: ordem de n para percorrer toda a árvore.", MudBlazor.Color.Warning),
        "binarysearchtree" => Guide("O(log n) médio; O(n) pior caso", "Dependente do balanceamento", "Uma BST balanceada reduz a busca pela altura; uma árvore degenerada pode virar uma lista.", "A busca percorre a altura h; balanceada h ≈ log₂(n), degenerada h ≈ n.", "Lê-se: logarítmica em média quando a árvore permanece equilibrada.", MudBlazor.Color.Warning),
        "hashtable" => Guide("O(1) médio; O(n) pior caso", "Dependente das colisões", "O acesso médio é constante quando a distribuição dos hashes é adequada; muitas colisões aumentam a busca.", "O custo depende do tamanho do bucket ou cadeia de colisões.", "Lê-se: constante em média e linear no pior caso.", MudBlazor.Color.Success),
        "graph" => Guide("O(V + E)", "Linear no grafo", "Travessias precisam considerar os vértices e as arestas alcançáveis.", "Some V, o número de vértices visitados, e E, o número de arestas examinadas.", "Lê-se: ordem de V mais E.", MudBlazor.Color.Success),
        "weightedgraph" => Guide("O(V + E) para travessia", "Linear no grafo", "A representação de adjacência permite percorrer vértices e arestas proporcionalmente ao tamanho do grafo.", "Conte V vértices e E relações examinadas; algoritmos de caminho podem acrescentar custos de fila de prioridade.", "Lê-se: ordem de V mais E para percorrer a representação.", MudBlazor.Color.Success),
        _ => Guide("Variável", "Complexidade variável", "Consulte as operações individuais da estrutura para determinar o custo.", "Analise a operação, o número de elementos e as referências percorridas.", "Leia a expressão associada à operação.", MudBlazor.Color.Warning)
    };

    private static ComplexityGuide Guide(string notation, string performance, string meaning, string calculation, string reading, MudBlazor.Color color) =>
        new(notation, performance, meaning, calculation, reading, color);
}
