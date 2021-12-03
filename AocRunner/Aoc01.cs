using Helpers;

namespace AocRunner
{
    internal class Aoc01
    {
        readonly int[] numbers;

        public Aoc01()
        {
            numbers = Loader.LoadInts("inputs\\day01.txt");
        }

        /*
         * --- Day 1: Sonar Sweep ---
         * https://adventofcode.com/2021/day/1
         *
         */

        public int RunPuzzle1()
        {
            int prev = int.MinValue;
            int result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                var sum = numbers[i];
                if (i > 0)
                {
                    if (sum > prev)
                        result++;
                }
                prev = sum;
            }

            return result;
        }

        public int RunPuzzle2()
        {
            int prev = int.MinValue;
            int result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (i + 2 < numbers.Length)
                {
                    var sum = numbers[i] + numbers[i + 1] + numbers[i + 2];
                    if (i > 0)
                    {
                        if (sum > prev)
                            result++;
                    }
                    prev = sum;
                }
            }

            return result;
        }
    }
}