using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int CashierCount = 5;

            if (args.Length == 0)
            {
                Console.WriteLine("Empty/insufficient args");
                Environment.Exit(0);
            }

            var fm = new FileManager();
            // для хранения суммы интервалов
            var intervals = new double[16];
            // для хранения содержимого файла
            var tempList = new List<double>(); 

            for (var i = 0; i < CashierCount; i++)
            {
                // считываем значения интервалов из файла "Cashi.txt"
                tempList = fm.GetLines(path: args[0] + "Cash" + (i+1).ToString() + ".txt"); 
                for (var j = 0; j < intervals.Length; j++)
                {
                    // суммы интервалов с 5 касс
                    intervals[j] += tempList[j]; 
                }
            }
            double max = intervals.Max();
            Console.WriteLine(Array.IndexOf(intervals, max)+1);
        }
    }
}
