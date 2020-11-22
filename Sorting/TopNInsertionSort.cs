using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class TopNInsertionSort<T> : ITopNSort<T>
    {
        public string Name { get { return "TopN-InsertionSort"; } }

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
            int k = inputOutput.Length;
            T[] list = new T[k];

            for (int i = 1; i < n; i++)
            {
                T key = inputOutput[i];
                int j = i - 1;

                while (j >= 0 && comparer.Compare(inputOutput[j], key) > 0)
                {
                    inputOutput[j + 1] = inputOutput[j];
                    j = j - 1;
                }
                inputOutput[j + 1] = key;
                list.Append<T>(key);
            }

            return list.Take<T>(n).ToArray();
        }
    }
}
