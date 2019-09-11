using System;
using System.Collections.Generic;

namespace Task2
{
    public struct Coordinates
    {
        public float x;
        public float y;
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Empty/insufficient args");
                Environment.Exit(0);
            }

            var fm = new FileManager();
            Task2 task = new Task2();
            var coords = new Coordinates();

            // вершины четырёхугольника
            List<string> Quad = fm.GetLines(path: args[0]);
            // точки для проверки
            List<string> Points = fm.GetLines(path: args[1]);

            // проверяем для каждой точки
            foreach (var point in Points) 
            {
                coords.x = task.GetXY(point, out coords.y);
                if (task.CheckPoint(Quad, point) == 0)
                {
                    Console.WriteLine(0 + @"\n");
                }
                else if (task.CheckSide(Quad, point) == 1)
                {
                    Console.WriteLine(1 + @"\n");
                }
                else if (task.CheckInside(Quad, point) == 2)
                {
                    Console.WriteLine(2 + @"\n");
                }
                else
                {
                    Console.WriteLine(3 + @"\n");
                }
            }
        }
    }


}
