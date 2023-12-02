using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2018.solutions
{
    internal class Day02 : Day
    {
        public override void Part1()
        {
            string[] lines = GetInputLines();
            int twos = 0;
            int threes = 0;
            foreach (string line in lines)
            {
                Box box = new Box(line);
                if (box.HasTwo())
                {
                    twos++;
                }
                if (box.HasThree())
                {
                    threes++;
                }
            }
            Answer(twos * threes);
        }

        public override void Part2()
        {
            string[] lines = GetInputLines();
            Box[] boxes = lines.Select(line => new Box(line)).ToArray();
            for (int i = 0; i < boxes.Length; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    if (boxes[i].IsOneDifferent(boxes[j]))
                    {
                        Answer(boxes[i].GetMutuals(boxes[j]));
                        return;
                    }
                }
            }
        }

        private class Box
        {
            string id;
            Dictionary<char, int> characterMapping;

            public Box(string id)
            {
                this.id = id;
                this.characterMapping = new Dictionary<char, int>();
                foreach (var item in id)
                {
                    if (characterMapping.ContainsKey(item))
                    {
                        characterMapping[item] = characterMapping[item] + 1;
                    } else
                    {
                        characterMapping.Add(item, 1);
                    }
                }
            }

            public bool HasTwo()
            {
                return characterMapping.ContainsValue(2);
            }

            public bool HasThree()
            {
                return characterMapping.ContainsValue(3);
            }

            public bool IsOneDifferent(Box otherBox)
            {
                int difference = 0;
                for (int i = 0; i < otherBox.id.Length; i++)
                {
                    if (otherBox.id[i] != this.id[i] && ++difference > 1)
                    {
                        return false;
                    }
                }
                return difference == 1;
            }

            public string GetMutuals(Box otherBox)
            {
                string mutuals = "";
                for (int i = 0; i < otherBox.id.Length; i++)
                {
                    if (this.id[i] == otherBox.id[i])
                    {
                        mutuals += this.id[i];
                    }
                }
                return mutuals;
            }

            public override string ToString()
            {
                return this.id;
            }
        }
    }
}
