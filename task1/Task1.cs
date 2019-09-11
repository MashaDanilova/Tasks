using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask
{
    class Task1
    {
        public double Percentile(List<short> input)
        {
            input.Sort();
            var index = 0.9 * (input.Count - 1) + 1;
            var x = index % 1;
            int n = (int)index;
            return (input[n - 1] + x * (input[n] - input[n - 1]));
        }

        public double Median(List<short> input)
        {
            int count = input.Count();
            input.Sort();
            if (count % 2 == 0)
            {
                return (input[count / 2] + input[count / 2 - 1]) / 2;
            }
            return (input[count / 2 - 1]);
        }

        public double MaxValue(List<short> input)
        {
            return input.Max();
        }

        public double MinValue(List<short> input)
        {
            return input.Min();
        }

        public double MeanValue(List<short> input)
        {
            var result = 0.0;
            foreach (var value in input)
            {
                result += value;
            }
            return result / input.Count();
        }
    }
}
