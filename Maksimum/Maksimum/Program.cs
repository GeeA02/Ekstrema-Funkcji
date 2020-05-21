using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maksimum
{
    class Program
    {
        static void Main(string[] args)
        {
            Genetic genetic = new Genetic(2, Functions.Kolokwium, 0, 50, true);
            Console.Read();
        }
    }
}
