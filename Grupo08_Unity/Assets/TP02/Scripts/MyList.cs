using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinkedList
{
    internal sealed class MyNode<T>
    {
        public T Value;
        public MyNode<T> Next;
        public MyNode<T> Prev;

        public MyNode(T value, MyNode<T> prev = null, MyNode<T> next = null)
        {
            Value = value;
            Prev = prev;
            Next = next;
        }

        public bool IsEquals(T other)
        {
            if (ReferenceEquals(Value, null))
                return ReferenceEquals(other, null);
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        public override string ToString() => Value?.ToString() ?? "null";
    }

    public class MyList<T> : IEnumerable<T>
    {
        private MyNode<T> root; // primer nodo
        private MyNode<T> tail; // último nodo

        public int Count { get; private set; }

        public bool IsEmpty() => Count == 0;

        public void Add(T value)
        {
            var node = new MyNode<T>(value);
            if (root == null)
            {
                root = tail = node;
            }
            else
            {
                tail.Next = node;
                node.Prev = tail;
                tail = node;
            }
            Count++;
        }

        public void AddRange(MyList<T> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            foreach (var v in values)
                Add(v);
        }

        public void AddRange(T[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }

        public bool Remove(T value)
        {
            var node = FindNodeByValue(value);
            if (node == null) return false;
            Unlink(node);
            return true;
        }

        public void RemoveAt(int index)
        {
            var node = GetNodeAt(index);
            Unlink(node);
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == Count) { Add(value); return; }
            if (index == 0)
            {
                var newNode = new MyNode<T>(value, null, root);
                if (root != null) root.Prev = newNode;
                root = newNode;
                if (tail == null) tail = root;
                Count++;
                return;
            }

            var current = GetNodeAt(index);
            var prev = current.Prev;
            var node = new MyNode<T>(value, prev, current);
            prev.Next = node;
            current.Prev = node;
            Count++;
        }

        public void Clear()
        {
            // romper enlaces para ayudar al GC
            var cur = root;
            while (cur != null)
            {
                var next = cur.Next;
                cur.Prev = null;
                cur.Next = null;
                cur.Value = default;
                cur = next;
            }
            root = tail = null;
            Count = 0;
        }

        public T this[int index]
        {
            get => GetNodeAt(index).Value;
            set
            {
                var node = GetNodeAt(index);
                node.Value = value;
            }
        }

        public override string ToString()
        {
            if (IsEmpty()) return "[]";
            var cur = root;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append('[');
            while (cur != null)
            {
                sb.Append(cur.ToString());
                cur = cur.Next;
                if (cur != null) sb.Append(", ");
            }
            sb.Append(']');
            return sb.ToString();
        }

        private MyNode<T> FindNodeByValue(T value)
        {
            var cur = root;
            while (cur != null)
            {
                if (cur.IsEquals(value)) return cur;
                cur = cur.Next;
            }
            return null;
        }

        private MyNode<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // Optimización: desde el extremo más cercano
            if (index <= Count / 2)
            {
                var cur = root;
                for (int i = 0; i < index; i++) cur = cur.Next;
                return cur;
            }
            else
            {
                var cur = tail;
                for (int i = Count - 1; i > index; i--) cur = cur.Prev;
                return cur;
            }
        }

        private void Unlink(MyNode<T> node)
        {
            var prev = node.Prev;
            var next = node.Next;

            if (prev != null) prev.Next = next; else root = next;
            if (next != null) next.Prev = prev; else tail = prev;

            node.Next = node.Prev = null;
            node.Value = default;
            Count--;
        }

        // Para poder usar foreach (no es obligatorio, pero ayuda mucho)
        public IEnumerator<T> GetEnumerator()
        {
            var cur = root;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
