using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maksimum
{
    public class Point
    {
        public double[] arg;
        private int n;

        public Point(params double[] arg)
        {
            this.n = arg.Length;
            this.arg = arg;
        }

        public override string ToString()
        {
            return String.Join(" ", arg.Select(a => a).ToArray());
        }

        public static Point getRandomPoint(Random rand, double[] start, double[] stop)
        {
            double[] point = new double[start.Length];
            for (int i = 0; i < point.Length; i++)
                point[i] = rand.NextDouble() * (stop[i] - start[i]) + start[i];

            return new Point(point);
        }
    }
}
