using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
//using System.Windows.Media;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public class MandelbrotParallellPartitioner : MandelbrotBase
    {
        public override string Name
        {
            get { return "MandelbrotParallellPartitioner"; }
        }

        public MandelbrotParallellPartitioner(int pixelsX, int pixelsY) : base(pixelsX, pixelsY)
        {
        }

        public override void Compute()
        {
            int widthPixels = Image.GetLength(0);
            int heightPixels = Image.GetLength(1);
            double stepx = (UpperX - LowerX) / widthPixels;
            double stepy = (UpperY - LowerY) / heightPixels;

            Parallel.ForEach(Partitioner.Create(0, widthPixels), (range) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    for (int j = 0; j < heightPixels; j++)
                    {
                        double tempx = LowerX + i * stepx;
                        double tempy = LowerY + j * stepy;
                        int color = Diverge(tempx, tempy);
                        Image[i, j] = MAX_ITERATIONS - color;
                    }
                }
            });
        }
    }
}
