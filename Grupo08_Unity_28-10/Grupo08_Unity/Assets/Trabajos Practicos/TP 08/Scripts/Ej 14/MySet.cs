using System;
using System.Collections.Generic;

public abstract class MySet<T>
{
    // Método para devolver todos los elementos como IEnumerable<T>
    public abstract T[] GetElements();

    public virtual void Show(){}
    public abstract bool IsEmpty();
    public abstract override string ToString();

    public abstract void Add(T item);
    public abstract void Remove(T item);
    public abstract void Clear();
    public abstract bool Contains(T item);
    public abstract int Cardinality();

    public abstract MySet<T> Union(MySet<T> other);
    public abstract MySet<T> Intersect(MySet<T> other);
    public abstract MySet<T> Difference(MySet<T> other);

}
