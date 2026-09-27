using DataStructure.Abstractions;

namespace DataStructure.Algorithms;

/// <summary>
/// Finds a graph path by prioritizing the vertex with the smallest heuristic.
/// </summary>
/// <remarks>
/// Unlike Dijkstra and A*, this algorithm does not include the accumulated path cost
/// in its priority. It is therefore not guaranteed to find a shortest path.
