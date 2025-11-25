using System;
using System.Text;
using UnityEngine;

public class DefinitiveSimpleList<T> : ISimpleList<T>
{
    T[] internalArray;
    int defaultCapacity = 4;

    public T this[int index] 
    { 
        get 
        {
            if (index < Count)
                return internalArray[index];
            else throw new IndexOutOfRangeException();
        }
        set
        {
            if (index < Count) 
                internalArray[index] = value;
            else throw new IndexOutOfRangeException();
        }
    }

    public int Count { get; private set; }

    public DefinitiveSimpleList()
    {
        Count = 0;
        internalArray = new T[defaultCapacity];
    }

    public void Add(T item)
    {
        if (Count >= internalArray.Length)
        {
            Resize();
        }

        internalArray[Count] = item;

        Count++;
    }

    public void AddRange(T[] collection)
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }

    public void Clear()
    {
        internalArray = new T[internalArray.Length];
        Count = 0;
    }

    public bool Remove(T item)
    {
        int index = Array.IndexOf(internalArray, item, 0, Count);
        if (index == -1)
            return false;

        for (int i = index; i < Count - 1; i++)
        {
            internalArray[i] = internalArray[i + 1];
        }

        internalArray[Count - 1] = default(T);
        Count--;
        return true;
    }

    void Resize()
    {
        if (Count >= internalArray.Length)
        {
            int newSize = internalArray.Length * 2;
            T[] newArray = new T[newSize];
            Array.Copy(internalArray, newArray, internalArray.Length);
            internalArray = newArray;
        }
    }

    public override string ToString()
    {
        if (Count == 0)
            return "[]";

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Count; i++)
        {
            sb.Append(internalArray[i]);
            if (i < Count - 1)
                sb.Append(", ");
        }
        return sb.ToString();
    }
}
