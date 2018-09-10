using System;
using System.Collections.Generic;

namespace binary_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            // Random numbers:
            // 26	18	90	94	45
            // 74	70	38	11	8
            // 68	65	87	39	18
            // 23	57	77	68	14
            // 30	34	43	90	96
            // 75	30	19	78	77
            // 98	53	62	44	37
            // 14	64	39	82	10
            var tree = new BinaryTree<int>();
            tree.Add(26);
            tree.Add(74);
            tree.Add(68);
            tree.Add(23);
            tree.Add(30);
            tree.Add(75);
            tree.Add(98);
            Console.WriteLine(tree.Add(14));
            Console.WriteLine(tree.Add(75));

            Console.WriteLine("These are the values:");
            foreach(var value in tree.ToSortedList())
            {
                Console.WriteLine(value);
            }

            Console.WriteLine($"Number of nodes: {tree.Size()}");
        }
    }

    public class BinaryTree<TValue>
        where TValue : IComparable<TValue>
    {
        private class Node<T>
        {
            public T Value { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node<TValue> _root = null;

        public BinaryTree()
        {
        }

        public bool Add(TValue value)
        {
            if(_root == null){
                _root = new Node<TValue>(value);
                return true;
            }

            return AddNewNode(_root, value);
        }

        private bool AddNewNode(Node<TValue> target, TValue valueToInsert)
        {
            var cmp = valueToInsert.CompareTo(target.Value);
            if(cmp < 0)
            {
                if(target.Left == null)
                {
                    target.Left = new Node<TValue>(valueToInsert);
                    return true;
                }
                else
                {
                    return AddNewNode(target.Left, valueToInsert);
                }
            }
            else if(cmp > 0)
            {
                if(target.Right == null)
                {
                    target.Right = new Node<TValue>(valueToInsert);
                    return true;
                }
                else
                {
                    return AddNewNode(target.Right, valueToInsert);
                }
            }
            else
            {
                return false;
            }
        }

        public void Remove(TValue value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(TValue value)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            int count = 0;
            Walk(value => {count++;}, _root);
            return count;
        }

        public IList<TValue> ToSortedList()
        {
            var list = new List<TValue>();
            Walk(list.Add, _root);
            return list;
        }

		private void Walk(Action<TValue> fn, Node<TValue> target)
		{
            if(target.Left != null)
            {
				Walk(fn, target.Left);
            }

            fn(target.Value);

            if(target.Right != null)
            {
                Walk(fn, target.Right);
            }
		}

		private bool FindNearestBiggerThan(Node<TValue> node)
        {
            throw new NotImplementedException();
        }

        private bool FindParent(Node<TValue> node) 
        {
            throw new NotImplementedException();
        }
    }
}
