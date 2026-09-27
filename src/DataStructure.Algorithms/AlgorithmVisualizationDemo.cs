using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Alternative algorithm demonstration that animates sorting algorithms in the terminal.
/// </summary>
public static class AlgorithmVisualizationDemo
{
    /// <summary>
    /// Runs the sorting visualization with the supplied frame callback.
    /// </summary>
    /// <param name="render">
    /// Callback invoked after each meaningful mutation of the sorting sequence.
    /// </param>
    public static void Run(Action<Visualization.Console> render)
    {
        ArgumentNullException.ThrowIfNull(render);

        // A permutation containing every height from 1 to N makes each
        // movement visible because no two bars have the same height.
        var values = CreateVisualizationValues();

        RunAlgorithm("BubbleSort", values, render, callback =>
        {
            var algorithm = new BubbleSort(callback);
            algorithm.ExecuteDSArray(new DSArray<int>(values));
        });

        RunAlgorithm("SelectionSort", values, render, callback =>
        {
            var algorithm = new SelectionSort(callback);
            algorithm.ExecuteDSArray(new DSArray<int>(values));
        });

        RunAlgorithm("InsertionSort", values, render, callback =>
        {
            var algorithm = new InsertionSort(callback);
            algorithm.ExecuteDSArray(new DSArray<int>(values));
        });

        RunAlgorithm("MergeSort", values, render, callback =>
        {
            var algorithm = new MergeSort(callback);
            algorithm.ExecuteDSArray(new DSArray<int>(values));
        });

        RunAlgorithm("QuickSort", values, render, callback =>
        {
            var algorithm = new QuickSort(callback);
            algorithm.ExecuteDSArray(new DSArray<int>(values));
        });

        RunAlgorithm("HeapSort", values, render, callback =>
        {
            var algorithm = new HeapSort(callback);
            algorithm.ExecuteDSArray(new DSArray<int>(values));
        });
    }

    /// <summary>Creates the deterministic permutation used by every animation.</summary>
    private static int[] CreateVisualizationValues()
    {
        return
        [
            8, 3, 12, 5, 1, 10, 7,
            14, 4, 11, 2, 13, 6, 9
        ];
    }

    /// <summary>
    /// Executes one algorithm from a fresh copy and converts every mutation
    /// into a visualization frame.
    /// </summary>
    private static void RunAlgorithm(
        string name,
        IReadOnlyList<int> values,
        Action<Visualization.Console> render,
        Action<Action<IReadOnlyList<int>>> execute)
    {
        var snapshot = values.ToArray();
        var step = 0;

        render(new Visualization.Console(name, snapshot, step));

        execute(current =>
        {
            step++;
            render(new Visualization.Console(name, current, step));
        });

        // The algorithm itself has already produced the sorted sequence.
        // This final frame explicitly shows the expected terminal state.
        render(new Visualization.Console(name, snapshot.Order().ToArray(), ++step));

        System.Threading.Thread.Sleep(250);
    }
}
