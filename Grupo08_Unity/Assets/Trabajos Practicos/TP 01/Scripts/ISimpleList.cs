public interface ISimpleList<T>
{
    //Debe dar acceso al elemento en el indice "index" de la lista
    public T this[int index] { get; set; }

    //Debe indicar la cantidad de elementos guardados en la lista
    public int Count { get; }

    //Debe agregar "item" al final de la lista
    public void Add(T item);

    //Debe agregar todos los elementos de "collection" al final de la lista
    public void AddRange(T[] collection);

    //Debe remover el primer elemento que sea igual a "item"
    //Tambien devolver true si se pudo borrar un elemento, false si no
    public bool Remove(T item);

    //Debe limpiar toda la lista
    public void Clear();
}
