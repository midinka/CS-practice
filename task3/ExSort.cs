
using static HelloWorld.Task3.ExSort;

namespace HelloWorld.Task3
{
    public static class ExSort
    {
        public enum TypeOfSort
        {
            Increase,
            Decrease
        }

        public enum Algorithm
        {
            Insert,
            Choose,
            Pyramidal,
            Fast,
            Merge
        }

        private static void check_collection<T>(T[] collection)
        {
            if (collection == null)
            {
                throw new ArgumentException(nameof(collection));
            }
        }
        public static T[] Sorting<T>(this T[]? collection, TypeOfSort typeofsort, Algorithm algorithm)where T: IComparable<T>
        {
            check_collection<T>(collection);

            if (collection.Clone() is not T[] res_arr) throw new ArgumentException("Problems with collection");

            var del = new Comparison<T>((x, y) =>
            {
                if (x==null && y!=null) return -1;
                if (x==null && y==null) return 0;
                if (x!=null && y==null) return 1;
                if (x!=null && y!=null) return x.CompareTo(y);
                return 0;
            });

            ChosenAlg(res_arr, algorithm, typeofsort, del);
            return res_arr;
        }

        public static T[] Sorting<T>(this T[]? collection, TypeOfSort typeofsort, Algorithm algorithm, IComparer<T>? comp)
        {
            check_collection<T>(collection);

            if (comp == null)
            {
                throw new ArgumentException(nameof(comp));
            }

            if (collection.Clone() is not T[] res_arr) throw new ArgumentException("Problems with collection");

            var del = new Comparison<T>(comp.Compare);
            ChosenAlg(res_arr, algorithm, typeofsort, del);
            return res_arr;
        }

        public static T[] Sorting<T>(this T[]? collection, TypeOfSort typeofsort, Algorithm algorithm, Comparer<T>? comp)
        {
            check_collection<T>(collection);

            if (comp == null)
            {
                throw new ArgumentException(nameof(comp));
            }

            if (collection.Clone() is not T[] res_arr) throw new ArgumentException("Problems with collection");

            var del = new Comparison<T>(comp.Compare);
            ChosenAlg(res_arr, algorithm, typeofsort, del);
            return res_arr;

        }

        public static T[] Sorting<T>(this T[]? collection, TypeOfSort typeofsort, Algorithm algorithm, Comparison<T>? comp)
        {
            check_collection<T>(collection);

            if (comp == null)
            {
                throw new ArgumentException(nameof(comp));
            }

            if (collection.Clone() is not T[] res_arr) throw new ArgumentException("Problems with collection");

            var del = new Comparison<T>(comp);
            ChosenAlg(res_arr, algorithm, typeofsort, del);
            return res_arr;

        }

        private static void ChosenAlg<T>(T[] res_arr, Algorithm algorithm, TypeOfSort  typeofsort, Comparison<T> del)
        {
            switch (algorithm)
            {
                case Algorithm.Insert:
                    InsertSort<T>(res_arr, typeofsort, del);
                    break;

                case Algorithm.Choose:
                    ChooseSort<T>(res_arr, typeofsort, del);
                    break;

                case Algorithm.Pyramidal:
                    PyramidalSort<T>(res_arr, typeofsort, del);
                    break;
                case Algorithm.Fast:
                    FastSort<T>(res_arr, typeofsort, del);
                    break;

                case Algorithm.Merge:
                    MergeSort<T>(res_arr, typeofsort, del);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Wrong algorithm");

            }
        }

        ///////////////////Insert sort////////////////

        private static void InsertSort<T>(T[] res_arr, TypeOfSort typeofsort, Comparison<T> comp)

        {

            int type = (typeofsort == TypeOfSort.Decrease) ? -1 : 1;
            int size = res_arr.Length;
            for (int i = 1; i < size; i++)
            {
                var value = res_arr[i];
                int j = i - 1;
                while (j >= 0 && comp.Invoke(res_arr[j], value) == type)
                {
                    res_arr[j + 1] = res_arr[j];
                    j--;
                }
                res_arr[j + 1] = value;
            }
        }

        /////////////////////Choose Sort/////////////////

        private static void ChooseSort<T>(T[] res_arr, TypeOfSort typeofsort, Comparison<T> comp)
        {

            int type = (typeofsort == TypeOfSort.Decrease) ? 1 : -1;

            int size = res_arr.Length;
            for (int i = 0; i < size - 1; i++)
            {
                var indexValue = i;
                for (int j = i + 1; j < size; j++)
                {
                    if (comp.Invoke(res_arr[j], res_arr[indexValue]) == type)
                    {
                        indexValue = j;
                    }
                }
                (res_arr[i], res_arr[indexValue]) = (res_arr[indexValue], res_arr[i]);
            }
        }

        /////////////////////Pyramidal Sort/////////////////// 

        private static void PyramidalSort<T>(T[] res_arr, TypeOfSort typeofsort, Comparison<T> comp)
        {
            int size = res_arr.Length;
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                CreateBunch(i, size, res_arr, comp, typeofsort);
            }

            for (int i = size - 1; i > 0; i--)
            {
                (res_arr[0], res_arr[i]) = (res_arr[i], res_arr[0]);
                CreateBunch(0, i, res_arr, comp, typeofsort);
            }
        }

        private static void CreateBunch<T>(int index, int size, T[] res_arr, Comparison<T> comp, TypeOfSort typeofsort)
        {
            int type = (typeofsort == TypeOfSort.Decrease) ? -1 : 1;

            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;
            if (left < size && comp.Invoke(res_arr[left], res_arr[largest]) == type)
            {
                largest = left;
            }
            if (right < size && comp.Invoke(res_arr[right], res_arr[largest]) == type)
            {
                largest = right;
            }

            if (index != largest)
            {
                (res_arr[largest], res_arr[index]) = (res_arr[index], res_arr[largest]);
                CreateBunch(largest, size, res_arr, comp, typeofsort);
            }
        }

        /////////////////////Fast Sort/////////////////////////

        private static void FastSort<T>(T[] res_arr, TypeOfSort typeofsort, Comparison<T> comp)
        {
            FastSort(res_arr, typeofsort, comp, res_arr.Length - 1, 0);
        }

        private static void FastSort<T>(T[] res_arr, TypeOfSort typeofsort, Comparison<T> comparator, int maxIndex, int minIndex)
        {
            if (minIndex < maxIndex)
            {
                var support = Support(res_arr, maxIndex, minIndex, typeofsort, comparator);
                FastSort(res_arr, typeofsort, comparator, support - 1, minIndex);
                FastSort(res_arr, typeofsort, comparator, maxIndex, support + 1);
            }
        }
        private static int Support<T>(T[] res_arr, int maxIndex, int minIndex, TypeOfSort typeofsort, Comparison<T> comp)
        {
            int type = (typeofsort == TypeOfSort.Decrease) ? -1 : 1;

            int support = minIndex - 1;
            int size = res_arr.Length;
            for (int i = minIndex; i < size; i++)
            {
                if (comp.Invoke(res_arr[i], res_arr[maxIndex]) == type)
                {
                    support++;
                    (res_arr[i], res_arr[support]) = (res_arr[support], res_arr[i]);
                }
            }

            support++;
            (res_arr[maxIndex], res_arr[support]) = (res_arr[support], res_arr[maxIndex]);
            return support;
        }

        ////////////////////////Merge Sort/////////////////////////

        private static void MergeSort<T>(T[] res_arr, TypeOfSort typeofsort, Comparison<T> comp)
        {
            int type = (typeofsort == TypeOfSort.Decrease) ? 1 : -1;
            MergeSortRecursion(res_arr, 0, res_arr.Length - 1, comp, type);
        }

        private static void MergeSortRecursion<T>(T[] res_arr, int lowIndex, int highIndex, Comparison<T> comp, int type)
        {
            if (lowIndex >= highIndex) return;
            var middleIndex = (lowIndex + highIndex) / 2;
            MergeSortRecursion(res_arr, lowIndex, middleIndex, comp, type);
            MergeSortRecursion(res_arr, middleIndex + 1, highIndex, comp, type);
            Merge(res_arr, lowIndex, middleIndex, highIndex, comp, type);
        }


        private static void Merge<T>(T[] res_arr, int lowIndex, int middleIndex, int highIndex, Comparison<T> comp, int type)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArr = new T[highIndex - lowIndex + 1];
            var index = 0;

            while (left <= middleIndex && right <= highIndex)
            {
                if (comp.Invoke(res_arr[left], res_arr[right]) == type)
                {
                    tempArr[index] = res_arr[left];
                    left++;
                }
                else
                {
                    tempArr[index] = res_arr[right];
                    right++;
                }
                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArr[index] = res_arr[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArr[index] = res_arr[i];
                index++;
            }

            for (var i = 0; i < tempArr.Length; i++)
                res_arr[lowIndex + i] = tempArr[i];
        }

    }
}
