using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolokwium
{
    public class Functions
    {
        static public double Shubert(params double[] arg)
        {
            int n = arg.Length;
            double y = 1;
            for (int i = 0; i < n; i++)
            {
                double tmp = 0;
                for (int j = 0; j < 5; j++)
                    tmp += j * Math.Cos((1 + j) * arg[i]);
                y *= tmp;
            }
            return y;
        }
        static public double Kolokwium(params double[] arg)
        {
            int n = arg.Length;
            double y = 0;
            for (int i = 0; i < n; i++)
            {
                y += Math.Pow(2,arg[i]);
            }
            return y*y;
        }
    }
}
