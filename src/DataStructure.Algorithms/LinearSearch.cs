using DataStructure.Abstractions;
namespace DataStructure.Algorithms;
/// <summary>Linear search. Time O(n) worst case; space O(1), excluding restoration buffers.</summary>
public sealed class LinearSearch
{
 public AlgorithmResult<int> ExecuteDSArray<T>(DSArray<T>d,T t)=>Run(d,t);
 public AlgorithmResult<int> ExecuteDSList<T>(DSList<T>d,T t)=>Run(d,t);
 public AlgorithmResult<int> ExecuteDSLinkedList<T>(DSLinkedList<T>d,T t)=>Run(d,t);
 public AlgorithmResult<int> ExecuteDSCollection<T>(DSCollection<T>d,T t)=>Run(d,t);
 public AlgorithmResult<int> ExecuteDSQueue<T>(DSQueue<T>d,T t){var s=System.Diagnostics.Stopwatch.StartNew();var n=d.Count;var f=-1;for(var i=0;i<n;i++){var x=d.Dequeue();if(f<0&&EqualityComparer<T>.Default.Equals(x,t))f=i;d.Enqueue(x);}return new(f,s.Elapsed);}
 public AlgorithmResult<int> ExecuteDSStack<T>(DSStack<T>d,T t){var s=System.Diagnostics.Stopwatch.StartNew();var b=new DSStack<T>();var f=-1;var i=0;while(d.Count>0){var x=d.Pop();if(f<0&&EqualityComparer<T>.Default.Equals(x,t))f=i;b.Push(x);i++;}while(b.Count>0)d.Push(b.Pop());return new(f,s.Elapsed);}
 public AlgorithmResult<int> ExecuteDSDeque<T>(DSDeque<T>d,T t){var s=System.Diagnostics.Stopwatch.StartNew();var n=d.Count;var f=-1;for(var i=0;i<n;i++){var x=d.RemoveFirst();if(f<0&&EqualityComparer<T>.Default.Equals(x,t))f=i;d.AddLast(x);}return new(f,s.Elapsed);}
 public AlgorithmResult<int> ExecuteDSPriorityQueue<T>(DSPriorityQueue<T>d,T t) where T:IComparable<T>{var s=System.Diagnostics.Stopwatch.StartNew();var b=new List<T>();var f=-1;var i=0;while(d.Count>0){var x=d.Dequeue();if(f<0&&x.CompareTo(t)==0)f=i;b.Add(x);i++;}foreach(var x in b)d.Enqueue(x);return new(f,s.Elapsed);}
 private static AlgorithmResult<int> Run<T>(IReadOnlyList<T>d,T t){var s=System.Diagnostics.Stopwatch.StartNew();for(var i=0;i<d.Count;i++)if(EqualityComparer<T>.Default.Equals(d[i],t))return new(i,s.Elapsed);return new(-1,s.Elapsed);}
}