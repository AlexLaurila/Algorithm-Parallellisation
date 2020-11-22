using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class TopNInsertionSortParallel<T> : ITopNSort<T>
    {
        public string Name { get { return "TopN-InsertionSortParallel"; } }

        public T[] TopNSort(T[] inputOutput, int n)
        {
            return TopNSort(inputOutput, n, Comparer<T>.Default);
        }

        public T[] TopNSort(T[] inputOutput, int n, IComparer<T> comparer)
        {
            T[] list = InsertionSort(inputOutput, n, comparer);
            return list;
        }

        public T[] InsertionSort(T[] inputOutput, int n, IComparer<T> comparer)
        {
            T[] list = new T[n];
            object monitor = new object();
            int i = 1;

            Parallel.ForEach(Partitioner.Create(i, n), (range) =>
            {
                T key = inputOutput[i];
                int j = i - 1;

                lock (monitor)
                {
                    while (j >= 0 && comparer.Compare(inputOutput[j], key) > 0)
                    {
                        inputOutput[j + 1] = inputOutput[j];
                        j = j - 1;
                    }
                    inputOutput[j + 1] = key;
                }
                list.Append<T>(key);
            });

            return list.Take<T>(n).ToArray();
        }
    }
}
