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
            ConcurrentBag<int> localMinList = new ConcurrentBag<int>();

            for (int i = 0; i < n - 1; i++)
            {
                //Tömmer concurrentbagen
                while (!localMinList.IsEmpty)
                {
                    localMinList.TryTake(out int derp);
                }
                int min = i;

                Parallel.ForEach(Partitioner.Create(i + 1, n), (range) =>
                {
                    localMinList.Add(range.Item1);
                    for (int j = range.Item1 + 1; j < range.Item2; j++)
                    {
                        if (comparer.Compare(inputOutput[j], inputOutput[range.Item1]) < 0)
                        {
                            T tmp = inputOutput[j];
                            inputOutput[j] = inputOutput[range.Item1];
                            inputOutput[range.Item1] = tmp;
                        }
                    }
                });

                int[] minList = localMinList.ToArray();

                for (int j = 0; j < localMinList.Count(); j++)
                {
                    if (comparer.Compare(inputOutput[minList[j]], inputOutput[min]) < 0)
                    {
                        T tmp = inputOutput[minList[j]];
                        inputOutput[minList[j]] = inputOutput[min];
                        inputOutput[min] = tmp;
                    }
                }
            }
        }
    }
}
