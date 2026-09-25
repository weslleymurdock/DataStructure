using DataStructure.Abstractions;
namespace DataStructure.Algorithms;
/// <summary>Binary search on sorted indexed data. Time O(log n) when indexed access is O(1); space O(1).</summary>
public sealed class BinarySearch
{
 public AlgorithmResult<int> ExecuteDSArray(DSArray<int>d,int t)=>Run(d.Count,i=>d[i],t);
 public AlgorithmResult<int> ExecuteDSList(DSList<int>d,int t)=>Run(d.Count,i=>d[i],t);
 public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int>d,int t)=>Run(d.Count,i=>d[i],t);
 public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int>d,int t)=>Run(d.Count,i=>d[i],t);
 private static AlgorithmResult<int> Run(int n,Func<int,int>get,int t){var s=System.Diagnostics.Stopwatch.StartNew();var lo=0;var hi=n-1;while(lo<=hi){var m=lo+(hi-lo)/2;var x=get(m);if(x==t)return new(m,s.Elapsed);if(x<t)lo=m+1;else hi=m-1;}return new(-1,s.Elapsed);}
}