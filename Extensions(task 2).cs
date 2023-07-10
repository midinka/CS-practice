
namespace HelloWorld
{
    public static class Ex
    {
        private static void has_the_same<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparator)
        {
            var tmp = collection.Distinct(comparator);
            if (collection.Count() != collection.Distinct(comparator).Count())
            {
                throw new ArgumentException("Found identical values in initial enumerable", nameof(collection));
            }
        }

        public static IEnumerable<IEnumerable<T>> generate_combinations<T>(this IEnumerable<T> collection, int k, IEqualityComparer<T> comparator)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comparator == null)
            {
                throw new ArgumentNullException(nameof(comparator));
            }

            if (k < 0)
            {
                throw new ArgumentException("Invalid value", nameof(k));
            }

            collection.has_the_same(comparator);
            var res_arr = new List<List<T>>();
            var cur_arr = new List<T>();

            generating_combinations<T>(0, k, res_arr, cur_arr, collection.ToList());
            return res_arr;


        }

        private static void generating_combinations<T>(int index, int k, List<List<T>> res_arr, List<T> cur_arr, List<T> collection)
        {
            if (k == cur_arr.Count)
            {
                res_arr.Add(cur_arr.ToList());
                return;
            }

            int size = collection.Count;

            for (int i = index; i < size; i++)
            {
                cur_arr.Add(collection[i]);
                generating_combinations<T>(i, k, res_arr, cur_arr, collection);
                cur_arr.RemoveAt(cur_arr.Count - 1);
            }
        }

        public static IEnumerable<IEnumerable<T>> generate_subsets<T>(this IEnumerable<T> collection,
        IEqualityComparer<T> comparator)
        {
            if (collection == null)
            {
                throw new ArgumentException(nameof(collection));
            }

            if (comparator == null)
            {
                throw new ArgumentException(nameof(comparator));
            }
            collection.has_the_same(comparator);

            var collection_arr = collection.ToList();

            var res_arr = new List<List<T>>();

            int size = collection_arr.Count;

            for (int i = 0; i < (1 << size); i++)
            {
                res_arr.Add(new List<T>());
                for (int j = 0; j < size; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        res_arr.Last().Add(collection_arr[j]);
                    }
                }
            }

            return res_arr;
        }

        public static IEnumerable<IEnumerable<T>> generate_permutations<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparator)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comparator == null)
            {
                throw new ArgumentNullException(nameof(comparator));
            }

            collection.has_the_same(comparator);
            var res_arr = new List<List<T>>();
            var cur_arr = new List<T>();

            generating_permutations<T>(collection.ToList(), cur_arr, res_arr);
            return res_arr;


        }

        private static void generating_permutations<T>(List<T> collection, List<T> cur_arr, List<List<T>> res_arr)
        {
            if (collection.Count == 0)
            {
                res_arr.Add(cur_arr);
                return;
            }

            int size = collection.Count;
            for (int i = 0; i < size; i++)
            {
                var newclollection = new List<T>(collection);
                newclollection.RemoveAt(i);
                var newCurrent = new List<T>(cur_arr) { collection[i] };
                generating_permutations<T>(newclollection, newCurrent, res_arr);
            }
        }

    
    }
}
