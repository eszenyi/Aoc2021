namespace AocRunner
{
    internal class Aoc14
    {
        /*
         * --- Day 14: Extended Polymerization ---
         * https://adventofcode.com/2021/day/14
         *
         */

        string template;
        IDictionary<string, char> rules = new Dictionary<string, char>();

        public Aoc14()
        {
            //var input = File.ReadAllLines("inputs\\sample14-1.txt");
            var input = File.ReadAllLines("inputs\\day14.txt");

            template = input[0];

            for (int i = 2; i < input.Length; i++)
            {
                var rule = input[i].Split(" -> ");
                rules.Add(rule[0], rule[1].ToCharArray()[0]);
            }
        }

        public long RunPuzzle1()
        {
            // initialise pairs
            InitialisePairs();

            for (int i = 0; i < 10; i++)
            {
                DoStep();
            }

            IDictionary<char, long> frequencies = CountFrequencies();
            var max = frequencies.Values.Max();
            var min = frequencies.Values.Min();

            return max - min;
        }

        public long RunPuzzle2()
        {
            // initialise pairs
            InitialisePairs();

            for (int i = 0; i < 40; i++)
            {
                DoStep();
            }

            IDictionary<char, long> frequencies = CountFrequencies();
            var max = frequencies.Values.Max();
            var min = frequencies.Values.Min();

            return max - min;
        }

        IDictionary<string, long> pairs = new Dictionary<string, long>();

        private void InitialisePairs()
        {
            for (int i = 0; i < template.Length - 1; i++)
            {
                var pair = $"{template[i]}{template[i + 1]}";
                if (pairs.ContainsKey(pair))
                {
                    pairs[pair]++;
                }
                else
                {
                    pairs.Add(pair, 1);
                }
            }
        }

        public void DoStep()
        {
            var changes = new Dictionary<string, long>();

            foreach (var pair in pairs)
            {
                if (!rules.ContainsKey(pair.Key))
                {
                    throw new InvalidOperationException("Error in rules");
                }

                // remove pair
                if (changes.ContainsKey(pair.Key))
                {
                    changes[pair.Key] -= pair.Value;
                }
                else
                {
                    changes[pair.Key] = pair.Value * -1;
                }

                // replace removed pair with two new pair
                string newPair1 = $"{pair.Key[0]}{rules[pair.Key]}";
                string newPair2 = $"{rules[pair.Key]}{pair.Key[1]}";
                if (changes.ContainsKey(newPair1))
                {
                    changes[newPair1] += pair.Value;
                }
                else
                {
                    changes.Add(newPair1, pair.Value);
                }
                if (changes.ContainsKey(newPair2))
                {
                    changes[newPair2] += pair.Value;
                }
                else
                {
                    changes.Add(newPair2, pair.Value);
                }
            }

            // apply changes after step
            foreach (var change in changes)
            {
                if (pairs.ContainsKey(change.Key))
                {
                    pairs[change.Key] += change.Value;
                }
                else
                {
                    pairs.Add(change.Key, change.Value);
                }
                if (pairs[change.Key] <= 0)
                {
                    pairs.Remove(change.Key);
                }
            }
        }

        private IDictionary<char, long> CountFrequencies()
        {
            var frequencies = new Dictionary<char, long>();
            foreach (var pair in pairs)
            {
                if (frequencies.ContainsKey(pair.Key[1]))
                {
                    frequencies[pair.Key[1]] += pair.Value;
                }
                else
                {
                    frequencies.Add(pair.Key[1], pair.Value);
                }
            }

            return frequencies;
        }
    }
}