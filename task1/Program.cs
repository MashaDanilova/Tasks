using System;
using System.Collections.Generic;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Empty args");
                Environment.Exit(0);
            }
            var task1 = new Task1();
            var fm = new FileManager();

            // входные числа 
            List<short> input = fm.GetLines(path: args[0]); 
            Console.WriteLine(string.Format("{0:0.00}", task1.Percentile(input)));
            Console.WriteLine(string.Format("{0:0.00}", task1.Median(input)));
            Console.WriteLine(string.Format("{0:0.00}", task1.MaxValue(input)));
            Console.WriteLine(string.Format("{0:0.00}", task1.MinValue(input)));
            Console.WriteLine(string.Format("{0:0.00}", task1.MeanValue(input)));
        }
    }
}
