using System;
using System.Collections.Generic;

public abstract class MySet<T>
{
    public abstract void Add(T item);
    public abstract void Remove(T item);
    public abstract void Clear();
    public abstract bool Contains(T item);
    public abstract void Show();
    public abstract override string ToString();
    public abstract int Cardinality();
    public abstract bool IsEmpty();

    public abstract MySet<T> Union(MySet<T> other);
    public abstract MySet<T> Intersect(MySet<T> other);
    public abstract MySet<T> Difference(MySet<T> other);

    // Método para devolver todos los elementos como IEnumerable<T>
    public abstract IEnumerable<T> GetElements();
}
