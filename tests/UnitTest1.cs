using System;
using binary_tree;
using Xunit;

namespace tests
{
	public class UnitTest1
	{
		[Theory]
		[InlineData(14, new[] { 23, 24, 26, 30, 35, 68, 70, 74, 78, 80 })]
		[InlineData(23, new[] { 14, 24, 26, 30, 35, 68, 70, 74, 78, 80 })]
		[InlineData(24, new[] { 14, 23, 26, 30, 35, 68, 70, 74, 78, 80 })]
		[InlineData(26, new[] { 14, 23, 24, 30, 35, 68, 70, 74, 78, 80 })]
		[InlineData(68, new[] { 14, 23, 24, 26, 30, 35, 70, 74, 78, 80 })]
		[InlineData(74, new[] { 14, 23, 24, 26, 30, 35, 68, 70, 78, 80 })]
		[InlineData(78, new[] { 14, 23, 24, 26, 30, 35, 68, 70, 74, 80 })]
		public void RemoveNode(int valueToRemove, int[] expected)
		{
			var tree = new BinaryTree<int>() { 26, 74, 68, 23, 24, 14, 78, 30, 80, 35, 70 };
			tree.Remove(valueToRemove);
			Assert.Equal(tree.ToSortedList(), expected);
		}

		[Fact]
		public void RemoveFromEmptyTreeReturnsFalse(){
			var tree = new BinaryTree<int>(){};

			var res = tree.Remove(10);
			
			Assert.False(res);
		}

		[Fact]
		public void RemoveNonExistingValueReturnsFalse(){
			var tree = new BinaryTree<int>(){10, 5, 15};

			var res = tree.Remove(11);
			
			Assert.False(res);
		}

		[Fact]
		public void RemoveRootWithNoChildren(){
			var tree = new BinaryTree<int>(){10};

			var res = tree.Remove(10);
			
			Assert.True(res);
			Assert.Equal(new int[0], tree.ToSortedList());
		}

		[Fact]
		public void RemoveRootWithLeftChild(){
			var tree = new BinaryTree<int>(){10, 5};

			var res = tree.Remove(10);
			
			Assert.True(res);
			Assert.Equal(new[]{5}, tree.ToSortedList());
		}

		[Fact]
		public void RemoveRootWithRightChild(){
			var tree = new BinaryTree<int>(){10, 15};
			
			var res = tree.Remove(10);
			
			Assert.True(res);
			Assert.Equal(new[]{15}, tree.ToSortedList());
		}

		[Fact]
		public void RemoveRootWithBothChildren(){
			var tree = new BinaryTree<int>(){10, 5, 15};
			
			var res = tree.Remove(10);

			Assert.True(res);
			Assert.Equal(new[]{5, 15}, tree.ToSortedList());
		}


		[Fact]
		public void RemoveRootWithBothChildrenAndAllGrandChildren(){
			var tree = new BinaryTree<int>(){10, 5, 2, 7, 15, 17, 12};

			var res = tree.Remove(10);

			Assert.True(res);
			Assert.Equal(new[]{2, 5, 7, 12, 15, 17}, tree.ToSortedList());
		}

		[Fact]
		public void RemoveRootWithBothChildrenAndAllGrandChildrenAndAllGreatGrandChildren(){
			var tree = new BinaryTree<int>(){10, 5, 15, 12, 11, 13, 17, 16, 18};

			var res = tree.Remove(10);

			Assert.True(res);
			Assert.Equal(tree.ToSortedList(), new[]{5, 11, 12, 13, 15, 16, 17, 18});
		}

		[Fact]
		public void RemoveNode1(){
			var tree = new BinaryTree<int>(){8, 10, 9, 11, 12};

			var res = tree.Remove(10);

			Assert.True(res);
			Assert.Equal(tree.ToSortedList(), new[]{8, 9, 11, 12});
		}
	}
}
