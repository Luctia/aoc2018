using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2018.solutions
{
    internal class Day01 : Day
    {
        public override void Part1()
        {
            string[] input = GetInputLines();
            int sum = 0;
            foreach (string line in input)
            {
                sum += int.Parse(line);
            }
            Answer(sum);
        }

        public override void Part2()
        {
            string[] input = GetInputLines();
            List<int> frequencies = new List<int>();
            int sum = 0;
            while (true)
            {
                foreach (string line in input)
                {
                    sum += int.Parse(line);
                    if (frequencies.Contains(sum))
                    {
                        Answer(sum);
                        return;
                    }
                    frequencies.Add(sum);
                }
            }
        }
    }
}
