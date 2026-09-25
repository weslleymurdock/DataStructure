Console.WriteLine("=== 1. ARRAY: Estações do Ano ===");
DemonstrarArray();

Console.WriteLine("\n=== 2. LISTA: Carrinho de Compras ===");
DemonstrarLista();

Console.WriteLine("\n=== 3. QUEUE (FILA): Sistema de Atendimento ===");
DemonstrarQueue();

Console.WriteLine("\n=== 4. DEQUE: Histórico de Navegação e VIPs ===");
DemonstrarDeque();

///<summary> Caso de Uso: Dados de tamanho conhecido e imutável que precisam de acesso super rápido.</summary>
static void DemonstrarArray()
{
    string[] estacoes = ["Primavera", "Verão", "Outono", "Inverno"] ;
    
    // Acesso direto via índice é instantâneo
    Console.WriteLine($"A segunda estação é: {estacoes[1]}");
    
    // Arrays são ótimos para iteração rápida de dados fixos
    foreach (var estacao in estacoes)
    {
        Console.WriteLine($"- {estacao}");
    }
}

static void DemonstrarLista()
{
    // Caso de Uso: Uma coleção que cresce e encolhe. O tamanho inicial não importa.
    List<string> carrinho = new List<string>();
    
    carrinho.Add("Notebook");
    carrinho.Add("Mouse");
    carrinho.Add("Teclado");

    Console.WriteLine($"Itens no carrinho: {carrinho.Count}");
    
    // Remoção pelo valor (dinamismo puro)
    carrinho.Remove("Mouse"); 
    Console.WriteLine("Mouse removido. Sobraram:");
    
    foreach (var item in carrinho)
    {
        Console.WriteLine($"- {item}");
    }
}

static void DemonstrarQueue()
{
    // Caso de Uso: Processamento na ordem exata de chegada (FIFO).
    Queue<string> filaAtendimento = new Queue<string>();
    
    filaAtendimento.Enqueue("Cliente A (Chegou às 10:00)");
    filaAtendimento.Enqueue("Cliente B (Chegou às 10:05)");
    filaAtendimento.Enqueue("Cliente C (Chegou às 10:10)");

    Console.WriteLine($"Pessoas aguardando: {filaAtendimento.Count}");

    // O primeiro a entrar DEVE ser o primeiro a ser atendido
    while (filaAtendimento.Count > 0)
    {
        string proximo = filaAtendimento.Dequeue();
        Console.WriteLine($"Atendendo: {proximo}");
    }
}

static void DemonstrarDeque()
{
    // Caso de Uso: Fila dupla. Vamos simular uma fila de tarefas onde tarefas normais 
    // vão para o final, mas tarefas URGENTES furam a fila e vão para o início.
    // No C#, o LinkedList<T> é a ferramenta padrão para agir como um Deque.
    LinkedList<string> dequeTarefas = new LinkedList<string>();

    // Inserções padrão (como numa Queue normal)
    dequeTarefas.AddLast("Tarefa Normal 1 (Enviar métricas)");
    dequeTarefas.AddLast("Tarefa Normal 2 (Limpar logs)");

    // Ocorre uma emergência! Precisamos inserir na frente (comportamento exclusivo do Deque/Stack)
    dequeTarefas.AddFirst("TAREFA CRÍTICA (Reiniciar Servidor de Banco de Dados)");

    // Outra emergência!
    dequeTarefas.AddFirst("TAREFA SUPER CRÍTICA (Bloquear IP atacante)");

    Console.WriteLine("Ordem de execução das tarefas no Deque:");
    while (dequeTarefas.Count > 0)
    {
        // Removemos e processamos da frente
        string tarefaAtual = dequeTarefas.First.Value;
        dequeTarefas.RemoveFirst();
        
        Console.WriteLine($"[Processando] {tarefaAtual}");
    }
}