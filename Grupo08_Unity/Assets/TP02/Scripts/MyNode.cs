using System.Collections.Generic;

namespace MyLinkedList
{
    public class MyNode<T>
    {
        public T Value { get; set; }
        public MyNode<T> Prev { get; set; }
        public MyNode<T> Next { get; set; }

        public MyNode(T value, MyNode<T> prev = null, MyNode<T> next = null)
        {
            Value = value;
            Prev = prev;
            Next = next;
        }

        public override string ToString() => Value?.ToString() ?? "null";

        public bool IsEquals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }
    }
}
