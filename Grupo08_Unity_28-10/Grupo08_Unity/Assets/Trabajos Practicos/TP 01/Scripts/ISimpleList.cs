public interface ISimpleList<T>
{
    public T this[int index] { get; set; }

    public int Count { get; }

    public void Add(T item);

    public void AddRange(T[] collection);

    public bool Remove(T item);

    public void Clear();
}
