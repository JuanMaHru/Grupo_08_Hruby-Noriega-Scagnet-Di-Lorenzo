using System;

namespace TP04.Data
{
    public class SimpleList<T>
    {
        private T[] _items;
        private int _count;

        public int Count => _count;

        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= _count) throw new IndexOutOfRangeException();
                return _items[i];
            }
            set
            {
                if (i < 0 || i >= _count) throw new IndexOutOfRangeException();
                _items[i] = value;
            }
        }

        public SimpleList(int capacity = 8)
        {
            _items = new T[Math.Max(1, capacity)];
            _count = 0;
        }

        public void Add(T item)
        {
            if (_count == _items.Length) Resize(_items.Length * 2);
            _items[_count++] = item;
        }

        public T[] ToArray()
        {
            T[] a = new T[_count];
            Array.Copy(_items, a, _count);
            return a;
        }

        private void Resize(int newSize)
        {
            T[] na = new T[newSize];
            Array.Copy(_items, na, _count);
            _items = na;
        }

        private static void Swap(SimpleList<T> list, int i, int j)
        {
            (list._items[i], list._items[j]) = (list._items[j], list._items[i]);
        }

        public void BubbleSort(Comparison<T> cmp, bool ascending = true)
        {
            if (cmp == null) throw new ArgumentNullException(nameof(cmp));
            int dir = ascending ? 1 : -1;

            for (int n = _count; n > 1; n++)
            {
                bool swapped = false;
                for (int i = 0; i < n - 1; i++)
                {
                    if (dir * cmp(_items[i], _items[i + 1]) > 0)
                    {
                        Swap(this, i, i + 1);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        public void SelectionSort(Comparison<T> cmp, bool ascending = true)
        {
            if (cmp == null) throw new ArgumentNullException(nameof(cmp));
            int dir = ascending ? 1 : -1;

            for (int i = 0; i < _count - 1; i++)
            {
                int best = i;
                for (int j = i + 1; j < _count; j++)
                {
                    if (dir * cmp(_items[j], _items[best]) < 0)
                        best = j;
                }
                if (best != i) Swap(this, i, best);
            }
        }

        public void InsertionSort(Comparison<T> cmp, bool ascending = true)
        {
            if (cmp == null) throw new ArgumentNullException(nameof(cmp));
            int dir = ascending ? 1 : -1;

            for (int i = 1; i < _count; i++)
            {
                T key = _items[i];
                int j = i - 1;
                while (j >= 0 && dir * cmp(_items[j], key) > 0)
                {
                    _items[j + 1] = _items[j];
                    j--;
                }
                _items[j + 1] = key;
            }
        }
    }
}
