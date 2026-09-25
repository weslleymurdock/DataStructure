using DataStructure.Abstractions;
namespace DataStructure.Algorithms;

/// <summary>
/// Linear search inspects elements sequentially until the target is found.
/// Time: O(n) in the worst case; space: O(1), excluding temporary restoration buffers.
/// </summary>
public sealed class LinearSearch
{
    public AlgorithmResult<int> ExecuteDSArray<T>(DSArray<T> data,T target)=>Run(data,target);
    public AlgorithmResult<int> ExecuteDSList<T>(DSList<T> data,T target)=>Run(data,target);
    public AlgorithmResult<int> ExecuteDSLinkedList<T>(DSLinkedList<T> data,T target)=>Run(data,target);
    public AlgorithmResult<int> ExecuteDSCollection<T>(DSCollection<T> data,T target)=>Run(data,target);
    public AlgorithmResult<int> ExecuteDSQueue<T>(DSQueue<T> data,T target){var sw=System.Diagnostics.Stopwatch.StartNew();var n=data.Count;var found=-1;for(var i=0;i<n;i++){var item=data.Dequeue();if(found<0&&EqualityComparer<T>.Default.Equals(item,target))found=i;data.Enqueue(item);}return new(found,sw.Elapsed);}
    public AlgorithmResult<int> ExecuteDSStack<T>(DSStack<T> data,T target){var sw=System.Diagnostics.Stopwatch.StartNew();var buffer=new DSStack<T>();var found=-1;var i=0;while(data.Count>0){var item=data.Pop();if(found<0&&EqualityComparer<T>.Default.Equals(item,target))found=i;buffer.Push(item);i++;}while(buffer.Count>0)data.Push(buffer.Pop());return new(found,sw.Elapsed);}
    public AlgorithmResult<int> ExecuteDSDeque<T>(DSDeque<T> data,T target){var sw=System.Diagnostics.Stopwatch.StartNew();var n=data.Count;var found=-1;for(var i=0;i<n;i++){var item=data.RemoveFirst();if(found<0&&EqualityComparer<T>.Default.Equals(item,target))found=i;data.AddLast(item);}return new(found,sw.Elapsed);}
    public AlgorithmResult<int> ExecuteDSPriorityQueue<T>(DSPriorityQueue<T> data,T target) where T:IComparable<T>{var sw=System.Diagnostics.Stopwatch.StartNew();var buffer=new List<T>();var found=-1;var i=0;while(data.Count>0){var item=data.Dequeue();if(found<0&&item.CompareTo(target)==0)found=i;buffer.Add(item);i++;}foreach(var item in buffer)data.Enqueue(item);return new(found,sw.Elapsed);}
    private static AlgorithmResult<int> Run<T>(IEnumerable<T> data,T target){var sw=System.Diagnostics.Stopwatch.StartNew();var i=0;foreach(var item in data){if(EqualityComparer<T>.Default.Equals(item,target))return new(i,sw.Elapsed);i++;}return new(-1,sw.Elapsed);}
}
