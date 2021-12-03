using Helpers;
using System.Text;

namespace AocRunner
{
    internal class Aoc03
    {
        readonly string[] lines;

        public Aoc03()
        {
            lines = File.ReadAllLines("inputs\\day03.txt");
        }

        /*
         * --- Day 3: Binary Diagnostic ---
         * https://adventofcode.com/2021/day/3
         *
         */

        public long RunPuzzle1()
        {
            var numDigits = lines[0].Length;

            var gamma = new StringBuilder();
            var epsilon = new StringBuilder();

            for (int i = 0; i < numDigits; i++)
            {
                var ones = 0;
                foreach (var line in lines)
                {
                    if (line[i] == '1')
                    {
                        ones++;
                    }
                }
                if (ones > lines.Length / 2)
                {
                    gamma.Append('1');
                    epsilon.Append('0');
                }
                else
                {
                    gamma.Append('0');
                    epsilon.Append('1');
                }
            }

            return Binary.BinaryToLong(gamma.ToString()) * Binary.BinaryToLong(epsilon.ToString());
        }

        public long RunPuzzle2()
        {
            var oxygenGenerator = FindRating(lines.ToList(), 0, oxygenGenerator: true);
            var co2Scrubber = FindRating(lines.ToList(), 0, oxygenGenerator: false);

            return Binary.BinaryToLong(oxygenGenerator[0]) * Binary.BinaryToLong(co2Scrubber[0]);
        }

        private List<string> FindRating(List<string> diagnostics, int bitIdx, bool oxygenGenerator)
        {
            var ones = new List<string>();
            var zeros = new List<string>();
            foreach (var diag in diagnostics)
            {
                if (diag[bitIdx] == '1')
                {
                    ones.Add(diag);
                }
                else
                {
                    zeros.Add(diag);
                }
            }

            if ((oxygenGenerator && ones.Count >= zeros.Count) ||
                (!oxygenGenerator && ones.Count < zeros.Count))
            {
                if (ones.Count == 1)
                {
                    return ones;
                }

                return FindRating(ones, bitIdx + 1, oxygenGenerator);
            }
            else
            {
                if (zeros.Count == 1)
                {
                    return zeros;
                }

                return FindRating(zeros, bitIdx + 1, oxygenGenerator);
            }
        }
    }
}