using System;
using System.Text;

public class SimpleList<T> : ISimpleList<T>
{
    private T[] _items;   
    private int _count;    

    public SimpleList(int capacity = 4)
    {
        _items = new T[capacity];
        _count = 0;
    }

    public int Count => _count;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Índice fuera de rango");
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Índice fuera de rango");
            _items[index] = value;
        }
    }

    // Agregar un único elemento
    public void Add(T item)
    {
        EnsureCapacity();
        _items[_count] = item;
        _count++;
    }

    // Agregar un array de elementos
    public void AddRange(T[] collection)
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }

    // Remueve la primera ocurrencia de un elemento y devuelve true si lo elimina
    public bool Remove(T item)
    {
        int index = Array.IndexOf(_items, item, 0, _count);
        if (index == -1)
            return false;

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _items[_count - 1] = default(T);
        _count--;
        return true;
    }

    // Limpia todos los elementos
    public void Clear()
    {
        _items = new T[_items.Length];
        _count = 0;
    }

    // Asegura que siempre haya espacio disponible
    private void EnsureCapacity()
    {
        if (_count >= _items.Length)
        {
            int newSize = _items.Length * 2;
            T[] newArray = new T[newSize];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }
    }

    // Sobrescribe ToString para mostrar elementos separados por comas
    public override string ToString()
    {
        if (_count == 0)
            return "[]";

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _count; i++)
        {
            sb.Append(_items[i]);
            if (i < _count - 1)
                sb.Append(", ");
        }
        return sb.ToString();
    }
}
