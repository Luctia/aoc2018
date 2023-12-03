using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2018.solutions
{
    internal class Day05 : Day
    {
        public override void Part1()
        {
            string polymer = GetInput();
            Answer(GetReactedLength(polymer));
        }

        public override void Part2()
        {
            string polymer = GetInput();
            int minLength = int.MaxValue;
            char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            foreach (char letter in letters)
            {
                string newPolymer = polymer.Replace(letter.ToString().ToUpper(), "");
                newPolymer = newPolymer.Replace(letter.ToString().ToLower(), "");
                int reactedLength = GetReactedLength(newPolymer);
                if (reactedLength < minLength)
                {
                    minLength = reactedLength;
                }
            }
            Answer(minLength);
        }

        private int GetReactedLength(string polymer)
        {
            bool changes = true;
            while (changes)
            {
                changes = false;
                for (int i = 0; i < polymer.Length - 1; i++)
                {
                    if (polymer[i].ToString().ToLower() == polymer[i + 1].ToString().ToLower() && polymer[i] != polymer[i + 1])
                    {
                        changes = true;
                        polymer = polymer.Substring(0, i) + polymer.Substring(i + 2);
                    }
                }
            }
            return polymer.Length;
        }
    }
}
