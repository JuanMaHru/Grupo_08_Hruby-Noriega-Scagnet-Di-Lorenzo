using System;
using System.Text;

namespace MyLinkedList
{
    public class MyList<T>
    {
        private MyNode<T> root;
        private MyNode<T> tail;

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                MyNode<T> node = GetNode(index);
                return node.Value;
            }
            set
            {
                MyNode<T> node = GetNode(index);
                node.Value = value;
            }
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void Add(T value)
        {
            MyNode<T> newNode = new MyNode<T>(value);

            if (root == null)
            {
                root = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            Count++;
        }

        public void AddRange(MyList<T> other)
        {
            if (other == null || other.IsEmpty())
                return;

            MyNode<T> current = other.root;
            while (current != null)
            {
                Add(current.Value);
                current = current.Next;
            }
        }

        public void AddRange(T[] values)
        {
            if (values == null)
                return;

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        public bool Remove(T value)
        {
            MyNode<T> current = root;

            while (current != null)
            {
                if (current.IsEquals(value))
                {
                    RemoveNode(current);
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            MyNode<T> node = GetNode(index);
            RemoveNode(node);
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == Count)
            {
                Add(value);
                return;
            }

            if (index == 0)
            {
                MyNode<T> newNode = new MyNode<T>(value);

                newNode.Next = root;
                if (root != null)
                    root.Prev = newNode;

                root = newNode;

                if (tail == null)
                    tail = newNode;

                Count++;
                return;
            }

            MyNode<T> current = GetNode(index);
            MyNode<T> previous = current.Prev;

            MyNode<T> nodeToInsert = new MyNode<T>(value);

            nodeToInsert.Prev = previous;
            nodeToInsert.Next = current;

            previous.Next = nodeToInsert;
            current.Prev = nodeToInsert;

            Count++;
        }

        public void Clear()
        {
            root = null;
            tail = null;
            Count = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            MyNode<T> current = root;
            while (current != null)
            {
                sb.Append(current.Value);

                if (current.Next != null)
                    sb.Append(", ");

                current = current.Next;
            }

            sb.Append("]");
            return sb.ToString();
        }

        private MyNode<T> GetNode(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            MyNode<T> current = root;
            int i = 0;

            while (i < index)
            {
                current = current.Next;
                i++;
            }

            return current;
        }

        private void RemoveNode(MyNode<T> node)
        {
            if (node.Prev == null)
            {
                root = node.Next;
            }
            else
            {
                node.Prev.Next = node.Next;
            }

            if (node.Next == null)
            {
                tail = node.Prev;
            }
            else
            {
                node.Next.Prev = node.Prev;
            }

            Count--;
        }
    }
}
