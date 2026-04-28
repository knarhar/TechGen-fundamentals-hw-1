namespace TechGen_fundamentals_hw_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList list = new MyList();

            Console.WriteLine("=== ADD ===");
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            // trigger resize
            list.Add(5);

            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + " ");
            Console.WriteLine();

            Console.WriteLine("Count: " + list.Count); // 5

            // ----------------------------

            Console.WriteLine("\n=== ADD RANGE ===");
            list.AddRange(new int[] { 6, 7, 8 });

            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + " ");
            Console.WriteLine();

            Console.WriteLine("Count: " + list.Count); // 8

            // null case
            list.AddRange(null);

            // ----------------------------

            Console.WriteLine("\n=== REMOVE ===");
            bool removed = list.Remove(3);
            Console.WriteLine("Removed 3: " + removed);

            removed = list.Remove(100);
            Console.WriteLine("Removed 100: " + removed);

            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + " ");
            Console.WriteLine();

            // ----------------------------

            Console.WriteLine("\n=== TRYGET ===");
            if (list.TryGet(2, out int value))
                Console.WriteLine("Index 2: " + value);
            else
                Console.WriteLine("Index 2: invalid");

            if (list.TryGet(100, out value))
                Console.WriteLine("Index 100: " + value);
            else
                Console.WriteLine("Index 100: invalid");

            // ----------------------------

            Console.WriteLine("\n=== INDEXOF / CONTAINS ===");
            Console.WriteLine("IndexOf 5: " + list.IndexOf(5)); // expected index
            Console.WriteLine("Contains 7: " + list.Contains(7)); // true
            Console.WriteLine("Contains 999: " + list.Contains(999)); // false

            // ----------------------------

            Console.WriteLine("\n=== INDEXER ===");
            Console.WriteLine("Before: " + list[0]);
            list[0] = 99;
            Console.WriteLine("After: " + list[0]);

            // ----------------------------

            Console.WriteLine("\n=== CLEAR ===");
            list.Clear();
            Console.WriteLine("Count after clear: " + list.Count); // 0

            // TryGet after clear
            if (list.TryGet(0, out value))
                Console.WriteLine(value);
            else
                Console.WriteLine("List is empty");
        }
    }
}
