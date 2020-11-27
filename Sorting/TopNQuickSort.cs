using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace Sorting
{
    public class TopNQuickSort<T> : ITopNSort<T>
    {
        public string Name { get { return "TopNQuickSort"; } }

        public T[] TopNSort(T[] inputOutput, int n)
        {
            return TopNSort(inputOutput, n, Comparer<T>.Default);
        }

        public T[] TopNSort(T[] inputOutput, int n, IComparer<T> comparer)
        {
            int left = 0;
            int right = inputOutput.Length - 1;
            TopNQuickSorter(inputOutput, comparer, left, right, n);
            return inputOutput.Take<T>(n).ToArray();
        }

        private static void TopNQuickSorter<T>(T[] inputOutput, IComparer<T> comparer, int left, int right, int n)
        {
            var depth = right - left;
            if (left >= right)
                return;

            int last = left;
            for (int current = left + 1; current <= right; ++current)
            {
                if (comparer.Compare(inputOutput[current], inputOutput[left]) < 0)
                {
                    last++;
                    SwapElements(inputOutput, last, current);
                }
            }

            SwapElements(inputOutput, left, last);

            if (depth < 2048)
            {
                TopNQuickSorter(inputOutput, comparer, left, last - 1, n);

                if(last < n)
                    TopNQuickSorter(inputOutput, comparer, last + 1, right, n);
            }
            else
            {
                if (last < n)
                {
                    Parallel.Invoke(
                        () => TopNQuickSorter(inputOutput, comparer, left, last - 1, n),
                        () => TopNQuickSorter(inputOutput, comparer, last + 1, right, n)
                    );
                }
                else
                    TopNQuickSorter(inputOutput, comparer, left, last - 1, n);
            }
        }

        static void SwapElements<T>(T[] inputOutput, int i, int j)
        {
            T temp = inputOutput[i];
            inputOutput[i] = inputOutput[j];
            inputOutput[j] = temp;
        }
    }
}
