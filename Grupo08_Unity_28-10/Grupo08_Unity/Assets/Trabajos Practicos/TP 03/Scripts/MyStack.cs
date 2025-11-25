using System;
using System.Text;

namespace TP03
{
    public class MyStack<T>
    {
        private T[] _items;
        private int _count;

        public int Count => _count;

        public MyStack(int capacity = 8)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            _items = new T[capacity];
            _count = 0;
        }

        public void Push(T item)
        {
            if (_count == _items.Length)
                Resize(_items.Length * 2);

            _items[_count++] = item;
        }

        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty.");

            T item = _items[--_count];
            _items[_count] = default;
            return item;
        }

        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty.");
            return _items[_count - 1];
        }

        public void Clear()
        {
            _items = new T[8];
            _count = 0;
        }

        public T[] ToArray()
        {
            T[] result = new T[_count];
            for (int i = 0; i < _count; i++)
                result[i] = _items[_count - 1 - i];
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = _count - 1; i >= 0; i--)
            {
                sb.Append(_items[i]);
                if (i > 0) sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public bool TryPop(out T item)
        {
            if (_count == 0)
            {
                item = default;
                return false;
            }

            item = Pop();
            return true;
        }

        public bool TryPeek(out T item)
        {
            if (_count == 0)
            {
                item = default;
                return false;
            }

            item = _items[_count - 1];
            return true;
        }

        private void Resize(int newSize)
        {
            T[] newArray = new T[newSize];
            for (int i = 0; i < _count; i++)
                newArray[i] = _items[i];

            _items = newArray;
        }
    }
}
