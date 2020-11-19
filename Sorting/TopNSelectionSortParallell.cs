using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting
{
    public class TopNSelectionSortParallell<T> : ITopNSort<T>
    {
        public string Name { get { return "TopN-SelectionSortParallell"; } }

        public T[] TopNSort(T[] inputOutput, int n)
        {
            return TopNSort(inputOutput, n, Comparer<T>.Default);
        }

        public T[] TopNSort(T[] inputOutput, int n, IComparer<T> comparer)
        {
            int m = inputOutput.Length;
            object monitor = new object();
            for (int i = 0; i < n - 1; i++)
            {
                int min = i;

                Parallel.ForEach(Partitioner.Create(i, m), (range) =>
                {
                    for (int j = range.Item1; j < range.Item2; j++)
                    {
                        lock (monitor)
                        {
                            if (comparer.Compare(inputOutput[j], inputOutput[min]) < 0)
                            {
                                T tmp = inputOutput[j];
                                inputOutput[j] = inputOutput[min];
                                inputOutput[min] = tmp;
                            }
                        }
                    }
                });
            }
            return inputOutput.Take<T>(n).ToArray();
        }
    }
}
