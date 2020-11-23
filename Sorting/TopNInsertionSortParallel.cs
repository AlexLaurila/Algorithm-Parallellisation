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
            return InsertionSort(inputOutput, n, comparer);
        }

        public T[] InsertionSort(T[] inputOutput, int n, IComparer<T> comparer)
        {
            int k = inputOutput.Length;
            ConcurrentBag<T> nValueList = new ConcurrentBag<T>();

            Parallel.ForEach(Partitioner.Create(0, k), (range) =>
            {
                //Loopar igenom hela listan k
                for (int i = range.Item1 + 1; i < range.Item2; i++)
                {
                    //Jämför med talen bakom
                    for (int j = i; j > range.Item1; j--)
                    {
                        //Byter plats
                        if (comparer.Compare(inputOutput[j], inputOutput[j - 1]) < 0)
                        {
                            T tmp = inputOutput[j - 1];
                            inputOutput[j - 1] = inputOutput[j];
                            inputOutput[j] = tmp;
                        }
                    }
                }
                //Lägger in n värden i nya listan
                for (int i = range.Item1; i < range.Item1 + n; i++)
                {
                    //Breakar om i är lika stor som maximala värdet i partitionen
                    if (i == range.Item2) break;
                    nValueList.Add(inputOutput[i]);
                }
            });

            T[] newList = nValueList.ToArray();

            //Kör sekventiell sortering på array
            for(int i = 1; i < newList.Length; i++)
            {
                for(int j = i; j > 0; j--)
                {
                    if (comparer.Compare(newList[j], newList[j - 1]) < 0)
                    {
                        T tmp = newList[j - 1];
                        newList[j - 1] = newList[j];
                        newList[j] = tmp;
                    }
                }
            }
            //Returnera sorterade array
            return newList.Take<T>(n).ToArray();
        }
    }
}