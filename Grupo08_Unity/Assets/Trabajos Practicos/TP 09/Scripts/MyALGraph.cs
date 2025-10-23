using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyALGraph<T>
{
    private readonly Dictionary<T, List<(T to, int weight)>> _adj = new Dictionary<T, List<(T to, int weight)>>();

    public IEnumerable<T> Vertices =>_adj.Keys;

    public void AddVertex(T vertex)
    {
        if (!_adj.ContainsKey(vertex))_adj[vertex] = new List<(T to, int weight)>();
    }

    public void RemoveVertex(T vertex) 
    {

        if (!_adj.ContainsKey(vertex)) return;

        _adj.Remove(vertex);

        foreach (var key in _adj.Keys.ToList())
        {
            _adj[key].RemoveAll(edge => EqualityComparer<T>.Default.Equals(edge.to, vertex));
        }
    }

    public void AddEdge (T from, (T to, int weight) edge)
    {
        AddVertex (from);
        AddVertex(edge.to);

        var list = _adj[from];

        int idx = list.FindIndex(e => EqualityComparer<T>.Default.Equals(e.to, edge.to));

        if (idx >= 0) list[idx] = (edge.to, edge.weight);

        else list.Add((edge.to, edge.weight));
    }

    public void RemoveEdge (T from, T to)
    {
        if (!_adj.ContainsKey(from)) return;
        _adj[from].RemoveAll(e => EqualityComparer<T>.Default.Equals(e.to, to));
    }

    public bool ContainsVertex(T vertex) => _adj.ContainsKey(vertex);

    public bool ContainsEdge(T from, T to)
    {
        if (!_adj.ContainsKey(from)) return false;

        return _adj[from].Any(e => EqualityComparer<T>.Default.Equals (e.to, to));
    }

    public int? GetWeight(T from, T to)
    {
        if (!_adj.ContainsKey(from)) return null;

        var edge = _adj[from].FirstOrDefault(e => EqualityComparer<T>.Default.Equals(e.to, to));

        bool exists = _adj[from].Any( e => EqualityComparer<T>.Default.Equals(e.to, to)); 
        return exists ? edge.weight : null;
    }

}
