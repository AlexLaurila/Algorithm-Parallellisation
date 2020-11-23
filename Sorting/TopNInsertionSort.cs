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

            for (int i = 1; i < k; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (comparer.Compare(inputOutput[j], inputOutput[j - 1]) < 0)
                    {
                        T tmp = inputOutput[j - 1];
                        inputOutput[j - 1] = inputOutput[j];
                        inputOutput[j] = tmp;
                    }
                }
            }

            return inputOutput.Take<T>(n).ToArray();
        }
    }
}
