using System;
using System.Text;

namespace TP03
{
    public class MyQueue<T>
    {
        private T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public int Count => _count;

        public MyQueue(int capacity = 8)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            _items = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        /// <summary>
        /// Adds an element at the end of the queue.
        /// </summary>
        public void Enqueue(T item)
        {
            if (_count == _items.Length)
                Resize(_items.Length * 2);

            _items[_tail] = item;
            _tail = (_tail + 1) % _items.Length;
            _count++;
        }

        /// <summary>
        /// Removes and returns the first element of the queue.
        /// </summary>
        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T item = _items[_head];
            _items[_head] = default;
            _head = (_head + 1) % _items.Length;
            _count--;
            return item;
        }

        /// <summary>
        /// Returns the first element without removing it.
        /// </summary>
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _items[_head];
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            _items = new T[8];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        /// <summary>
        /// Converts the queue into an array.
        /// </summary>
        public T[] ToArray()
        {
            T[] result = new T[_count];
            for (int i = 0; i < _count; i++)
                result[i] = _items[(_head + i) % _items.Length];
            return result;
        }

        /// <summary>
        /// Returns the queue content as a string.
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _count; i++)
            {
                sb.Append(_items[(_head + i) % _items.Length]);
                if (i < _count - 1) sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Tries to remove and return the first element safely.
        /// </summary>
        public bool TryDequeue(out T item)
        {
            if (_count == 0)
            {
                item = default;
                return false;
            }

            item = Dequeue();
            return true;
        }

        /// <summary>
        /// Tries to peek the first element safely.
        /// </summary>
        public bool TryPeek(out T item)
        {
            if (_count == 0)
            {
                item = default;
                return false;
            }

            item = _items[_head];
            return true;
        }

        private void Resize(int newSize)
        {
            T[] newArray = new T[newSize];
            for (int i = 0; i < _count; i++)
                newArray[i] = _items[(_head + i) % _items.Length];

            _items = newArray;
            _head = 0;
            _tail = _count;
        }
    }
}
