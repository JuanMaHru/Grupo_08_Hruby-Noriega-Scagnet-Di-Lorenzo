using System;
using UnityEngine;

public class ExampleList<T> : ISimpleList<T>
{
    T[] internalArray;
    int defaultCapacity = 4;

    //Cuando haga Debug.Log(examplList[0]) me va a mostrar lo que hay en este array en el indice 0
    //Cuando haga exampleList[0] = 3 va a guardar el value (3) en ese lugar 
    public T this[int index] 
    { 
        get 
        {
            //Como el Count de la lista (elementos ocupados) no siempre es lo mismo que el Length del array interno (espacios posibles)
            //Chequeamos que se pueda acceder al indice en cuestion
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

    //List<int> numbers = new List<T>(); esto llama al constructor de la List
    //Constructor: funcion que se llama cuando hago new
    public ExampleList()
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

        //Si llegamos hasta aca, es porque hay lugar, ya sea que había lugar, o que agrandamos el array porque no había lugar
        internalArray[Count] = item;

        Count++;
    }

    public void AddRange(T[] collection)
    {
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new System.NotImplementedException();
        Count--;
    }

    void Resize()
    {

    }
}
