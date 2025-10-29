using System;

namespace TP06.ABB
{
    public class MyABBNode<T>
    {
        public T Value;
        public MyABBNode<T> Left;
        public MyABBNode<T> Right;

        public MyABBNode(T value)
        {
            Value = value;
        }

        public override string ToString() => Value?.ToString() ?? "null";
    }
}
