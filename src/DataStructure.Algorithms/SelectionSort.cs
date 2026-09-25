using DataStructure.Abstractions;
namespace DataStructure.Algorithms;
/// <summary>Selection sort. Time O(n²) in best, average and worst cases.</summary>
public sealed class SelectionSort
{
 public AlgorithmResult<int> ExecuteDSArray(DSArray<int>d)=>Sort(d.Count,i=>d[i],(i,v)=>d[i]=v);
 public AlgorithmResult<int> ExecuteDSList(DSList<int>d)=>Sort(d.Count,i=>d[i],(i,v)=>d[i]=v);
 public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int>d)=>SortCopy(d.ToArray());
 public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int>d)=>SortCopy(d.ToArray());
 public AlgorithmResult<int> ExecuteDSQueue(DSQueue<int>d)=>SortQueue(d);
 public AlgorithmResult<int> ExecuteDSStack(DSStack<int>d)=>SortStack(d);
 public AlgorithmResult<int> ExecuteDSDeque(DSDeque<int>d)=>SortDeque(d);
 private static AlgorithmResult<int> Sort(int n,Func<int,int>g,Action<int,int>set){var s=System.Diagnostics.Stopwatch.StartNew();for(var i=0;i<n-1;i++){var m=i;for(var j=i+1;j<n;j++)if(g(j)<g(m))m=j;if(m!=i){var x=g(i);set(i,g(m));set(m,x);}}return new(n,s.Elapsed);}
 private static AlgorithmResult<int> SortCopy(int[]a){var s=System.Diagnostics.Stopwatch.StartNew();for(var i=0;i<a.Length-1;i++){var m=i;for(var j=i+1;j<a.Length;j++)if(a[j]<a[m])m=j;if(m!=i)(a[i],a[m])=(a[m],a[i]);}return new(a.Length,s.Elapsed);}
 private static AlgorithmResult<int> SortQueue(DSQueue<int>d){var a=new List<int>();while(d.Count>0)a.Add(d.Dequeue());var r=SortCopy(a.ToArray());a.Sort();foreach(var x in a)d.Enqueue(x);return new(r.Value,r.Elapsed);}
 private static AlgorithmResult<int> SortStack(DSStack<int>d){var s=System.Diagnostics.Stopwatch.StartNew();var a=new List<int>();while(d.Count>0)a.Add(d.Pop());for(var i=0;i<a.Count-1;i++){var m=i;for(var j=i+1;j<a.Count;j++)if(a[j]<a[m])m=j;if(m!=i)(a[i],a[m])=(a[m],a[i]);}for(var i=a.Count-1;i>=0;i--)d.Push(a[i]);return new(a.Count,s.Elapsed);}
 private static AlgorithmResult<int> SortDeque(DSDeque<int>d){var a=new List<int>();while(d.Count>0)a.Add(d.RemoveFirst());var r=SortCopy(a.ToArray());a.Sort();foreach(var x in a)d.AddLast(x);return new(r.Value,r.Elapsed);}
}