using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSort<T> : ISort<T>
    {
        public string Name { get { return "QuickSort"; } }

        public void Sort(T[] inputOutput)
        {
            Sort(inputOutput, Comparer<T>.Default);
        }

        public void Sort(T[] inputOutput, IComparer<T> comparer)
        {
            int left = 0;
            int right = inputOutput.Length - 1;
            QuickSorter(inputOutput, comparer, left, right);

        }

        private static void QuickSorter<T>(T[] inputOutput, IComparer<T> comparer, int left, int right)
        {
            var depth = right - left;
            if (left >= right)
                return;

            SwapElements(inputOutput, left, (left + right) / 2);
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
            QuickSorter(inputOutput, comparer, left, last - 1);
            QuickSorter(inputOutput, comparer, last + 1, right);
            }

        static void SwapElements<T>(T[] inputOutput, int i, int j)
        {
            T temp = inputOutput[i];
            inputOutput[i] = inputOutput[j];
            inputOutput[j] = temp;
        }
    }
}
