namespace AocRunner
{
    internal class Aoc05
    {
        private List<Line> lines;
        private IDictionary<int, int> pointsCovered;
        private int width;

        /*
         * --- Day 5: Hydrothermal Venture ---
         * https://adventofcode.com/2021/day/5
         *
         */

        public Aoc05()
        {
            lines = new List<Line>();
            pointsCovered = new Dictionary<int, int>();

            //ParseInput("inputs\\sample05-1.txt");
            ParseInput("inputs\\day05.txt");
        }

        private void ParseInput(string input)
        {
            var maxX = int.MinValue;
            var inputLines = File.ReadAllLines(input);
            foreach (var rawLine in inputLines)
            {
                // e.g. 0,9 -> 5,9
                var points = rawLine.Split("->", StringSplitOptions.RemoveEmptyEntries);
                var point1 = points[0].Trim().Split(',');
                var point2 = points[1].Trim().Split(',');

                var x1 = int.Parse(point1[0]);
                var y1 = int.Parse(point1[1]);
                var x2 = int.Parse(point2[0]);
                var y2 = int.Parse(point2[1]);

                if (Math.Max(x1, x2) > maxX)
                {
                    maxX = Math.Max(x1, x2);
                }

                lines.Add(new Line(x1, y1, x2, y2));
            }

            width = maxX + 1;
        }

        public long RunPuzzle1()
        {
            foreach (var line in lines)
            {
                if (!line.IsStraight)
                    continue;

                foreach (var covered in line.PointsCovered)
                {
                    var key = CalculateKey(covered);
                    if (pointsCovered.ContainsKey(key))
                    {
                        pointsCovered[key]++;
                    }
                    else
                    {
                        pointsCovered[key] = 1;
                    }
                }
            }

            return pointsCovered
                .Values
                .Where(v => v > 1)
                .Count();
        }

        public long RunPuzzle2()
        {
            foreach (var line in lines)
            {
                foreach (var covered in line.PointsCovered)
                {
                    var key = CalculateKey(covered);
                    if (pointsCovered.ContainsKey(key))
                    {
                        pointsCovered[key]++;
                    }
                    else
                    {
                        pointsCovered[key] = 1;
                    }
                }
            }

            return pointsCovered
                .Values
                .Where(v => v > 1)
                .Count();
        }

        private int CalculateKey(Point p)
        {
            return p.Y * width + p.X;
        }
    }
}