using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
//using System.Windows.Media;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public class MandelbrotParallell : MandelbrotBase
    {
        public override string Name
        {
            get { return "MandelbrotParallell"; }
        }

        public MandelbrotParallell(int pixelsX, int pixelsY) : base(pixelsX, pixelsY)
        {
        }

        public override void Compute()
        {
            //Compute(new Tuple<double, double>(LowerX, UpperX),
            //    new Tuple<double, double>(LowerY, UpperY),
            //    Image);

            int widthPixels = Image.GetLength(0);
            int heightPixels = Image.GetLength(1);
            double stepx = (UpperX - LowerX) / widthPixels;
            double stepy = (UpperY - LowerY) / heightPixels;


            Parallel.For(0, widthPixels, i =>
            {
                for (int j = 0; j < heightPixels; j++)
                {
                    double tempx = LowerX + i * stepx;
                    double tempy = LowerY + j * stepy;
                    int color = Diverge(tempx, tempy);
                    Image[i, j] = MAX_ITERATIONS - color;
                }
            });

            //for (int i = 0; i < widthPixels; i++)
            //{
            //    for (int j = 0; j < heightPixels; j++)
            //    {
            //        double tempx = LowerX + i * stepx;
            //        double tempy = LowerY + j * stepy;
            //        int color = Diverge(tempx, tempy);
            //        Image[i, j] = MAX_ITERATIONS - color;
            //    }
            //}
        }
    }
}
