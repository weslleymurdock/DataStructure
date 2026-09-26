using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>Insertion sort. Time O(n²) average/worst and O(n) best; space O(1) for indexed structures.</summary>
public sealed class InsertionSort
{
 public AlgorithmResult<int> ExecuteDSArray(DSArray<int> d)=>Sort(d.Count,i=>d[i],(i,v)=>d[i]=v);
 public AlgorithmResult<int> ExecuteDSList(DSList<int> d)=>Sort(d.Count,i=>d[i],(i,v)=>d[i]=v);
 public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int> d)=>SortCopy(d.ToArray(),d);
 public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int> d)=>SortCopy(d.ToArray(),d);
 public AlgorithmResult<int> ExecuteDSQueue(DSQueue<int> d)=>SortQueue(d);
 public AlgorithmResult<int> ExecuteDSStack(DSStack<int> d)=>SortStack(d);
 public AlgorithmResult<int> ExecuteDSDeque(DSDeque<int> d)=>SortDeque(d);

 private static AlgorithmResult<int> Sort(int n,Func<int,int> get,Action<int,int> set){var s=System.Diagnostics.Stopwatch.StartNew();for(var i=1;i<n;i++){var value=get(i);var j=i-1;while(j>=0&&get(j)>value){set(j+1,get(j));j--;}set(j+1,value);}return new(n,s.Elapsed);}
 private static AlgorithmResult<int> SortCopy(int[] a,object _){var s=System.Diagnostics.Stopwatch.StartNew();for(var i=1;i<a.Length;i++){var value=a[i];var j=i-1;while(j>=0&&a[j]>value){a[j+1]=a[j];j--;}a[j+1]=value;}return new(a.Length,s.Elapsed);}
 private static AlgorithmResult<int> SortQueue(DSQueue<int>d){var a=new List<int>();while(d.Count>0)a.Add(d.Dequeue());var r=SortCopy(a.ToArray(),d);foreach(var x in a.Order())d.Enqueue(x);return r;}
 private static AlgorithmResult<int> SortStack(DSStack<int>d){var a=new List<int>();while(d.Count>0)a.Add(d.Pop());var r=SortCopy(a.ToArray(),d);foreach(var x in a.OrderDescending())d.Push(x);return r;}
 private static AlgorithmResult<int> SortDeque(DSDeque<int>d){var a=new List<int>();while(d.Count>0)a.Add(d.RemoveFirst());var r=SortCopy(a.ToArray(),d);foreach(var x in a.Order())d.AddLast(x);return r;}
}