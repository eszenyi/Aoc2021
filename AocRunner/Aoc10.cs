namespace AocRunner
{
    internal class Aoc10
    {
        /*
         * --- Day 10: Syntax Scoring ---
         * https://adventofcode.com/2021/day/10
         *
         */

        readonly string[] lines;

        public Aoc10()
        {
            //lines = File.ReadAllLines("inputs\\sample10-1.txt");
            lines = File.ReadAllLines("inputs\\day10.txt");
        }

        public long RunPuzzle1()
        {
            var illegalValues = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

            var sumIllegal = 0;
            foreach (var line in lines)
            {
                var result = IsBalanced(line);
                if (result.Item1 == BalanceResult.Corrupted && illegalValues.ContainsKey(result.Item2))
                {
                    sumIllegal += illegalValues[result.Item2];
                }
            }

            return sumIllegal;
        }

        public long RunPuzzle2()
        {
            var bracketPoints = new Dictionary<char, int> { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
            var completionValues = new List<long>();

            foreach (var line in lines)
            {
                var result = IsBalanced(line);
                if (result.Item1 == BalanceResult.Incomplete)
                {
                    // correct, but incomplete line
                    var chars = MakeBalanced(line);
                    long value = 0;
                    foreach (char c in chars)
                    {
                        value *= 5;
                        value += bracketPoints[c];
                    }
                    completionValues.Add(value);
                }
            }

            completionValues.Sort();
            return completionValues[completionValues.Count / 2];
        }

        private enum BalanceResult
        {
            Balanced,
            Corrupted,
            Incomplete
        }

        private static (BalanceResult, char) IsBalanced(string s)
        {
            var stack = new Stack<char>();
            var closing = new Dictionary<char, char> { { '}', '{' }, { ')', '(' }, { ']', '[' }, { '>', '<' } };
            var opening = new List<char> { '{', '(', '[', '<' };

            foreach (char ch in s)
            {
                if (closing.ContainsKey(ch) && stack.Count == 0)
                {
                    return (BalanceResult.Corrupted, default(char));
                }

                if (opening.Contains(ch))
                {
                    stack.Push(ch);
                }

                if (closing.ContainsKey(ch))
                {
                    var fromStack = stack.Pop();
                    if (fromStack != closing[ch])
                    {
                        return (BalanceResult.Corrupted, ch);
                    }
                }
            }

            return stack.Count == 0 ? (BalanceResult.Balanced, default(char)) : (BalanceResult.Incomplete, default(char));
        }

        private static List<char> MakeBalanced(string s)
        {
            var stack = new Stack<char>();
            var closing = new Dictionary<char, char> { { '}', '{' }, { ')', '(' }, { ']', '[' }, { '>', '<' } };
            var opening = new Dictionary<char, char> { { '{', '}' }, { '(', ')' }, { '[', ']' }, { '<', '>' } };

            foreach (char ch in s)
            {
                if (opening.ContainsKey(ch))
                {
                    stack.Push(ch);
                }

                if (closing.ContainsKey(ch))
                {
                    stack.Pop();
                }
            }

            var chars = new List<char>();
            while (stack.Count > 0)
            {
                chars.Add(opening[stack.Pop()]);
            }

            return chars;
        }
    }
}