using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Task2
{
    class Task2
    {
        // получаем координаты точки
        public float GetXY(string input, out float y) 
        {
            input = Regex.Replace(input, @"\\n", " ");
            string[] tmp = input.Split(' ');
            float x = float.Parse(tmp[0]);
            y = float.Parse(tmp[1]);
            return x;
        }
        // является ли точка вершиной четырёхугольника
        public int CheckPoint(List<string> quad, string point) 
        {
            var quadcoord = new Coordinates();
            var pointcoord = new Coordinates();
            pointcoord.x = GetXY(point, out pointcoord.y);
            foreach (var coordset in quad)
            {
                quadcoord.x = GetXY(coordset, out quadcoord.y);
                if (quadcoord.x == pointcoord.x && quadcoord.y == pointcoord.y)
                {
                    return 0;
                }
                else
                {
                    continue;
                }
            }
            return -1;
        }
        // принадлежит ли точка отрезку-ребру четырёхугольника
        public bool IsSide(float x1, float x2, float y1, float y2, float a, float b) 
        {
            if (((a - x1)* (y2 - y1)) == ((b-y1)* (x2 - x1)) && (a<=x2) && (a>=x1) && (y1<=b) && (y2>=b))
            {
                return true;
            }
            return false;
        }
        // лежит ли точка на стороне четырёхугольника
        public int CheckSide(List<string> quad, string point) 
        {
            var pointcoord = new Coordinates();
            pointcoord.x = GetXY(point, out pointcoord.y);
            var quadcoords = new List<Coordinates>();
            foreach (var coordset in quad)
            {
                var tmp = new Coordinates();
                tmp.x = GetXY(coordset, out tmp.y);
                quadcoords.Add(tmp);
            }
            if (IsSide(quadcoords[0].x, quadcoords[0].y, quadcoords[1].x, quadcoords[1].y, pointcoord.x, pointcoord.y) || IsSide(quadcoords[1].x, quadcoords[1].y, quadcoords[2].x, quadcoords[2].y, pointcoord.x, pointcoord.y) || IsSide(quadcoords[2].x, quadcoords[2].y, quadcoords[3].x, quadcoords[3].y, pointcoord.x, pointcoord.y) || IsSide(quadcoords[3].x, quadcoords[3].y, quadcoords[0].x, quadcoords[0].y, pointcoord.x, pointcoord.y))
            {
                return 1;
            }
            return -1;
        }
        //площадь четырёхугольника - формула Гаусса
        public float QuadSquare(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4) 
        {
            float sq = Math.Abs(x1 * y2 + x2 * y3 + x3 * y4 + x4 * y1 - x2 * y1 - x3 * y2 - x4 * y3 - x1 * y4);
            float result = sq / 2;
            return result;
        }
        //площадь треугольника - формула Гаусса
        public float TriSquare(float x1, float y1, float x2, float y2, float x3, float y3) 
        {
            float sq = Math.Abs(x1 * y2 + x2 * y3 + x3 * y1 - x2 * y1 - x3 * y2 - x1 * y3);
            float result = sq / 2;
            return result;
        }
        public int CheckInside(List<string> quad, string point)
        {
            var pointcoord = new Coordinates();
            pointcoord.x = GetXY(point, out pointcoord.y);
            var quadcoords = new List<Coordinates>();
            foreach (var coordset in quad)
            {
                var tmp = new Coordinates();
                tmp.x = GetXY(coordset, out tmp.y);
                quadcoords.Add(tmp);
            }
            // если площадь исходного четырёхугольника равна сумме площадей треугольников со сторонами-рёбрами четырёхугольника и вершиной в рассматриваемой точке, то эта точка лежит внутри четырёхугольника
            float qsquare = QuadSquare(quadcoords[0].x, quadcoords[0].y, quadcoords[1].x, quadcoords[1].y, quadcoords[2].x, quadcoords[2].y, quadcoords[3].x, quadcoords[3].y);
            float tsquare1 = TriSquare(quadcoords[0].x, quadcoords[0].y, quadcoords[1].x, quadcoords[1].y, pointcoord.x, pointcoord.y);
            float tsquare2 = TriSquare(quadcoords[1].x, quadcoords[1].y, quadcoords[2].x, quadcoords[2].y, pointcoord.x, pointcoord.y);
            float tsquare3 = TriSquare(quadcoords[2].x, quadcoords[2].y, quadcoords[3].x, quadcoords[3].y, pointcoord.x, pointcoord.y);
            float tsquare4 = TriSquare(quadcoords[3].x, quadcoords[3].y, quadcoords[0].x, quadcoords[0].y, pointcoord.x, pointcoord.y);
            float sq = tsquare1 + tsquare2 + tsquare3 + tsquare4;
            if (qsquare == sq)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
