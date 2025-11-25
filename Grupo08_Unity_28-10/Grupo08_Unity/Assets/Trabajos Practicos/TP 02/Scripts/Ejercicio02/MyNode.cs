using System.Collections.Generic;

namespace MyLinkedList
{
    public class MyNode<T>
    {
        public T Value;
        public MyNode<T> Prev;
        public MyNode<T> Next;

        public MyNode(T value)
        {
            Value = value;
            Prev = null;
            Next = null;
        }

        public override string ToString() 
        {  
            return Value.ToString();
        }

        public bool IsEquals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }
    }
}
