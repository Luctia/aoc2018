using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2018
{
    internal abstract class Day
    {
        public abstract void Part1();
        public abstract void Part2();

        protected string GetInput(string type = null) => File.ReadAllText($"inputs\\{GetType().Name}{type}.txt");

        protected string[] GetInputLines(string type = null) => GetInput(type).Split("\r\n");

        protected void Answer(object answer)
        {
            Clipboard.SetText(answer.ToString());
            Console.WriteLine(GetType().Name + ": " +  answer);
        }
    }
}
