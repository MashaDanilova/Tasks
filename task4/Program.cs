using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    public class TimeInterval
    {
        public readonly TimeSpan startTime;
        public readonly TimeSpan endTime;
        public TimeInterval(string startTime, string endTime)
        {
            this.startTime = new TimeSpan(Convert.ToInt32(startTime.Split(':')[0]), Convert.ToInt32(startTime.Split(':')[1]), 00);
            this.endTime = new TimeSpan(Convert.ToInt32(endTime.Split(':')[0]), Convert.ToInt32(endTime.Split(':')[1]), 00);
        }
        public TimeInterval(TimeSpan startTime, TimeSpan endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Empty/insufficient args");
                Console.ReadKey();
                Environment.Exit(0);
            }
           
            var fm = new FileManager();
            var visitorTimes = fm.GetLines(path: args[0]);
            visitorTimes = SortTimes(visitorTimes); 
            TimeSpan currentTimeLinePos = new TimeSpan(0, 0, 0);
            // "числовая прямая" минут работы банка
            int timeLineLength = 60 * 12;

            List<TimeInterval> incIntervals = new List<TimeInterval>();
            // хранит список списков (incIntervals) интервалов вхождения в них точки - для каждой из точек прямой timeLineLength
            List<List<TimeInterval>> intervalList = new List<List<TimeInterval>>(); 

            // проходим по всей часловой прямой поминутно 
            for (var i = 1; i < timeLineLength; i++)
            {
                // текущая точка на числовой прямой
                currentTimeLinePos = new TimeSpan(8, i, 0);
                // если текущая точка принадлежит интервалу из файла - добавляем этот интервал в список
                for (var j = 0; j < visitorTimes.Count; j++)
                {
                    if (currentTimeLinePos > visitorTimes[j].startTime && currentTimeLinePos < visitorTimes[j].endTime)
                    {
                        incIntervals.Add(visitorTimes[j]);
                    }
                }
                if (incIntervals.Count > 0)
                {
                    intervalList.Add(new List<TimeInterval>(incIntervals));
                    incIntervals.Clear();
                }
            }

            // находим максимальное количество "пересечений" интервалов 
            int max = -1;
            for (int i = 0; i < intervalList.Count; i++)
            {
                int tmp = intervalList[i].Count;
                if (tmp > max)
                {
                    max = tmp;
                }
            }
            // заполняем список maxIndex индексами элементов (интервалов), которые содержат наибольшее количество посетителей (max)
            var maxIndex = new List<int>();
            for (int i = 0; i < intervalList.Count; i++)
            {
                if (intervalList[i].Count == max)
                {
                    maxIndex.Add(i);
                }
            }
            // заполняем список times элементами (интервалами), которые соответствуют индексам maxIndex
            var times = new List<List<TimeInterval>>();
            for (int i = 0; i < maxIndex.Count; i++)
            {
                times.Add(intervalList[maxIndex[i]]);
            }
            // для всех элементов каждого подсписка times ищем пересечение интервалов и записываем полученный интервал в result
            List<TimeInterval> result = new List<TimeInterval>();
            foreach (var item in times)
            {
                List<TimeInterval> tmp = item;
                TimeSpan start = tmp[tmp.Count-1].startTime;
                TimeSpan min = new TimeSpan(20, 0, 0);
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i].endTime < min)
                    {
                        min = tmp[i].endTime;
                    }
                }
                result.Add(new TimeInterval(start, min));
            }

            List<TimeInterval> unique = Distinct(result);
            List<TimeInterval> final = new List<TimeInterval>();
            // "склеиваем" нужные интервалы - например, интервалы 08:15 08:30, 08:30 08:45, 08:45 09:00 в интервал 08:15 09:00
            for (int i = 0; i < unique.Count-1; i++)
            {
                if (unique[i].endTime == unique[i+1].startTime)
                {
                    unique[i+1] = new TimeInterval(unique[i].startTime, unique[i + 1].endTime);
                }
            }
            // выбираем нужные нам интервалы - это самый поздний startTime 
            TimeSpan startInt = unique[0].startTime;
            for (int i = 0; i < unique.Count; i++)
            {
                if (unique[i].startTime == startInt)
                {
                    var interval = unique.Where(x => x.startTime == startInt).Last();
                    if (!final.Contains(interval))
                    {
                        final.Add(interval);
                    }
                }
                else
                {
                    startInt = unique[i].startTime;
                    var interval = unique.Where(x => x.startTime == startInt).Last();
                    if (!final.Contains(interval))
                    {
                        final.Add(interval);
                    }
                }
            }

            foreach (var item in final)
            {
                Console.WriteLine(item.startTime.ToString(@"hh\:mm") + " " + item.endTime.ToString(@"hh\:mm") + @"\n");
            }

            Console.ReadKey();
        }
        // сортировка списка интервалов по значению начала интервала
        public static List<TimeInterval> SortTimes(List<TimeInterval> list)
        {
            var count = list.Count;
            TimeInterval tmp;
            for (int i = 0; i < count; i++)
            {
                for (int j = count-1; j > i; j--)
                {
                    if (list[j-1].startTime > list[j].startTime)
                    {
                        tmp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = tmp;
                    }
                }
            }
            return list;
        }
        // выбор уникальных значений из списка интервалов
        public static List<TimeInterval> Distinct(List<TimeInterval> interval)
        {
            List<TimeInterval> unique = new List<TimeInterval>();
            TimeSpan start = TimeSpan.Zero;
            TimeSpan end = TimeSpan.Zero;
            for (int i = 0; i < interval.Count; i++)
            {
                if (interval[i].startTime!=start && interval[i].endTime!=end)
                {
                    start = interval[i].startTime;
                    end = interval[i].endTime;
                    unique.Add(interval[i]);
                }
                else
                {
                    continue;
                }
            }
            return unique;
        }
    }
}
