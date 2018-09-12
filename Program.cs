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
            // 75	30	19	78	77
            // 98	53	62	44	37
            // 24	64	39	82	10
            // 14
            // 75
            var tree = new BinaryTree<int>();
            tree.Add(26);
            tree.Add(74);
            tree.Add(68);
            tree.Add(23);
            tree.Add(30);
            tree.Add(75);
            tree.Add(98);
            tree.Add(24);
            Console.WriteLine($"Add(14): {tree.Add(14)}");
            Console.WriteLine($"Add(75): {tree.Add(75)}");

            Console.WriteLine($"Contains(23): {tree.Contains(23)}");
            Console.WriteLine($"Contains(1000): {tree.Contains(1000)}");

            Console.WriteLine($"Find(75): {tree.Find(75)}");
            Console.WriteLine($"Find(12): {tree.Find(12)}");

            Console.WriteLine();
            Console.WriteLine("These are the values:");
            foreach(var value in tree.ToSortedList())
            {
                Console.WriteLine(value);
            }
            Console.WriteLine($"Number of nodes: {tree.Count()}");

            Console.WriteLine();
            Console.WriteLine($"Remove(...): {tree.Remove(74)}");
            Console.WriteLine("These are the values after removal:");
            foreach(var value in tree.ToSortedList())
            {
                Console.WriteLine(value);
            }
            Console.WriteLine($"Number of nodes: {tree.Count()}");

            // Console.WriteLine();
            // Console.WriteLine("These are the values from 20 to 30:");
            // foreach(var value in tree.ToSortedList(20, 30))
            // {
            //     Console.WriteLine(value);
            // }
            // Console.WriteLine($"Number of nodes from 20 to 30: {tree.Count(20, 30)}");

        }
    }
}
