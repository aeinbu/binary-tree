using System;

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
            // 78	30	19	78	77
            // 98	53	62	44	37
            // 24	64	39	82	10
            // 14
            // 75
            var tree = new BinaryTree<int>{26, 74, 68, 23, 24, 14, 78, 30, 80, 35, 70};

            Console.WriteLine();
            Console.WriteLine("These are the values:");
            foreach(var value in tree.ToSortedList())
            {
                Console.WriteLine(value);
            }
            Console.WriteLine($"Number of nodes: {tree.Count()}");

            Console.WriteLine();
            Console.WriteLine($"Remove(...): {tree.Remove(26)}");
            Console.WriteLine("These are the values after removal:");
            foreach(var value in tree.ToSortedList())
            {
                Console.WriteLine(value);
            }
            Console.WriteLine($"Number of nodes: {tree.Count()}");

        }
    }
}
