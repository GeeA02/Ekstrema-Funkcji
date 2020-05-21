using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolokwium
{
    class Genetic
    {
        private int gen = 200;
        private int population = 5;
        private int xMin = -10;
        private int xMax = 10;
        private int xToCross;
        private List<Point> xList;
        private double[] xStart;
        private double[] xStop;
        private int dim;
        public string Result{get;set;}

        public Genetic(int dim, Func<double[], double> function, bool Max)
        {
            this.xToCross = Convert.ToInt32(population * 0.9);
            this.dim = dim;
            this.Result = Algorithm(function, Max);
        }

        public Genetic(int dim, Func<double[], double> function, int min, int max, bool Max, int population, int generations)
        {
            this.xMin = min;
            this.xMax = max;
            this.xToCross = Convert.ToInt32(population * 0.9);
            this.dim = dim;
            this.population = population;
            this.gen = generations;
            this.Result = Algorithm(function, Max);
        }

        string Algorithm(Func<double[], double> function, bool Max)
        {
            string result="";
            xStart = new double[dim];
            xStop = new double[dim];

            for (int i = 0; i < dim; i++)
            {
                xStart[i] = xMin;
                xStop[i] = xMax;
            }
            Random random = new Random();
            xList = Enumerable.Range(0, population).Select(n => Point.getRandomPoint(random, xStart, xStop)).ToList();
            if(Max)
                xList = sortMax(xList, function);
            else
                xList = sortMin(xList, function);

            int generation = 0;

            while (generation < gen)
            {
                for (int j = 0; j < xToCross; j++)
                {
                    int i;
                    do
                    {
                        i = random.Next(0, xToCross);
   
                    } while (i == j);

                    if (random.NextDouble() < 0.5)
                        xList.Add(Mutation(Crossing(xList[i], xList[j])));
                    else
                        xList.Add(Crossing(xList[i], xList[j]));
                }
                if (Max)
                    xList = sortMax(xList, function);
                else
                    xList = sortMin(xList, function);

                xList.RemoveRange(population, xList.Count - population);

                //Console.WriteLine($"\nGeneration: {generation}");
                //for (int i = 0; i < xList.Count; i++)
                //    Console.WriteLine($"{i + 1}. f({String.Join(",", xList[i].arg.Select(x => x).ToArray())}) = {function(xList[i].arg)}");
                //Console.WriteLine();
                result += $"Generacja: {generation + 1}\n";
                if (Max)
                    result += $"Fmax({xList[0]}) = {function(xList[0].arg)}\n";
                else
                    result += $"Fmin({xList[0]}) = {function(xList[0].arg)}\n";
                generation++;
            }
            return result;
        }


        private Point Crossing(Point mom, Point dad)
        {
            double[] args = new double[dim];
            Random rand = new Random();
            double p1 = rand.NextDouble();
            double p2 = 1 - p1;

            for (int i = 0; i < dim; i++)
            {
                args[i] = Math.Max(Math.Min(mom.arg[i] * p1 + dad.arg[i] * p2, xMax), xMin);
            }

            return new Point(args);
        }

        private Point Mutation(Point point)
        {
            double[] args = new double[dim];

            for (int i = 0; i < dim; i++)
            {
                Random rand = new Random();
                double mutationRate = rand.NextDouble() + 0.5;

                args[i] = Math.Min(point.arg[i] * mutationRate, xMax);
            }

            return new Point(args);
        }

        private List<Point> sortMax(List<Point> list, Func<double[], double> function)
        {
            List<double> values = list.Select(l => function(l.arg)).ToList();
            for (int i = 0; i < values.Count - 1; i++)
                for (int j = 0; j < values.Count - 1; j++)
                    if (values[j] < values[j + 1])
                    {
                        double temp = values[j];
                        Point p = list[j];

                        values[j] = values[j + 1];
                        list[j] = list[j + 1];

                        values[j + 1] = temp;
                        list[j + 1] = p;
                    }
            return list;
        }

        private List<Point> sortMin(List<Point> list, Func<double[], double> function)
        {
            List<double> values = list.Select(l => function(l.arg)).ToList();
            // czemu tu jest Count - 1
            for (int i = 0; i < values.Count - 1; i++)
                for (int j = 0; j < values.Count - 1; j++)
                    if (values[j] > values[j + 1])
                    {
                        double temp = values[j];
                        Point p = list[j];

                        values[j] = values[j + 1];
                        list[j] = list[j + 1];

                        values[j + 1] = temp;
                        list[j + 1] = p;
                    }
            return list;
        }
    }
}
