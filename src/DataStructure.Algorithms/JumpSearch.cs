using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>Jump search on sorted indexed data. Average and worst time O(sqrt(n)); space O(1).</summary>
public sealed class JumpSearch
{
 public AlgorithmResult<int> ExecuteDSArray(DSArray<int>d,int target)=>Run(d.Count,i=>d[i],target);
 public AlgorithmResult<int> ExecuteDSList(DSList<int>d,int target)=>Run(d.Count,i=>d[i],target);
 public AlgorithmResult<int> ExecuteDSLinkedList(DSLinkedList<int>d,int target)=>Run(d.Count,i=>d[i],target);
 public AlgorithmResult<int> ExecuteDSCollection(DSCollection<int>d,int target)=>Run(d.Count,i=>d[i],target);

 private static AlgorithmResult<int> Run(int n,Func<int,int>get,int target){var s=System.Diagnostics.Stopwatch.StartNew();if(n==0)return new(-1,s.Elapsed);var step=Math.Max(1,(int)Math.Sqrt(n));var prev=0;var next=step;while(prev<n&&get(Math.Min(next,n)-1)<target){prev=next;next+=step;if(prev>=n)return new(-1,s.Elapsed);}for(var i=prev;i<Math.Min(next,n);i++){var x=get(i);if(x==target)return new(i,s.Elapsed);if(x>target)break;}return new(-1,s.Elapsed);}
}