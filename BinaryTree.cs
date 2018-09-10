using System;
using System.Collections.Generic;

namespace binary_tree
{
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
			if (_root == null)
			{
				_root = new Node<TValue>(value);
				return true;
			}

			return AddNewNode(_root, value);
		}

		private bool AddNewNode(Node<TValue> target, TValue valueToInsert)
		{
			var cmp = valueToInsert.CompareTo(target.Value);
			if (cmp < 0)
			{
				if (target.Left == null)
				{
					target.Left = new Node<TValue>(valueToInsert);
					return true;
				}
				return AddNewNode(target.Left, valueToInsert);
			}

			if (cmp > 0)
			{
				if (target.Right == null)
				{
					target.Right = new Node<TValue>(valueToInsert);
					return true;
				}
				return AddNewNode(target.Right, valueToInsert);
			}

			return false;
		}

		public void Remove(TValue value)
		{
			throw new NotImplementedException();
		}

		public bool Contains(TValue value)
		{
			if (_root == null)
			{
				return false;
			}

			return Contains(_root, value);
		}

		private bool Contains(Node<TValue> target, TValue value)
		{
			var cmp = value.CompareTo(target.Value);

			if (cmp == 0)
			{
				return true;
			}

			if (cmp < 0 && target.Left != null)
			{
				return Contains(target.Left, value);
			}

			if (cmp > 0 && target.Right != null)
			{
				return Contains(target.Right, value);
			}

			return false;
		}

		public int Size()
		{
			int count = 0;
			Walk(value => { count++; }, _root);
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
			if (target.Left != null)
			{
				Walk(fn, target.Left);
			}

			fn(target.Value);

			if (target.Right != null)
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
