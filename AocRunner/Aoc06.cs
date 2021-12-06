using Helpers;

namespace AocRunner
{
    internal class Aoc06
    {
        /*
         * --- Day 6: Lanternfish ---
         * https://adventofcode.com/2021/day/6
         *
         */

        private int[] numbers;

        public Aoc06()
        {
            //numbers = Loader.LoadIntsSingleLine("inputs\\sample06-1.txt", ',');
            numbers = Loader.LoadIntsSingleLine("inputs\\day06.txt", ',');
        }

        const int newFishInitial = 8;
        const int oldFishReset = 6;

        public long RunPuzzle1()
        {
            for (int days = 0; days < 80; days++)
            {
                var newList = new List<int>();
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == 0)
                    {
                        numbers[i] = oldFishReset;
                        newList.Add(newFishInitial);
                        continue;
                    }

                    numbers[i] -= 1;
                }

                if (newList.Any())
                {
                    int startIdx = numbers.Length;
                    Array.Resize(ref numbers, numbers.Length + newList.Count);
                    foreach (var newItem in newList)
                    {
                        numbers[startIdx++] = newItem;
                    }
                }
            }

            return numbers.Length;
        }

        public long RunPuzzle2()
        {
            var dict = new Dictionary<int, long> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
            foreach (var num in numbers)
            {
                dict[num]++;
            }

            for (int days = 0; days < 256; days++)
            {
                dict = DoStep(dict);
            }

            return dict.Values.Sum();
        }

        private static Dictionary<int, long> DoStep(Dictionary<int, long> dict)
        {
            var newValues = new Dictionary<int, long>();
            foreach (var kvp in dict)
            {
                var newKey = kvp.Key - 1;
                if (newKey == -1)
                {
                    if (kvp.Value > 0)
                    {
                        newValues[newFishInitial] = kvp.Value;
                    }

                    newKey = oldFishReset;
                }

                if (newValues.ContainsKey(newKey))
                {
                    newValues[newKey] += kvp.Value;
                }
                else
                {
                    newValues[newKey] = kvp.Value;
                }
            }
            return newValues;
        }
    }
}