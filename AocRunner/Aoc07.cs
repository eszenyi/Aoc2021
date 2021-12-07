using Helpers;

namespace AocRunner
{
    internal class Aoc07
    {
        /*
         * --- Day 7: The Treachery of Whales ---
         * https://adventofcode.com/2021/day/7
         *
         */

        private List<int> numbers;

        public Aoc07()
        {
            //numbers = Loader.LoadIntsSingleLine("inputs\\sample07-1.txt", ',').ToList();
            numbers = Loader.LoadIntsSingleLine("inputs\\day07.txt", ',').ToList();
        }


        public long RunPuzzle1()
        {
            numbers.Sort();
            var median = numbers[numbers.Count / 2];
            int result = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                result += Math.Abs(numbers[i] - median);
            }

            return result;
        }

        public long RunPuzzle2()
        {
            var dict = new Dictionary<int, int>();
            for (int i = numbers.Min(); i < numbers.Max(); i++)
            {
                int steps = 0;
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (numbers[j] != i)
                    {
                        double increment = Math.Abs(numbers[j] - i);
                        steps += (int)((increment * increment / 2) + (increment / 2));
                    }
                }
                dict.Add(i, steps);
            }

            return dict.Values.Min();
        }
    }
}