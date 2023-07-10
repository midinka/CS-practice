
namespace HelloWorld.Task3
{
    class Program
    {
        private static int comparator(int x, int y)
        {
            return x.CompareTo(y);
        }

        static void Main(string[] args)
        {
            var a = new[] { 3, 12, -9, 0, 36 };
            Console.WriteLine("Decrease - Merge");
            var array1 = a.Sorting(ExSort.TypeOfSort.Decrease, ExSort.Algorithm.Merge).ToArray();
            foreach (var item in array1)
            {
                Console.WriteLine(item);
            }

            var b = new[] { 4, 7, 132, -432, 3 };
            Console.WriteLine("Increase - Insert");
            var array2 = b.Sorting(ExSort.TypeOfSort.Increase, ExSort.Algorithm.Insert).ToArray();
            foreach (var item in array2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Increase - Pyramidal");

            var c = new[] { 2, 12, 32, -6, -2, 0, 10 };
            var array3 = c.Sorting(ExSort.TypeOfSort.Increase, ExSort.Algorithm.Pyramidal, Comparer<int>.Create(new Comparison<int>(comparator)));
            foreach (var item in array3)
            {
                Console.WriteLine(item);
            }

        }
    }
}
