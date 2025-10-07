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

        /// <summary>
        /// Pushes an element onto the stack.
        /// </summary>
        public void Push(T item)
        {
            if (_count == _items.Length)
                Resize(_items.Length * 2);

            _items[_count++] = item;
        }

        /// <summary>
        /// Pops and returns the last element.
        /// </summary>
        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty.");

            T item = _items[--_count];
            _items[_count] = default;
            return item;
        }

        /// <summary>
        /// Returns the last element without removing it.
        /// </summary>
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty.");
            return _items[_count - 1];
        }

        /// <summary>
        /// Clears the stack.
        /// </summary>
        public void Clear()
        {
            _items = new T[8];
            _count = 0;
        }

        /// <summary>
        /// Converts the stack into an array.
        /// </summary>
        public T[] ToArray()
        {
            T[] result = new T[_count];
            for (int i = 0; i < _count; i++)
                result[i] = _items[_count - 1 - i];
            return result;
        }

        /// <summary>
        /// Returns the stack content as a string.
        /// </summary>
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

        /// <summary>
        /// Tries to pop the last element safely.
        /// </summary>
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

        /// <summary>
        /// Tries to peek the last element safely.
        /// </summary>
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
