using DataStructure.Abstractions;
using DataStructure.Algorithms;

namespace DataStructure.github.io.Services;

public sealed record AlgorithmTreeNode<T>(
    T Value,
    IReadOnlyList<AlgorithmTreeNode<T>> Children);

public sealed record AdvancedLabResult(
    string Algorithm,
    string Description,
    IReadOnlyList<string> Steps,
    string Result,
    TimeSpan Elapsed,
    AlgorithmTreeNode<int>? Tree = null,
    IReadOnlyList<int>? CurrentItems = null);

public sealed class AdvancedAlgorithmLabService
{
    public IReadOnlyList<string> Algorithms =>
        ["HashSearch", "BreadthFirstSearch", "DepthFirstSearch", "Dijkstra", "AStar", "GreedyBestFirstSearch", "SimilaritySearch"];

    public AdvancedLabResult Run(string algorithm)
        => algorithm switch
        {
            "HashSearch" => RunHashSearch(),
            "BreadthFirstSearch" => RunBreadthFirst(),
            "DepthFirstSearch" => RunDepthFirst(),
            "Dijkstra" => RunDijkstra(),
            "AStar" => RunAStar(),
            "GreedyBestFirstSearch" => RunGreedy(),
            "SimilaritySearch" => RunSimilarity(),
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm))
        };

    private static AdvancedLabResult RunHashSearch()
    {
        var table = new DSHashTable<int, string>();
        table.Add(10, "A");
        table.Add(18, "B");
        table.Add(26, "C");
        table.Add(42, "D");

        var algorithm = new HashSearch();
        var result = algorithm.Execute(table, 26);

        return new(
            "HashSearch",
            "Consulta a chave 26 na tabela de dispersão.",
            [
                "Calcula o hash da chave.",
                "Seleciona o bucket correspondente.",
                "Percorre a cadeia de colisões, se necessário.",
                "Compara as chaves até encontrar 26."
            ],
            result.Value ? "Chave 26 encontrada." : "Chave 26 não encontrada.",
            result.Elapsed,
            CreateTraversalTree(),
            result.Value);
    }

    private static AdvancedLabResult RunBreadthFirst()
    {
        var graph = CreateGraph();
        var result = new BreadthFirstSearch().Execute(graph, 1);

        return new(
            "BreadthFirstSearch",
            "Percorre o grafo a partir do vértice 1 por níveis.",
            result.Value.Select((value, index) => $"{index + 1}. visita {value}").ToArray(),
            $"Ordem: {string.Join(" → ", result.Value)}",
            result.Elapsed,
            CreateTraversalTree(),
            result.Value);
    }

    private static AdvancedLabResult RunDepthFirst()
    {
        var graph = CreateGraph();
        var result = new DepthFirstSearch().Execute(graph, 1);

        return new(
            "DepthFirstSearch",
            "Explora cada ramo do grafo a partir do vértice 1 antes de retornar.",
            result.Value.Select((value, index) => $"{index + 1}. visita {value}").ToArray(),
            $"Ordem: {string.Join(" → ", result.Value)}",
            result.Elapsed,
            CreateWeightedTree(),
            [1, 2, 5, 3, 6, 7, 8, 4]);
    }

    private static AdvancedLabResult RunDijkstra()
    {
        var graph = CreateWeightedGraph();
        var result = new Dijkstra().Execute(graph, 1);

        return new(
            "Dijkstra",
            "Calcula o menor custo da origem 1 para todos os vértices.",
            result.Value
                .OrderBy(item => item.Key)
                .Select(item => $"{item.Key}: {FormatDistance(item.Value)}")
                .ToArray(),
            string.Join(" | ", result.Value.OrderBy(item => item.Key).Select(item => $"{item.Key}={FormatDistance(item.Value)}")),
            result.Elapsed,
            CreateWeightedTree(),
            result.Value);
    }

    private static AdvancedLabResult RunAStar()
    {
        var graph = CreateWeightedGraph();
        var coordinates = Coordinates;
        var result = new AStar().Execute(graph, 1, 8, (from, to) => Distance(coordinates[from], coordinates[to]));

        return new(
            "AStar",
            "Procura um caminho de 1 até 8 usando distância euclidiana como heurística.",
            result.Value.Select((value, index) => $"{index + 1}. {value}").ToArray(),
            $"Caminho: {string.Join(" → ", result.Value)}",
            result.Elapsed,
            CreateWeightedTree(),
            result.Value);
    }

    private static AdvancedLabResult RunGreedy()
    {
        var graph = CreateWeightedGraph();
        var coordinates = Coordinates;
        var result = new GreedyBestFirstSearch().Execute(graph, 1, 8, (from, to) => Distance(coordinates[from], coordinates[to]));

        return new(
            "GreedyBestFirstSearch",
            "Prioriza somente a distância estimada até o objetivo 8.",
            result.Value.Select((value, index) => $"{index + 1}. {value}").ToArray(),
            $"Caminho encontrado: {string.Join(" → ", result.Value)}",
            result.Elapsed);
    }

    private static AdvancedLabResult RunSimilarity()
    {
        var candidates = new[]
        {
            "estrutura de dados e algoritmos",
            "algoritmos de busca em grafos",
            "estrutura de dados para aplicações",
            "receita de bolo de chocolate"
        };

        var result = new SimilaritySearch().ExecuteText("algoritmos e estruturas de dados", candidates);

        return new(
            "SimilaritySearch",
            "Compara a consulta com quatro textos usando similaridade de Jaccard sobre tokens.",
            result.Select(item => $"{item.Score:P1} — {item.Value}").ToArray(),
            $"Mais semelhante: {result[0].Value}",
            TimeSpan.Zero);
    }

    private static AlgorithmTreeNode<int> CreateTraversalTree()
        => new(1,
        [
            new(2,
            [
                new(4, []),
                new(5, [new(8, [])])
            ]),
            new(3,
            [
                new(6, [new(8, [])]),
                new(7, [])
            ])
        ]);

    private static AlgorithmTreeNode<int> CreateWeightedTree()
        => new(1,
        [
            new(2,
            [
                new(4, [new(7, [new(8, [])])]),
                new(5, [new(7, [new(8, [])])])
            ]),
            new(3,
            [
                new(5, [new(6, [new(8, [])])]),
                new(6, [new(8, [])])
            ])
        ]);

    private static DSGraph<int> CreateGraph()
    {
        var graph = new DSGraph<int>();
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(2, 5);
        graph.AddEdge(3, 6);
        graph.AddEdge(3, 7);
        graph.AddEdge(5, 8);
        graph.AddEdge(6, 8);
        return graph;
    }

    private static DSWeightedGraph<int> CreateWeightedGraph()
    {
        var graph = new DSWeightedGraph<int>();
        graph.AddEdge(1, 2, 2);
        graph.AddEdge(1, 3, 5);
        graph.AddEdge(2, 4, 2);
        graph.AddEdge(2, 5, 3);
        graph.AddEdge(3, 5, 1);
        graph.AddEdge(3, 6, 2);
        graph.AddEdge(4, 7, 4);
        graph.AddEdge(5, 7, 2);
        graph.AddEdge(6, 8, 3);
        graph.AddEdge(7, 8, 1);
        graph.AddEdge(5, 6, 2);
        return graph;
    }

    private static IReadOnlyDictionary<int, (double X, double Y)> Coordinates { get; } =
        new Dictionary<int, (double X, double Y)>
        {
            [1] = (0, 0),
            [2] = (1, 1),
            [3] = (1, -1),
            [4] = (2, 2),
            [5] = (2, 0),
            [6] = (2, -2),
            [7] = (3, 1),
            [8] = (4, 0)
        };

    private static double Distance((double X, double Y) left, (double X, double Y) right)
        => Math.Sqrt(Math.Pow(left.X - right.X, 2) + Math.Pow(left.Y - right.Y, 2));

    private static string FormatDistance(double value)
        => double.IsPositiveInfinity(value) ? "∞" : value.ToString("0.##");
}
