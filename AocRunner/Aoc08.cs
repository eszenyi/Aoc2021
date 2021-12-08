namespace AocRunner
{
    internal class Aoc08
    {
        /*
         * --- Day 8: Seven Segment Search ---
         * https://adventofcode.com/2021/day/8
         *
         */

        private string[] lines;

        public Aoc08()
        {
            //lines = File.ReadAllLines("inputs\\sample08-1.txt");
            //lines = File.ReadAllLines("inputs\\sample08-2.txt");
            lines = File.ReadAllLines("inputs\\day08.txt");
        }

        public long RunPuzzle1()
        {
            var outputs = new List<string>();

            foreach (var line in lines)
            {
                var segments = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                segments[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(s => outputs.Add(s));
            }

            return outputs.Count(o => o.Length < 5 || o.Length > 6);
        }

        public long RunPuzzle2()
        {
            // holds the numbers 0-9
            var numberPatterns = new string[10];
            const string wires = "abcdefg";

            long result = 0;

            /*
             * 
             *
             * 0:      1:      2:      3:      4:
             *  aaaa    ....    aaaa    aaaa    ....
             * b    c  .    c  .    c  .    c  b    c
             * b    c  .    c  .    c  .    c  b    c
             * ....    ....    dddd    dddd    dddd
             * e    f  .    f  e    .  .    f  .    f
             * e    f  .    f  e    .  .    f  .    f
             *  gggg    ....    gggg    gggg    ....
             * 
             * 5:      6:      7:      8:      9:
             *  aaaa    aaaa    aaaa    aaaa    aaaa
             * b    .  b    .  .    c  b    c  b    c
             * b    .  b    .  .    c  b    c  b    c
             *  dddd    dddd    ....    dddd    dddd
             * .    f  e    f  .    f  e    f  .    f
             * .    f  e    f  .    f  e    f  .    f
             *  gggg    gggg    ....    gggg    gggg
             * 
             * 
             * Segments:
             *    0
             *   1 2
             *    3
             *   4 5
             *    6
             */

            foreach (var line in lines)
            {
                var temp = line.Split('|', StringSplitOptions.RemoveEmptyEntries);

                // holds the 10 patterns (changes in every line)
                var patterns = temp[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // holds the 4 outputs
                var outputs = temp[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // store the unique patterns for their corresponding numbers
                numberPatterns[1] = patterns.Single(p => p.Length == 2);
                numberPatterns[4] = patterns.Single(p => p.Length == 4);
                numberPatterns[7] = patterns.Single(p => p.Length == 3);
                numberPatterns[8] = patterns.Single(p => p.Length == 7);

                // deduce the rest of the numbers
                var segments = new Dictionary<int, List<char>>();
                segments[0] = new List<char>(numberPatterns[7].Except(numberPatterns[1]));
                segments[2] = new List<char>(numberPatterns[1]);
                segments[5] = new List<char>(numberPatterns[1]);
                segments[1] = new List<char>(numberPatterns[4].Except(numberPatterns[1]));
                segments[3] = new List<char>(numberPatterns[4].Except(numberPatterns[1]));

                numberPatterns[6] = patterns.Single(p => p.Length == 6 && (!p.Contains(segments[2][0]) || !p.Contains(segments[2][1])));
                segments[2] = new List<char>(wires.Except(numberPatterns[6]));
                segments[5] = new List<char>(segments[5].Except(segments[2]));

                numberPatterns[0] = patterns.Single(p => p.Length == 6 && (!p.Contains(segments[1][0]) || !p.Contains(segments[1][1])));
                segments[3] = new List<char>(wires.Except(numberPatterns[0]));
                segments[1] = new List<char>(segments[1].Except(segments[3]));

                numberPatterns[9] = patterns.Single(p => p.Length == 6 && p != numberPatterns[0] && p != numberPatterns[6]);
                segments[4] = new List<char>(wires.Except(numberPatterns[9]));

                var usedWires = new List<char>();
                foreach (var segment in segments.Where(s => s.Key != 6))
                {
                    usedWires.AddRange(segment.Value.Select(s => s));
                }
                segments[6] = new List<char>(wires.Except(usedWires));

                numberPatterns[2] = patterns.Single(p => p.Length == 5 &&
                p.Contains(segments[0][0]) &&
                p.Contains(segments[2][0]) &&
                p.Contains(segments[3][0]) &&
                p.Contains(segments[4][0]) &&
                p.Contains(segments[6][0]));

                numberPatterns[3] = patterns.Single(p => p.Length == 5 &&
                p.Contains(segments[0][0]) &&
                p.Contains(segments[2][0]) &&
                p.Contains(segments[3][0]) &&
                p.Contains(segments[5][0]) &&
                p.Contains(segments[6][0]));

                numberPatterns[5] = patterns.Single(p => p.Length == 5 &&
                p.Contains(segments[0][0]) &&
                p.Contains(segments[1][0]) &&
                p.Contains(segments[3][0]) &&
                p.Contains(segments[5][0]) &&
                p.Contains(segments[6][0]));

                // decode the digits
                int digits = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < numberPatterns.Length; j++)
                    {
                        if (outputs[i].Length == numberPatterns[j].Length && IsAnagram(outputs[i], numberPatterns[j]))
                        {
                            digits += (int)Math.Pow(10, 3 - i) * j;
                        }
                    }
                }

                result += digits;
            }

            return result;
        }

        private static bool IsAnagram(string str1, string str2)
        {
            return !str1.Except(str2).Any();
        }
    }
}