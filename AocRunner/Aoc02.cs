namespace AocRunner
{
    internal class Aoc02
    {
        readonly string[] lines;

        public Aoc02()
        {
            lines = File.ReadAllLines("inputs\\day02.txt");
        }

        public int RunPuzzle1()
        {
            var horizontal = 0;
            var vertical = 0;

            foreach (var line in lines)
            {
                var parsed = ParseLine(line);
                switch (parsed.Item1)
                {
                    case "forward":
                        horizontal += parsed.Item2;
                        break;
                    case "down":
                        vertical += parsed.Item2;
                        break;
                    case "up":
                        vertical -= parsed.Item2;
                        break;
                }
            }

            return horizontal * vertical;
        }

        public int RunPuzzle2()
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var line in lines)
            {
                var parsed = ParseLine(line);
                switch (parsed.Item1)
                {
                    case "forward":
                        horizontal += parsed.Item2;
                        depth += aim * parsed.Item2;
                        break;
                    case "down":
                        aim += parsed.Item2;
                        break;
                    case "up":
                        aim -= parsed.Item2;
                        break;
                }
            }

            return horizontal * depth;
        }

        private static (string, int) ParseLine(string line)
        {
            string direction = line.Split(' ')[0];
            int amount = int.Parse(line.Split(' ')[1]);
            return (direction, amount);
        }
    }
}