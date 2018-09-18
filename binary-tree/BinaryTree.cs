using System;
using System.Collections.Generic;

namespace binary_tree
{
	public class BinaryTree<TValue>
		where TValue : IComparable<TValue>
	{
		private class Node<T> : IComparable<Node<T>>, IComparable<T>
			where T : IComparable<T>
		{
			public T Value { get; set; }

			public Node<T> Left;

			public Node<T> Right;

			public Node(T value)
			{
				Value = value;
			}

			public override string ToString() => $"Node({Value})";

			public int CompareTo(Node<T> other) => this.Value.CompareTo(other.Value);

			public int CompareTo(T other) => this.Value.CompareTo(other);


			public ref Node<T> GetLegFor(Node<T> childNode)
			{
				if (childNode == Left)
				{
					return ref Left;
				}

				if (childNode == Right)
				{
					return ref Right;
				}

				throw new InvalidOperationException();
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

		public bool Contains(TValue value)
		{
			var foundNode = FindNode(value);
			return foundNode != null;
		}

		public int Count()
		{
			int count = 0;
			Walk(value => { count++; }, _root);
			return count;
		}

		public int Count(TValue rangeFrom, TValue rangeTo)
		{
			int count = 0;
			Walk(value => { count++; }, _root, rangeFrom, rangeTo);
			return count;
		}

		public IList<TValue> ToSortedList()
		{
			var list = new List<TValue>();
			Walk(list.Add, _root);
			return list;
		}

		public IList<TValue> ToSortedList(TValue rangeFrom, TValue rangeTo)
		{
			var list = new List<TValue>();
			Walk(list.Add, _root, rangeFrom, rangeTo);
			return list;
		}

		private void Walk(Action<TValue> action, Node<TValue> target)
		{
			if (target == null)
			{
				return;
			}

			Walk(action, target.Left);

			action(target.Value);

			Walk(action, target.Right);
		}

		private void Walk(Action<TValue> action, Node<TValue> target, TValue rangeFrom, TValue rangeTo)
		{
			if (target == null)
			{
				return;
			}

			if (target.Value.CompareTo(rangeFrom) > 0)
			{
				Walk(action, target.Left, rangeFrom, rangeTo);
			}

			if (target.Value.CompareTo(rangeTo) <= 0 && target.Value.CompareTo(rangeFrom) >= 0)
			{
				action(target.Value);
			}

			if (target.Value.CompareTo(rangeTo) < 0)
			{
				Walk(action, target.Right, rangeFrom, rangeTo);
			}
		}

		public bool Remove(TValue value)
		{
			var nodeToRemove = FindNode(value);
			System.Console.WriteLine($"*** nodeToRemove: {nodeToRemove}");
			if (nodeToRemove == null)
			{
				return false;
			}

			// zero or one leg
			ref var legOfParentOfNodeToRemove = ref GetReferenceToChange(nodeToRemove);
			if (nodeToRemove.Left == null || nodeToRemove.Right == null)
			{
				legOfParentOfNodeToRemove = nodeToRemove.Left ?? nodeToRemove.Right ?? null;
				return true;
			}

			// two legs


			var rightNodeOfNodeToRemove = nodeToRemove.Right;
			if (rightNodeOfNodeToRemove.Left == null)
			{
				rightNodeOfNodeToRemove.Left = nodeToRemove.Left;
				legOfParentOfNodeToRemove = rightNodeOfNodeToRemove;
				return true;
			}

			var replacementNode = GetLeftmostNode(rightNodeOfNodeToRemove);
			System.Console.WriteLine($"*** replacementNode: {replacementNode}");
			var parentOfReplacementNode = FindParentNode(replacementNode);
			System.Console.WriteLine($"*** parentOfReplacementNode: {parentOfReplacementNode}");
			if(parentOfReplacementNode != nodeToRemove)
			{
				System.Console.WriteLine("A");
				parentOfReplacementNode.Left = replacementNode.Right;
				legOfParentOfNodeToRemove = replacementNode;
				replacementNode.Left = nodeToRemove.Left;
				replacementNode.Right = parentOfReplacementNode;
				return true;
			}
			else
			{
				System.Console.WriteLine("B");
				legOfParentOfNodeToRemove = replacementNode;
			}

			// two legs
			// 			var replacementNode = FindNearestNodeBiggerThan(nodeToRemove);
			// 			System.Console.WriteLine($"*** replacementNode: {replacementNode}");

			// 			var parentOfReplacementNode = FindParentNode(replacementNode);
			// 			System.Console.WriteLine($"*** parentOfReplacementNode: {parentOfReplacementNode}");

			// if(parentOfReplacementNode == nodeToRemove)
			// {
			// // handle this special case - good night
			// }

			// 			parentOfReplacementNode.Right = replacementNode.Right;
			// 			replacementNode.Left = null;
			// 			replacementNode.Right = nodeToRemove.Right;

			// 			legOfParentOfNodeToRemove = replacementNode;

			return true;
		}

		private Node<TValue> GetLeftmostNode(Node<TValue> node)
		{
			return node.Left != null ?
					GetLeftmostNode(node.Left) :
					node;
		}

		private ref Node<TValue> GetReferenceToChange(Node<TValue> nodeToRemove)
		{
			if (nodeToRemove == _root) return ref _root;
			var parentOfNodeToRemove = FindParentNode(nodeToRemove);
			return ref parentOfNodeToRemove.GetLegFor(nodeToRemove);
		}

		private Node<TValue> FindNearestNodeBiggerThan(Node<TValue> startNode) => FindNearestNodeBiggerThan(startNode, startNode);

		private Node<TValue> FindNearestNodeBiggerThan(Node<TValue> targetNode, Node<TValue> startNode)
		{
			if (targetNode == null)
			{
				return null;
			}

			if (targetNode == startNode)
			{
				return FindNearestNodeBiggerThan(targetNode.Right, startNode);
			}

			if (targetNode.Left != null)
			{
				return FindNearestNodeBiggerThan(targetNode.Left, startNode);
			}

			return targetNode;
		}

		private Node<TValue> FindParentNode(Node<TValue> childNode)
		{
			return FindNode(node => node.Left == childNode || node.Right == childNode ? 0 : node.CompareTo(childNode), _root);
		}

		public TValue Find(TValue value, bool throwOnNotFound = false)
		{
			var foundNode = FindNode(node => node.CompareTo(value), _root);
			return foundNode != null ?
				foundNode.Value :
				throwOnNotFound ?
					throw new Exception() :
					default(TValue);
		}

		private Node<TValue> FindNode(TValue value)
		{
			var foundNode = FindNode(node => node.CompareTo(value), _root);
			return foundNode;
		}


		private Node<TValue> FindNode(Func<Node<TValue>, int> comparer, Node<TValue> target)
		{
			if (target == null)
			{
				return null;
			}

			if (comparer(target) == 0)
			{
				return target;
			}

			if (comparer(target) > 0)
			{
				return FindNode(comparer, target.Left);
			}

			if (comparer(target) < 0)
			{
				return FindNode(comparer, target.Right);
			}

			return null;
		}
	}
}

