namespace DataStructure.github.io.Services;

public sealed record FlowStepGuide(
    int Number,
    string Element,
    string Explanation,
    string Pseudocode,
    string Direction);

public static class FlowStepCatalog
{
    public static IReadOnlyList<FlowStepGuide> ForAlgorithm(AlgorithmGuide guide)
    {
        IReadOnlyList<string> steps = guide.Steps ?? [];
        return steps.Select((step, index) =>
        {
            var number = index + 1;
            var direction = number < steps.Count
                ? $"Avança para o step {number + 1}."
                : "Conclui a operação.";

            if (step.Contains("repita", StringComparison.OrdinalIgnoreCase) ||
                step.Contains("até", StringComparison.OrdinalIgnoreCase))
            {
                direction = number > 1
                    ? $"Retorna ao step 2 enquanto a condição de repetição for verdadeira; depois avança para o step {Math.Min(number + 1, steps.Count)}."
                    : "Retorna ao início do ciclo enquanto a condição de repetição for verdadeira.";
            }

            return new(
                number,
                AlgorithmElement(guide.Key, number),
                step,
                $"// {step}",
                direction);
        }).ToArray();
    }

    public static IReadOnlyList<FlowStepGuide> ForStructure(string key) => key switch
    {
        "array" =>
        [
            new(1, "Armazenamento", "Crie um vetor contíguo de tamanho fixo. O tamanho determina quantas posições existirão durante toda a vida da estrutura.", "items = new T[length]", "Avança para o step 2."),
            new(2, "Índice", "Cada posição é identificada por um índice iniciado em zero. O índice permite calcular diretamente a posição sem percorrer elementos anteriores.", "value = items[index]", "Avança para o step 3."),
            new(3, "Contiguidade", "As posições pertencem ao mesmo bloco lógico de armazenamento. Por isso, o acesso indexado é direto e não depende da quantidade de elementos anteriores.", "address = baseAddress + index * elementSize", "A estrutura pode retornar diretamente ao step 2 para qualquer outro índice."),
            new(4, "Limite", "O último índice válido é length - 1. Antes de acessar, valide o intervalo para evitar uma leitura fora do armazenamento.", "if (index < 0 || index >= length) throw", "Conclui a explicação estrutural.")
        ],
        "collection" =>
        [
            new(1, "Estado", "Separe capacidade física e quantidade lógica. O vetor pode ter posições livres depois de count.", "items = new T[capacity]; count = 0", "Avança para o step 2."),
            new(2, "Capacidade", "Antes de inserir, verifique se count + 1 cabe no vetor. Se não couber, aumente a capacidade antes de escrever.", "EnsureCapacity(count + 1)", "Se houver espaço, avança para o step 3; caso contrário, segue para o step 4."),
            new(3, "Inserção", "Escreva o item na primeira posição livre e só então incremente count para torná-lo parte da coleção.", "items[count] = item; count++", "Conclui a inserção ou retorna ao step 2 para outra inserção."),
            new(4, "Crescimento", "Crie um vetor maior, copie os elementos lógicos e substitua o armazenamento. Depois retorne à escrita do novo elemento.", "capacity *= 2; resize(items, capacity)", "Retorna ao step 3.")
        ],
        "list" =>
        [
            new(1, "Vetor de suporte", "Os elementos lógicos ocupam posições consecutivas de 0 até count - 1 em um vetor redimensionável.", "items[0..count-1]", "Avança para o step 2."),
            new(2, "Acesso", "O índice aponta diretamente para uma posição. Nenhuma travessia é necessária para leitura ou substituição.", "value = items[index]", "Para inserir ou remover, avança para o step 3."),
            new(3, "Inserção", "Ao inserir no meio, desloque o sufixo uma posição para a direita e escreva o novo valor no índice.", "copy(items, index, index + 1); items[index] = value", "Avança para o step 4."),
            new(4, "Remoção", "Ao remover, desloque o sufixo uma posição para a esquerda, reduza count e limpe a última posição lógica.", "copy(items, index + 1, index); count--", "Conclui a operação.")
        ],
        "linkedlist" =>
        [
            new(1, "Head", "Head referencia o primeiro nó. Se a lista estiver vazia, head e tail são nulos.", "head = firstNode", "Avança para o step 2."),
            new(2, "Nó duplamente ligado", "Cada nó possui Value, Next e Previous. Next aponta para frente e Previous permite voltar ao nó anterior.", "node.Next = next; node.Previous = previous", "Avança para o step 3."),
            new(3, "Tail", "Tail referencia o último nó. O primeiro nó tem Previous nulo e o último tem Next nulo.", "tail = lastNode; tail.Next = null", "Avança para o step 4."),
            new(4, "Travessia", "Para localizar um índice, comece por head ou tail conforme a proximidade e avance pela referência apropriada.", "current = index < count / 2 ? head : tail", "Retorna ao step 2 para cada nó visitado até alcançar a posição.")
        ],
        "nodelist" =>
        [
            new(1, "Head", "Head identifica o primeiro nó da cadeia. Em uma lista vazia, head é nulo.", "head = firstNode", "Avança para o step 2."),
            new(2, "Next", "Cada nó possui somente a referência para o próximo nó. Não existe ponteiro para voltar.", "current.Next = next", "Avança para o step 3."),
            new(3, "Tail", "Tail identifica o último nó para permitir anexar elementos sem percorrer toda a cadeia.", "tail = lastNode", "Avança para o step 4."),
            new(4, "Acesso", "Para chegar ao índice solicitado, comece em head e siga Next exatamente index vezes.", "current = head; repeat index times: current = current.Next", "Retorna ao step 2 durante a travessia.")
        ],
        "circularlinkedlist" =>
        [
            new(1, "Head", "Head marca o início lógico da volta pelo ciclo.", "head = firstNode", "Avança para o step 2."),
            new(2, "Tail", "Tail referencia o último nó e tail.Next aponta novamente para head.", "tail.Next = head", "Avança para o step 3."),
            new(3, "Ciclo", "Seguir Next nunca produz null; depois do último nó, a referência volta ao primeiro.", "current = current.Next", "Retorna ao step 2 enquanto current != head."),
            new(4, "Parada", "Use o nó inicial ou Count como sentinela. Esperar null causaria repetição infinita.", "do { visit(current); current = current.Next; } while (current != head)", "Conclui após uma volta completa.")
        ],
        "queue" =>
        [
            new(1, "FIFO", "A fila preserva a ordem de chegada: o primeiro item inserido será o primeiro removido.", "enqueue(item)", "Avança para o step 2."),
            new(2, "Tail", "Enqueue cria um nó no fim e conecta o tail anterior ao novo nó.", "tail.Next = node; tail = node", "Avança para o step 3."),
            new(3, "Head", "Dequeue lê o primeiro nó e move head para o próximo, removendo o item mais antigo.", "value = head.Value; head = head.Next", "Avança para o step 4."),
            new(4, "Estado vazio", "Se head ficar nulo após a remoção, tail também deve ser nulo para manter a fila consistente.", "if (head == null) tail = null", "Conclui a operação.")
        ],
        "stack" =>
        [
            new(1, "LIFO", "A pilha opera exclusivamente pelo topo: o último item inserido é o primeiro removido.", "push(item)", "Avança para o step 2."),
            new(2, "Push", "O novo nó aponta para o topo anterior e passa a ser o novo topo.", "node.Next = top; top = node", "Avança para o step 3."),
            new(3, "Peek", "Peek lê top.Value sem alterar referências ou Count.", "value = top.Value", "Retorna ao step 2 para uma nova inserção ou consulta."),
            new(4, "Pop", "Pop salva o valor do topo, avança top e decrementa Count.", "value = top.Value; top = top.Next; count--", "Retorna ao step 2 enquanto houver elementos.")
        ],
        "deque" or "deck" =>
        [
            new(1, "Dois extremos", "A estrutura mantém head e tail para permitir operações eficientes nas duas extremidades.", "head = first; tail = last", "Avança para o step 2."),
            new(2, "Entrada", "Inserir no início conecta o novo nó antes de head; inserir no fim conecta depois de tail.", "addFirst(x) / addLast(x)", "Avança para o step 3."),
            new(3, "Saída", "Remover em uma extremidade salva o valor e atualiza a referência daquele extremo.", "removeFirst() / removeLast()", "Retorna ao step 2 para novas operações."),
            new(4, "Invariante", "Quando o último elemento sai, head e tail devem voltar ao estado vazio.", "if (count == 0) head = tail = null", "Conclui a operação.")
        ],
        "priorityqueue" =>
        [
            new(1, "Heap mínimo", "A raiz do heap contém o menor elemento disponível para remoção.", "heap[0] = minimum", "Avança para o step 2."),
            new(2, "Índices", "Em um heap armazenado em vetor, o pai de i está em floor((i - 1) / 2) e os filhos em 2i + 1 e 2i + 2.", "parent = (i - 1) / 2", "Avança para o step 3."),
            new(3, "Sift up", "Ao inserir, coloque o item no fim e troque com o pai enquanto o novo item tiver prioridade maior.", "while (item < parent) swap(item, parent)", "Retorna ao step 2 durante as trocas."),
            new(4, "Sift down", "Ao remover a raiz, mova o último item para ela e troque com o menor filho até restaurar a propriedade.", "root = removeLast(); siftDown(root)", "Retorna ao step 2 durante o reajuste.")
        ],
        "binarytree" =>
        [
            new(1, "Raiz", "A árvore começa em um nó raiz; cada nó pode possuir zero, um ou dois filhos.", "root = node", "Avança para o step 2."),
            new(2, "Left", "Left referencia a raiz da subárvore esquerda do nó atual.", "current.Left = left", "Avança para o step 3."),
            new(3, "Right", "Right referencia a raiz da subárvore direita do nó atual.", "current.Right = right", "Avança para o step 4."),
            new(4, "Recursão", "Cada filho também é uma árvore. A mesma regra se aplica recursivamente até encontrar uma referência nula.", "visit(node.Left); visit(node.Right)", "Retorna aos steps 2 e 3 para cada subárvore.")
        ],
        "binarysearchtree" =>
        [
            new(1, "Current", "Comece na raiz e use current como cursor da busca ou inserção.", "current = root", "Avança para o step 2."),
            new(2, "Comparação", "Compare o alvo com current.Value para escolher uma única subárvore possível.", "comparison = compare(target, current.Value)", "Se target < current, avança para o step 3; caso contrário, para o step 4."),
            new(3, "Esquerda", "Valores menores seguem para Left. Atualize current e repita a comparação.", "current = current.Left", "Retorna ao step 2."),
            new(4, "Direita", "Valores maiores ou iguais, conforme a política de duplicatas, seguem para Right.", "current = current.Right", "Retorna ao step 2 até encontrar o valor ou uma referência nula.")
        ],
        "heap" => Heap(false),
        "maxheap" => Heap(true),
        "hashtable" =>
        [
            new(1, "Hash", "Calcule o hash da chave. O hash é uma representação usada para escolher o bucket, não o índice final.", "hash = key.GetHashCode()", "Avança para o step 2."),
            new(2, "Bucket", "Normalize o hash e aplique módulo pela capacidade para obter uma posição válida.", "bucketIndex = normalize(hash) % capacity", "Avança para o step 3."),
            new(3, "Colisão", "Procure a chave somente dentro do bucket. Chaves diferentes podem compartilhar o mesmo bucket e formam uma cadeia.", "for entry in buckets[bucketIndex] ...", "Retorna ao step 3 enquanto houver entradas."),
            new(4, "Resize", "Ao ultrapassar o fator de carga, aumente a capacidade e refaça o hash de todas as entradas, pois os índices podem mudar.", "capacity *= 2; rehash(allEntries)", "Retorna ao step 2 para recalcular os buckets.")
        ],
        "graph" =>
        [
            new(1, "Vértice", "Registre cada vértice como uma chave de uma tabela de adjacência.", "adjacency[vertex] = new HashSet<T>()", "Avança para o step 2."),
            new(2, "Aresta", "Uma aresta direcionada adiciona o destino ao conjunto de vizinhos da origem.", "adjacency[from].Add(to)", "Avança para o step 3."),
            new(3, "Vizinhos", "Consultar um vértice retorna diretamente os próximos vértices alcançáveis.", "foreach (var next in adjacency[current])", "Retorna ao step 3 durante uma travessia."),
            new(4, "Visitação", "Um algoritmo de busca usa visited para processar cada vértice no máximo uma vez.", "if (visited.Add(next)) enqueueOrRecurse(next)", "Retorna ao step 3 enquanto houver vizinhos.")
        ],
        "weightedgraph" =>
        [
            new(1, "Vértice", "Cada vértice possui uma lista de adjacência própria.", "adjacency.TryAdd(vertex, [])", "Avança para o step 2."),
            new(2, "Aresta ponderada", "A relação guarda destino e peso. O peso representa custo, distância ou outra métrica.", "adjacency[from].Add((to, weight))", "Avança para o step 3."),
            new(3, "Vizinhança", "Algoritmos de caminho percorrem os pares destino/peso para calcular custos candidatos.", "foreach (var (next, weight) in adjacency[current])", "Retorna ao step 3 durante a expansão."),
            new(4, "Custo", "O peso pertence à aresta, portanto o custo acumulado é atualizado ao atravessar a relação.", "candidate = distance[current] + weight", "Conclui a representação.")
        ],
        _ =>
        [
            new(1, "Armazenamento", "Identifique como os elementos e referências são armazenados internamente.", "storage = initialize()", "Avança para o step 2."),
            new(2, "Operação", "Aplique a operação sobre o estado interno preservando as invariantes da estrutura.", "state = operate(state)", "Avança para o step 3."),
            new(3, "Resultado", "Retorne o resultado ou exponha o estado atualizado.", "return result", "Conclui.")
        ]
    };

    public static IReadOnlyList<FlowStepGuide> ForMethod(string key, MethodGuide method)
    {
        var name = method.Name;

        if (key is "array" or "collection" or "list")
            return ArrayMethod(name, method.Explanation);

        if (key is "linkedlist" or "deque" or "deck")
            return LinkedMethod(name, method.Explanation);

        if (key == "queue")
            return QueueMethod(name, method.Explanation);

        if (key == "stack")
            return StackMethod(name, method.Explanation);

        if (key == "nodelist")
            return NodeListMethod(name, method.Explanation);

        if (key == "circularlinkedlist")
            return CircularMethod(name, method.Explanation);

        if (key == "hashtable")
            return HashMethod(name, method.Explanation);

        if (key == "graph")
            return GraphMethod(name, method.Explanation);

        if (key == "weightedgraph")
            return WeightedGraphMethod(name, method.Explanation);

        if (key == "binarysearchtree")
            return BstMethod(name, method.Explanation);

        if (key == "binarytree")
            return TreeMethod(name, method.Explanation);

        if (key is "heap" or "maxheap" or "priorityqueue")
            return HeapMethod(name, method.Explanation, key == "maxheap");

        return GenericMethod(name, method.Explanation);
    }

    private static IReadOnlyList<FlowStepGuide> ArrayMethod(string name, string explanation) => name switch
    {
        "Add" => [
            new(1, "Capacidade", "Garanta que exista uma posição livre; se não houver, redimensione antes de escrever.", "EnsureCapacity(count + 1)", "Avança para o step 2."),
            new(2, "Escrita", "Grave o item na primeira posição livre.", "items[count] = item", "Avança para o step 3."),
            new(3, "Count", "Incremente a quantidade lógica de elementos.", "count++", "Conclui.")
        ],
        "RemoveAt" or "InsertAt" or "Insert" => [
            new(1, "Índice", "Valide o índice permitido pela operação.", "validate(index)", "Avança para o step 2."),
            new(2, "Deslocamento", "Mova o trecho afetado para abrir espaço ou preencher o buraco deixado pela remoção.", "copyRange(items, index)", "Avança para o step 3."),
            new(3, "Estado", "Escreva o valor quando for inserção ou reduza Count e limpe a posição final quando for remoção.", "items[index] = value; count += delta", "Conclui.")
        ],
        "IndexOf" or "Contains" => [
            new(1, "Cursor", "Comece na primeira posição lógica.", "index = 0", "Avança para o step 2."),
            new(2, "Comparação", "Compare o elemento atual com o alvo.", "if (equals(items[index], target)) return index", "Se não encontrar, avança para o step 3."),
            new(3, "Avanço", "Incremente o índice enquanto houver elementos.", "index++", "Retorna ao step 2."),
            new(4, "Ausência", "Ao alcançar Count sem encontrar o alvo, retorne a representação de ausência.", "return -1 // or false", "Conclui.")
        ],
        _ => GenericMethod(name, explanation)
    };

    private static IReadOnlyList<FlowStepGuide> LinkedMethod(string name, string explanation) => name switch
    {
        "AddFirst" => [
            new(1, "Novo nó", "Crie o nó e faça Next apontar para o head anterior.", "node.Next = head", "Avança para o step 2."),
            new(2, "Previous", "Atualize Previous do antigo head para manter a ligação dupla.", "if (head != null) head.Previous = node", "Avança para o step 3."),
            new(3, "Head", "Torne o novo nó o primeiro e incremente Count.", "head = node; count++", "Conclui.")
        ],
        "AddLast" => [
            new(1, "Novo nó", "Crie o nó e faça Previous apontar para o tail anterior.", "node.Previous = tail", "Avança para o step 2."),
            new(2, "Next", "Conecte o antigo tail ao novo nó.", "if (tail != null) tail.Next = node", "Avança para o step 3."),
            new(3, "Tail", "Torne o novo nó a última posição e incremente Count.", "tail = node; count++", "Conclui.")
        ],
        "RemoveFirst" or "RemoveLast" => [
            new(1, "Valor", "Salve o valor da extremidade antes de alterar os ponteiros.", "value = endpoint.Value", "Avança para o step 2."),
            new(2, "Referência", "Mova head para Next ou tail para Previous, removendo a extremidade lógica.", "head = head.Next // or tail = tail.Previous", "Avança para o step 3."),
            new(3, "Invariante", "Corrija a referência da nova extremidade ou zere head e tail se a lista ficou vazia.", "fixBoundaryReferences(); count--", "Conclui e retorna o valor.")
        ],
        _ => GenericMethod(name, explanation)
    };

    private static IReadOnlyList<FlowStepGuide> QueueMethod(string name, string explanation) =>
        name == "Enqueue"
            ? [
                new(1, "Novo nó", "Crie o item que será anexado ao fim.", "node = new Node(item)", "Avança para o step 2."),
                new(2, "Tail", "Conecte o tail atual ao novo nó ou inicialize head e tail quando a fila estiver vazia.", "if (tail == null) head = tail = node; else tail.Next = node; tail = node", "Avança para o step 3."),
                new(3, "Count", "Atualize a quantidade lógica.", "count++", "Conclui.")]
            : name == "Dequeue"
                ? [
                    new(1, "Head", "Leia o valor do primeiro item, que é o mais antigo.", "value = head.Value", "Avança para o step 2."),
                    new(2, "Avanço", "Mova head para o próximo nó.", "head = head.Next", "Avança para o step 3."),
                    new(3, "Vazia", "Se head ficou nulo, tail também deve ser nulo.", "if (head == null) tail = null; count--", "Conclui e retorna value.")]
                : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> StackMethod(string name, string explanation) =>
        name == "Push"
            ? [
                new(1, "Novo nó", "Faça o novo nó apontar para o topo anterior.", "node.Next = top", "Avança para o step 2."),
                new(2, "Top", "Substitua top pela nova referência.", "top = node", "Avança para o step 3."),
                new(3, "Count", "Incremente Count.", "count++", "Conclui.")]
            : name == "Pop"
                ? [
                    new(1, "Valor", "Salve o valor do topo antes de removê-lo.", "value = top.Value", "Avança para o step 2."),
                    new(2, "Avanço", "Mova top para o próximo nó.", "top = top.Next", "Avança para o step 3."),
                    new(3, "Count", "Decremente Count e retorne o valor salvo.", "count--; return value", "Conclui.")]
                : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> NodeListMethod(string name, string explanation) =>
        name == "Add"
            ? [
                new(1, "Novo nó", "Crie o nó a ser anexado.", "node = new Node(item)", "Avança para o step 2."),
                new(2, "Next", "Conecte o tail atual ao novo nó ou inicialize head e tail quando vazia.", "if (tail == null) head = tail = node; else tail.Next = node", "Avança para o step 3."),
                new(3, "Tail", "Atualize tail e Count.", "tail = node; count++", "Conclui.")]
            : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> CircularMethod(string name, string explanation) =>
        name is "Contains" or "Enumerate"
            ? [
                new(1, "Sentinela", "Comece em head e guarde o primeiro nó como referência de parada.", "start = head; current = head", "Avança para o step 2."),
                new(2, "Visita", "Processe current e avance por Next.", "visit(current); current = current.Next", "Retorna ao step 2 enquanto current != start."),
                new(3, "Parada", "Uma volta completa termina quando o cursor retorna ao head.", "while (current != start)", "Conclui.")]
            : name is "AddFirst" or "AddLast"
                ? [
                    new(1, "Novo nó", "Crie o nó e faça sua referência Next apontar para head, porque o ciclo precisa continuar fechado.", "node.Next = head", "Avança para o step 2."),
                    new(2, "Tail", "Conecte tail.Next ao novo nó quando a inserção ocorrer no fim ou ao novo head quando ocorrer no início.", "tail.Next = newBoundary", "Avança para o step 3."),
                    new(3, "Extremo", "Atualize head ou tail e incremente Count sem romper a ligação tail -> head.", "head = node; count++ // or tail = node", "Conclui.")]
                : name is "RemoveFirst" or "RemoveLast"
                    ? [
                        new(1, "Valor", "Guarde o valor da extremidade que será removida.", "value = boundary.Value", "Avança para o step 2."),
                        new(2, "Religação", "Avance a extremidade; o último nó deve continuar apontando para o novo head.", "head = head.Next; tail.Next = head", "Avança para o step 3."),
                        new(3, "Caso unitário", "Quando Count era 1, o ciclo deixa de existir e head e tail devem ser nulos.", "if (count == 1) head = tail = null; count--", "Conclui e retorna value.")]
                    : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> HashMethod(string name, string explanation) =>
        name is "TryGetValue" or "ContainsKey"
            ? [
                new(1, "Hash", "Calcule o hash da chave.", "hash = hashCode(key)", "Avança para o step 2."),
                new(2, "Bucket", "Normalize o hash e obtenha o índice do bucket.", "index = normalize(hash) % capacity", "Avança para o step 3."),
                new(3, "Cadeia", "Compare a chave com cada entrada daquele bucket.", "for entry in buckets[index] ...", "Retorna ao step 3 até encontrar ou terminar."),
                new(4, "Resultado", "Retorne o valor encontrado ou informe ausência.", "return found ? value : false", "Conclui.")]
            : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> GraphMethod(string name, string explanation) =>
        name == "AddEdge"
            ? [
                new(1, "Vértices", "Garanta que origem e destino estejam cadastrados.", "AddVertex(from); AddVertex(to)", "Avança para o step 2."),
                new(2, "Adjacência", "Adicione o destino ao conjunto de vizinhos da origem.", "adjacency[from].Add(to)", "Avança para o step 3."),
                new(3, "Direção", "A relação from -> to não cria automaticamente to -> from.", "edge = from -> to", "Conclui.")]
            : name == "BreadthFirst"
                ? [
                    new(1, "Inicialização", "Marque a origem e coloque-a na fila.", "visited = {start}; queue.Enqueue(start)", "Avança para o step 2."),
                    new(2, "Fila", "Retire o próximo vértice da fila.", "current = queue.Dequeue()", "Avança para o step 3."),
                    new(3, "Vizinhos", "Descubra e enfileire vizinhos ainda não visitados.", "if (visited.Add(next)) queue.Enqueue(next)", "Retorna ao step 2 enquanto a fila não estiver vazia."),
                    new(4, "Término", "Quando a fila esvaziar, todos os vértices alcançáveis foram processados.", "while (queue.Count > 0) ...", "Conclui.")]
                : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> WeightedGraphMethod(string name, string explanation) =>
        name == "AddEdge"
            ? [
                new(1, "Vértices", $"Garanta que origem e destino existam antes de criar a relação. {explanation}", "AddVertex(from); AddVertex(to)", "Avança para o step 2."),
                new(2, "Peso", "Valide e associe o custo da aresta à relação entre origem e destino.", "edge = (to, weight)", "Avança para o step 3."),
                new(3, "Adjacência", "Armazene a aresta na lista de vizinhos da origem. Em um grafo direcionado, a relação inversa precisa ser adicionada explicitamente.", "adjacency[from].Add(edge)", "Conclui.")
            ]
            : name == "Neighbors"
                ? [
                    new(1, "Origem", "Localize a lista de adjacência do vértice consultado.", "edges = adjacency[vertex]", "Avança para o step 2."),
                    new(2, "Destino e peso", "Percorra cada relação recuperando o destino e o custo associado.", "foreach (edge in edges) yield (edge.To, edge.Weight)", "Retorna ao step 2 até consumir todas as arestas."),
                    new(3, "Resultado", "Retorne a sequência de relações sem alterar o grafo.", "return edges", "Conclui.")
                ]
                : name == "Vertices"
                    ? [
                        new(1, "Tabela", "Consulte as chaves da estrutura de adjacência.", "vertices = adjacency.Keys", "Avança para o step 2."),
                        new(2, "Materialização", "Materialize ou enumere as chaves sem modificar as relações armazenadas.", "return vertices", "Conclui.")
                    ]
                    : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> BstMethod(string name, string explanation) =>
        name is "Insert" or "Contains"
            ? [
                new(1, "Current", "Comece na raiz.", "current = root", "Avança para o step 2."),
                new(2, "Comparação", "Compare o alvo com o valor atual.", "comparison = compare(target, current.Value)", "Se menor, avança ao step 3; caso contrário, ao step 4."),
                new(3, "Esquerda", "Desça para Left quando o alvo for menor.", "current = current.Left", "Retorna ao step 2."),
                new(4, "Direita", "Desça para Right quando o alvo for maior ou igual.", "current = current.Right", "Retorna ao step 2 até encontrar ou alcançar null.")]
            : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> TreeMethod(string name, string explanation) =>
        name switch
        {
            "PreOrder" => [
                new(1, "Nó", "Pare quando o nó for nulo.", "if (node == null) return", "Avança ao step 2."),
                new(2, "Raiz", "Processe o valor antes dos filhos.", "yield node.Value", "Avança ao step 3."),
                new(3, "Esquerda", "Percorra recursivamente a subárvore esquerda.", "PreOrder(node.Left)", "Retorna ao step 1 durante a recursão."),
                new(4, "Direita", "Depois da esquerda, percorra a subárvore direita.", "PreOrder(node.Right)", "Retorna ao step 1 durante a recursão.")],
            "InOrder" => [
                new(1, "Nó", "Pare quando o nó for nulo.", "if (node == null) return", "Avança ao step 2."),
                new(2, "Esquerda", "Percorra a esquerda antes da raiz.", "InOrder(node.Left)", "Retorna ao step 1 durante a recursão."),
                new(3, "Raiz", "Processe o valor depois da esquerda.", "yield node.Value", "Avança ao step 4."),
                new(4, "Direita", "Percorra a direita depois da raiz.", "InOrder(node.Right)", "Retorna ao step 1 durante a recursão.")],
            "PostOrder" => [
                new(1, "Nó", "Pare quando o nó for nulo.", "if (node == null) return", "Avança ao step 2."),
                new(2, "Esquerda", "Percorra a esquerda.", "PostOrder(node.Left)", "Retorna ao step 1 durante a recursão."),
                new(3, "Direita", "Percorra a direita.", "PostOrder(node.Right)", "Retorna ao step 1 durante a recursão."),
                new(4, "Raiz", "Processe o valor somente depois dos dois filhos.", "yield node.Value", "Retorna ao chamador.")],
            _ => GenericMethod(name, explanation)
        };

    private static IReadOnlyList<FlowStepGuide> HeapMethod(string name, string explanation, bool max) =>
        name is "Add" or "Enqueue"
            ? [
                new(1, "Inserção", "Acrescente o valor no fim para preservar a forma de árvore completa.", "heap.Add(value)", "Avança para o step 2."),
                new(2, "Pai", "Calcule o índice do pai.", "parent = (index - 1) / 2", "Avança para o step 3."),
                new(3, "Sift up", $"Troque enquanto o novo valor violar a propriedade {(max ? "máxima" : "mínima")} do heap.", "while (violates(index, parent)) swap(index, parent)", "Retorna ao step 2 durante as trocas."),
                new(4, "Fim", "Quando não houver violação, a invariante do heap está restaurada.", "return", "Conclui.")]
            : name is "Remove" or "Dequeue"
                ? [
                    new(1, "Raiz", $"Guarde a raiz, que contém o {(max ? "máximo" : "mínimo")} valor.", "result = heap[0]", "Avança para o step 2."),
                    new(2, "Último", "Remova o último item e coloque-o na raiz quando ainda houver elementos.", "last = removeLast(); heap[0] = last", "Avança para o step 3."),
                    new(3, "Sift down", $"Troque com o melhor filho enquanto a propriedade {(max ? "máxima" : "mínima")} for violada.", "while (violates(parent, bestChild)) swap(parent, bestChild)", "Retorna ao step 3 até restaurar a propriedade."),
                    new(4, "Resultado", "O heap continua completo e o valor removido pode ser retornado.", "return result", "Conclui.")]
                : GenericMethod(name, explanation);

    private static IReadOnlyList<FlowStepGuide> GenericMethod(string name, string explanation) => name switch
    {
        "Peek" or "PeekFirst" or "PeekLast" or "Count" or "AsArray" or "Vertices" => [
            new(1, "Consulta", $"A operação {name} lê o estado atual sem alterar a estrutura. {explanation}", $"result = readState({name})", "Avança para o step 2."),
            new(2, "Validação", "Verifique a condição necessária para a consulta, como existência de elementos ou da chave solicitada.", "validateReadableState()", "Avança para o step 3."),
            new(3, "Leitura", "Acesse somente as referências necessárias e não altere os ponteiros, o vetor ou Count.", "result = state.value", "Avança para o step 4."),
            new(4, "Resultado", "Retorne o valor ou a visão solicitada preservando exatamente o estado anterior.", "return result", "Conclui.")
        ],
        "Clear" => [
            new(1, "Estado atual", $"Identifique todo o armazenamento lógico que pertence à estrutura. {explanation}", "oldState = state", "Avança para o step 2."),
            new(2, "Limpeza", "Remova as referências ou valores que representam os elementos armazenados.", "clearStorage(state)", "Avança para o step 3."),
            new(3, "Contagem", "Zere Count e restaure as referências de extremidade para o estado vazio quando aplicável.", "count = 0; head = null; tail = null", "Conclui.")
        ],
        "EnsureCapacity" => [
            new(1, "Necessidade", "Compare a capacidade exigida com a capacidade física atual.", "if required <= capacity: return", "Se já houver espaço, conclui; caso contrário, avança para o step 2."),
            new(2, "Crescimento", "Aumente a capacidade progressivamente até comportar required, normalmente usando crescimento geométrico.", "while capacity < required: capacity *= 2", "Avança para o step 3."),
            new(3, "Cópia", "Aloque o novo armazenamento e copie somente os elementos lógicos existentes.", "items = resizeAndCopy(items, capacity)", "Conclui.")
        ],
        "CopyTo" => [
            new(1, "Destino", $"Valide o array de destino, o índice inicial e o espaço disponível. {explanation}", "validate(destination, arrayIndex)", "Avança para o step 2."),
            new(2, "Cópia", "Copie os elementos lógicos na mesma ordem, começando na posição de destino indicada.", "copy(items, 0, destination, arrayIndex, count)", "Avança para o step 3."),
            new(3, "Estado", "Não altere a coleção de origem; CopyTo produz apenas uma cópia externa.", "sourceState = unchanged", "Conclui.")
        ],
        "Remove" => [
            new(1, "Busca", $"Percorra a estrutura até localizar a primeira ocorrência do valor. {explanation}", "indexOrNode = find(item)", "Avança para o step 2 se encontrado; caso contrário, conclui com false."),
            new(2, "Remoção", "Desconecte o elemento e restaure as referências ou desloque o armazenamento conforme a estrutura.", "removeAt(indexOrNode)", "Avança para o step 3."),
            new(3, "Estado", "Atualize Count e retorne sucesso para indicar que a estrutura foi modificada.", "count--; return true", "Conclui.")
        ],
        "Enumerator" or "Enumerate" => [
            new(1, "Início", $"Defina o primeiro elemento da travessia. {explanation}", "current = first", "Avança para o step 2."),
            new(2, "Yield", "Produza o valor atual sem perder a referência necessária para continuar a travessia.", "yield current.Value", "Avança para o step 3."),
            new(3, "Avanço", "Siga a próxima referência ou índice e repita enquanto houver elementos.", "current = current.Next; repeat", "Retorna ao step 2 até atingir o fim ou o sentinela."),
            new(4, "Fim", "Encerre a enumeração sem modificar a estrutura.", "return", "Conclui.")
        ],
        "indexer" => [
            new(1, "Índice", $"Receba o índice solicitado. {explanation}", "index = requestedIndex", "Avança para o step 2."),
            new(2, "Validação", "Garanta que o índice pertença ao intervalo lógico atual.", "validate(index)", "Avança para o step 3."),
            new(3, "Acesso", "Leia ou substitua diretamente o elemento correspondente à posição.", "value = items[index]", "Conclui.")
        ],
        "IndexOf" or "Contains" => [
            new(1, "Cursor", $"Comece na primeira posição lógica e prepare a regra de igualdade. {explanation}", "current = first", "Avança para o step 2."),
            new(2, "Comparação", "Compare o valor atual com o alvo.", "if equals(current.Value, target): found", "Se não encontrar, avança para o step 3; se encontrar, conclui."),
            new(3, "Avanço", "Siga para o próximo elemento e repita até o fim.", "current = current.Next", "Retorna ao step 2."),
            new(4, "Ausência", "Se toda a estrutura foi percorrida sem igualdade, retorne o valor que representa ausência.", "return -1 // or false", "Conclui.")
        ],
        _ => [
            new(1, "Entrada", $"Receba os argumentos de {name} e identifique a parte da estrutura afetada. {explanation}", $"input = {name}(arguments)", "Avança para o step 2."),
            new(2, "Pré-condições", "Valide índices, referências e estado antes de executar a operação.", "validate(input); locate(state, input)", "Avança para o step 3."),
            new(3, "Operação", $"Execute {name} preservando as invariantes específicas da estrutura.", "state = operate(state, input)", "Avança para o step 4."),
            new(4, "Resultado", "Retorne o valor produzido ou confirme a conclusão sem deixar o estado inconsistente.", "return result", "Conclui.")
        ]
    };

    private static IReadOnlyList<FlowStepGuide> Heap(bool max) =>
    [
        new(1, max ? "Heap máximo" : "Heap mínimo", $"A raiz contém o {(max ? "maior" : "menor")} valor.", "heap[0] = root", "Avança para o step 2."),
        new(2, "Árvore completa", "O vetor representa níveis completos sem lacunas.", "left = 2*i + 1; right = 2*i + 2", "Avança para o step 3."),
        new(3, "Invariante", $"Cada pai deve ser {(max ? "maior ou igual" : "menor ou igual")} aos filhos; o heap não é totalmente ordenado.", "parent >= child // max; parent <= child // min", "Retorna ao step 2 durante ajustes."),
        new(4, "Ajuste", "Inserções sobem e remoções descem até restaurar a propriedade.", "siftUp(); siftDown()", "Retorna aos steps 2 e 3 enquanto houver violação.")
    ];

    public static IReadOnlyList<FlowStepGuide> ForAdvancedGraph() =>
    [
        new(1, "Grafo não ponderado", "Os vértices são identificados pelas chaves da adjacência e cada chave aponta para os destinos diretamente alcançáveis.", "adjacency[vertex] = neighbors", "Avança para o step 2."),
        new(2, "Grafo ponderado", "Na representação ponderada, cada relação acrescenta também um peso que será usado como custo pelos algoritmos de caminhos.", "adjacency[from].Add((to, weight))", "Avança para o step 3."),
        new(3, "Expansão", "BFS e DFS seguem as listas de vizinhos; Dijkstra, A* e Busca Gulosa também usam os vizinhos, mas avaliam custos ou heurísticas antes de escolher a próxima expansão.", "neighbors = adjacency[current]", "Retorna ao step 3 durante a busca até a condição de parada."),
        new(4, "Convergência", "O vértice 8 pode ser alcançado por caminhos diferentes. Um algoritmo precisa controlar visitados, custos ou predecessores para evitar processamento incorreto e reconstruir o resultado.", "visited.Add(v); predecessor[v] = current", "Conclui a representação.")
    ];

    private static string AlgorithmPseudocode(string key, int number) => key switch
    {
        "bubble-sort" => number switch
        {
            1 => "end = n - 1; changed = false",
            2 => "for i from 0 to end - 1: compare data[i] and data[i + 1]",
            3 => "if data[i] > data[i + 1]: swap(data[i], data[i + 1])",
            _ => "repeat while changed; end-- after each pass"
        },
        "selection-sort" => number switch
        {
            1 => "for position from 0 to n - 2",
            2 => "minimum = position; scan the remaining indexes",
            3 => "if data[index] < data[minimum]: minimum = index",
            _ => "swap(data[position], data[minimum]); position++"
        },
        "insertion-sort" => number switch
        {
            1 => "sorted region = data[0]",
            2 => "value = data[index]; position = index - 1",
            3 => "while position >= 0 and data[position] > value: data[position + 1] = data[position]",
            _ => "data[position + 1] = value; index++"
        },
        "merge-sort" => number switch
        {
            1 => "if low >= high: return; middle = (low + high) / 2",
            2 => "MergeSort(left); MergeSort(right)",
            3 => "compare leftValue and rightValue",
            _ => "copy smaller; consume remaining side; copy temporary range back"
        },
        "quick-sort" => number switch
        {
            1 => "pivot = values[high]",
            2 => "scan from low to high - 1",
            3 => "if values[index] <= pivot: swap(values[smaller], values[index]); smaller++",
            _ => "swap pivot into smaller; QuickSort(left); QuickSort(right)"
        },
        "heap-sort" => number switch
        {
            1 => "buildMaxHeap(values)",
            2 => "swap(values[0], values[end])",
            3 => "heapLength = end; siftDown(0, heapLength)",
            _ => "repeat until end == 0"
        },
        "linear-search" => number switch
        {
            1 => "cursor = first element of traversal",
            2 => "if current == target: return current position",
            3 => "advance cursor to next element",
            _ => "return -1 when traversal is exhausted"
        },
        "binary-search" => number switch
        {
            1 => "low = 0; high = n - 1",
            2 => "middle = low + (high - low) / 2",
            3 => "if data[middle] == target: return middle",
            4 => "if data[middle] < target: low = middle + 1; else high = middle - 1",
            _ => "return -1"
        },
        "jump-search" => number switch
        {
            1 => "step = floor(sqrt(n)); previous = 0",
            2 => "while data[min(next, n) - 1] < target: previous = next; next += step",
            3 => "scan from previous to min(next, n) - 1",
            _ => "return index when found; otherwise return -1"
        },
        "interpolation-search" => number switch
        {
            1 => "low = 0; high = n - 1; validate target is within endpoint values",
            2 => "position = low + ((target - data[low]) * (high - low)) / (data[high] - data[low])",
            3 => "compare data[position] with target",
            4 => "if smaller: low = position + 1; otherwise high = position - 1",
            _ => "return position when equal; otherwise -1"
        },
        "hash-search" => number switch
        {
            1 => "hash = key.GetHashCode()",
            2 => "bucket = normalize(hash) % capacity",
            3 => "for entry in bucket: compare entry.Key with key",
            _ => "return entry.Value when equal; otherwise not found"
        },
        "breadth-first-search" => number switch
        {
            1 => "visited = { start }; queue.Enqueue(start)",
            2 => "current = queue.Dequeue(); process(current)",
            3 => "for neighbor in adjacency[current]: if visited.Add(neighbor) ...",
            4 => "queue.Enqueue(neighbor); repeat while queue is not empty",
            _ => "finish when the queue is empty"
        },
        "depth-first-search" => number switch
        {
            1 => "if !visited.Add(current): return",
            2 => "process(current)",
            3 => "for neighbor in adjacency[current]: DepthFirstSearch(neighbor)",
            4 => "return to caller after all neighbors are processed",
            _ => "finish when the root call returns"
        },
        "dijkstra" => number switch
        {
            1 => "distance[start] = 0; all other distances = infinity",
            2 => "current = priorityQueue.extractMin()",
            3 => "candidate = distance[current] + edge.weight",
            4 => "if candidate < distance[next]: update distance and enqueue",
            _ => "repeat until the priority queue has no reachable candidates"
        },
        "a-star" => number switch
        {
            1 => "openSet.Add(start); g[start] = 0",
            2 => "f[node] = g[node] + heuristic(node, goal)",
            3 => "current = openSet.extractMinBy(f)",
            4 => "candidateG = g[current] + edge.weight; update when candidateG is smaller",
            _ => "reconstruct predecessors when current == goal"
        },
        "greedy-best-first-search" => number switch
        {
            1 => "openSet.Enqueue(start, heuristic(start, goal))",
            2 => "priority = heuristic(node, goal)",
            3 => "current = openSet.extractMinBy(priority)",
            4 => "visited.Add(current); enqueue each unvisited neighbor by heuristic",
            _ => "return predecessor path when goal is extracted"
        },
        "similarity-search" => number switch
        {
            1 => "queryVector = represent(query); candidateVector = represent(candidate)",
            2 => "score = similarity(queryVector, candidateVector)",
            3 => "results.Add(candidate, score)",
            4 => "sort results by score descending",
            _ => "return ranked candidates"
        },
        _ => $"execute step {number} according to the algorithm definition"
    };

    private static string AlgorithmElement(string key, int number) => key switch
    {
        "linear-search" => number switch { 1 => "Cursor", 2 => "Comparação", 3 => "Resultado", _ => "Percurso" },
        "binary-search" => number switch { 1 => "Intervalo", 2 => "Meio", 3 => "Comparação", 4 => "Redução", _ => "Resultado" },
        "jump-search" => number switch { 1 => "Bloco", 2 => "Salto", 3 => "Busca linear", _ => "Resultado" },
        "interpolation-search" => number switch { 1 => "Faixa", 2 => "Estimativa", 3 => "Comparação", 4 => "Redução", _ => "Resultado" },
        "hash-search" => number switch { 1 => "Hash", 2 => "Bucket", 3 => "Colisão", _ => "Resultado" },
        "breadth-first-search" => number switch { 1 => "Fila", 2 => "Vértice", 3 => "Vizinhos", 4 => "Descoberta", _ => "Conclusão" },
        "depth-first-search" => number switch { 1 => "Entrada", 2 => "Visitado", 3 => "Recursão", 4 => "Backtrack", _ => "Conclusão" },
        "dijkstra" => number switch { 1 => "Distância", 2 => "Mínimo", 3 => "Relaxamento", 4 => "Atualização", _ => "Conclusão" },
        "a-star" => number switch { 1 => "Origem", 2 => "Função f", 3 => "Expansão", 4 => "Relaxamento", _ => "Caminho" },
        "greedy-best-first-search" => number switch { 1 => "Origem", 2 => "Heurística", 3 => "Expansão", 4 => "Vizinhos", _ => "Caminho" },
        "similarity-search" => number switch { 1 => "Representação", 2 => "Similaridade", 3 => "Pontuação", 4 => "Ordenação", _ => "Resultado" },
        _ => $"Etapa {number}"
    };
}