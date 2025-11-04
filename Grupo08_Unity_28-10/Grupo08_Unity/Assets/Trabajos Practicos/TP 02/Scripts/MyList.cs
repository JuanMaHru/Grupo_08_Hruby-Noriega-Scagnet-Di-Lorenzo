using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinkedList
{
    public class MyList<T> : IEnumerable<T>
    {
        private MyNode<T> root;  
        private MyNode<T> tail;  

        public int Count { get; private set; }

        public bool IsEmpty() => Count == 0;

        // Agrega un único elemento al final de la lista
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

        // Agrega todos los elementos de otra lista MyList<T>
        public void AddRange(MyList<T> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            foreach (var v in values)
                Add(v);
        }

        // Agrega todos los elementos de un array
        public void AddRange(T[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }

        // Remueve la primera ocurrencia de un valor
        public bool Remove(T value)
        {
            var node = FindNodeByValue(value);
            if (node == null) return false;
            Unlink(node);
            return true;
        }

        // Remueve un nodo en un índice específico
        public void RemoveAt(int index)
        {
            var node = GetNodeAt(index);
            Unlink(node);
        }

        // Inserta un elemento en un índice específico
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

        // Limpia completamente la lista
        public void Clear()
        {
            var cur = root;
            while (cur != null)
            {
                var next = cur.Next;
                cur.Prev = null;
                cur.Next = null;
                cur = next;
            }
            root = tail = null;
            Count = 0;
        }

        // Indexador para acceder por índice
        public T this[int index]
        {
            get => GetNodeAt(index).Value;
            set
            {
                var node = GetNodeAt(index);
                node.Value = value;
            }
        }

        // Representación en string de la lista
        public override string ToString()
        {
            if (IsEmpty()) return "[]";
            var cur = root;
            var sb = new System.Text.StringBuilder();
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

        // Busca un nodo por valor
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

        // Obtiene un nodo en un índice específico (optimizado: head/tail)
        private MyNode<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

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

        // Desconecta un nodo de la lista
        private void Unlink(MyNode<T> node)
        {
            var prev = node.Prev;
            var next = node.Next;

            if (prev != null) prev.Next = next; else root = next;
            if (next != null) next.Prev = prev; else tail = prev;

            node.Next = node.Prev = null;
            Count--;
        }

        // Permite usar foreach en la lista
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
