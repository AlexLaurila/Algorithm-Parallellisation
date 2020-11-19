using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace Sorting
{
    public class SelectionSortParallell<T> : ISort<T>
    {
        public string Name { get { return "SelectionSortParallell"; } }

        public void Sort(T[] inputOutput)
        {
            Sort(inputOutput, Comparer<T>.Default);
        }

        public void Sort(T[] inputOutput, IComparer<T> comparer)
        {
            int n = inputOutput.Length;
            object monitor = new object();
            for (int i = 0; i < n - 1; i++)
            {
                int min = i;

                Parallel.ForEach(Partitioner.Create(i, n), (range) =>
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
        }
    }
}
