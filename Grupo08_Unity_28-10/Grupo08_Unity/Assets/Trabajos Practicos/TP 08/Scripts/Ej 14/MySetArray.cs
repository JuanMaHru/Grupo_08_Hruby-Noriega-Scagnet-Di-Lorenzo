using System;
using System.Collections.Generic;

public class MySetArray<T> : MySet<T>
{
    private List<T> elements = new List<T>();

    public override void Add(T item)
    {
        if (!Contains(item))
            elements.Add(item);
    }

    public override void Remove(T item)
    {
        elements.Remove(item);
    }

    public override void Clear()
    {
        elements.Clear();
    }

    public override bool Contains(T item)
    {
        return elements.Contains(item);
    }

    public override void Show()
    {
        foreach (var item in elements)
            Console.WriteLine(item);
    }

    public override string ToString()
    {
        return string.Join(", ", elements);
    }

    public override int Cardinality()
    {
        return elements.Count;
    }

    public override bool IsEmpty()
    {
        return elements.Count == 0;
    }

    public override MySet<T> Union(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        foreach (var item in this.GetElements())
            result.Add(item);

        foreach (var item in other.GetElements())
            result.Add(item);

        return result;
    }

    public override MySet<T> Intersect(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        foreach (var item in this.GetElements())
            if (other.Contains(item))
                result.Add(item);

        return result;
    }

    public override MySet<T> Difference(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        foreach (var item in this.GetElements())
            if (!other.Contains(item))
                result.Add(item);

        return result;
    }

    public override IEnumerable<T> GetElements()
    {
        foreach (var item in elements)
            yield return item;
    }
}
