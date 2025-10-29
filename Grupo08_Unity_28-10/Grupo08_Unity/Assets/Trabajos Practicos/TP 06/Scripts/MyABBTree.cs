using System;
using System.Collections.Generic;

namespace TP06.ABB
{
    /// <summary>
    /// Binary Search Tree (ABB). Generic + configurable comparer.
    /// Height convention: null => -1, leaf => 0 (AVL-style). 
    /// BalanceFactor(node) = Height(left) - Height(right).
    /// </summary>
    public class MyABBTree<T>
    {
        private readonly IComparer<T> _cmp;
        public MyABBNode<T> Root { get; private set; }

        public MyABBTree(IComparer<T> comparer = null)
        {
            _cmp = comparer ?? Comparer<T>.Default;
        }

        public MyABBNode<T> Insert(T value)
        {
            if (Root == null)
            {
                Root = new MyABBNode<T>(value);
                return Root;
            }

            MyABBNode<T> cur = Root;
            while (true)
            {
                int c = _cmp.Compare(value, cur.Value);
                if (c < 0)
                {
                    if (cur.Left == null) { cur.Left = new MyABBNode<T>(value); return cur.Left; }
                    cur = cur.Left;
                }
                else
                {
                    if (cur.Right == null) { cur.Right = new MyABBNode<T>(value); return cur.Right; }
                    cur = cur.Right;
                }
            }
        }

        // ---- Heights & Balance ----
        public int GetHeight() => GetHeight(Root);

        public int GetHeight(MyABBNode<T> node)
        {
            if (node == null) return -1; // null -> -1; leaf -> 0
            int hl = GetHeight(node.Left);
            int hr = GetHeight(node.Right);
            return 1 + (hl > hr ? hl : hr);
        }

        public int GetBalanceFactor() => GetBalanceFactor(Root);

        public int GetBalanceFactor(MyABBNode<T> node)
        {
            if (node == null) return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        // ---- Traversals (values) ----
        public List<T> InOrder()
        {
            var list = new List<T>();
            InOrder(Root, list);
            return list;
        }
        private void InOrder(MyABBNode<T> n, List<T> outList)
        {
            if (n == null) return;
            InOrder(n.Left, outList);
            outList.Add(n.Value);
            InOrder(n.Right, outList);
        }

        public List<T> PreOrder()
        {
            var list = new List<T>();
            PreOrder(Root, list);
            return list;
        }
        private void PreOrder(MyABBNode<T> n, List<T> outList)
        {
            if (n == null) return;
            outList.Add(n.Value);
            PreOrder(n.Left, outList);
            PreOrder(n.Right, outList);
        }

        public List<T> PostOrder()
        {
            var list = new List<T>();
            PostOrder(Root, list);
            return list;
        }
        private void PostOrder(MyABBNode<T> n, List<T> outList)
        {
            if (n == null) return;
            PostOrder(n.Left, outList);
            PostOrder(n.Right, outList);
            outList.Add(n.Value);
        }

        public List<T> LevelOrder()
        {
            var list = new List<T>();
            if (Root == null) return list;

            var q = new Queue<MyABBNode<T>>();
            q.Enqueue(Root);
            while (q.Count > 0)
            {
                var n = q.Dequeue();
                list.Add(n.Value);
                if (n.Left != null) q.Enqueue(n.Left);
                if (n.Right != null) q.Enqueue(n.Right);
            }
            return list;
        }

        // Elimina UNA ocurrencia del valor, la más cercana a la raíz.
        // Devuelve true si quitó algo.
        public bool RemoveOne(T value)
        {
            bool removed = false;
            Root = RemoveFirst(Root, value, ref removed);
            return removed;
        }

        // Borrado “primera coincidencia” en el camino de búsqueda.
        // Con duplicados a la DERECHA, la primera coincidencia es la más cercana a la raíz.
        private MyABBNode<T> RemoveFirst(MyABBNode<T> node, T value, ref bool removed)
        {
            if (node == null) return null;

            int c = _cmp.Compare(value, node.Value);
            if (c < 0)
            {
                node.Left = RemoveFirst(node.Left, value, ref removed);
                return node;
            }
            else if (c > 0)
            {
                node.Right = RemoveFirst(node.Right, value, ref removed);
                return node;
            }
            else
            {
                // Encontrado: eliminar este nodo
                removed = true;

                // Casos 0 o 1 hijo
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                // Caso 2 hijos: reemplazo por sucesor in-order (mínimo del subárbol derecho)
                T succ;
                node.Right = RemoveMin(node.Right, out succ);
                node.Value = succ;
                return node;
            }
        }

        // Quita el mínimo de un subárbol y devuelve el nuevo subárbol por arriba.
        // 'min' sale por out.
        private MyABBNode<T> RemoveMin(MyABBNode<T> node, out T min)
        {
            if (node.Left == null)
            {
                min = node.Value;
                return node.Right; // conectamos el hijo derecho hacia arriba
            }
            node.Left = RemoveMin(node.Left, out min);
            return node;
        }
    }
}
