namespace AocRunner
{
    internal class Aoc13
    {
        /*
         * --- Day 13: Transparent Origami ---
         * https://adventofcode.com/2021/day/13
         *
         */

        List<Point> points = new();
        readonly List<Tuple<string, int>> folds = new();

        public Aoc13()
        {
            //var input = File.ReadAllLines("inputs\\sample13-1.txt");
            var input = File.ReadAllLines("inputs\\day13.txt");

            int idx = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var xy = line.Split(',');
                points.Add(new Point { X = int.Parse(xy[0]), Y = int.Parse(xy[1]) });
                idx++;
            }

            for (int i = idx + 1; i < input.Length; i++)
            {
                var fld = input[i].Split('=');
                folds.Add(new Tuple<string, int>(fld[0][(fld[0].Length - 1)..], int.Parse(fld[1])));
            }
        }
        public long RunPuzzle1()
        {
            var firstFold = folds[0];
            if (firstFold.Item1 == "x")
            {
                FoldLeft(firstFold.Item2);
            }
            else
            {
                FoldUp(firstFold.Item2);
            }

            return points.Count;
        }

        public long RunPuzzle2()
        {
            foreach (var fold in folds)
            {
                if (fold.Item1 == "x")
                {
                    FoldLeft(fold.Item2);
                }
                else
                {
                    FoldUp(fold.Item2);
                }
            }

            Console.Clear();

            // print points
            foreach (var point in points)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write("#");
            }

            Console.WriteLine();

            return 0;
        }

        private void FoldUp(int whereToFold)
        {
            var newPoints = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < whereToFold)
                {
                    newPoints.Add(new Point(points[i].X, points[i].Y));
                }
            }
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y > whereToFold)
                {
                    var newPoint = new Point(points[i].X, whereToFold - (points[i].Y - whereToFold));
                    if (!newPoints.Contains(newPoint))
                    {
                        newPoints.Add(newPoint);
                    }
                }
            }

            points = newPoints;
        }

        private void FoldLeft(int whereToFold)
        {
            var newPoints = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < whereToFold)
                {
                    newPoints.Add(new Point(points[i].X, points[i].Y));
                }
            }

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X > whereToFold)
                {
                    var newPoint = new Point(whereToFold - (points[i].X - whereToFold), points[i].Y);
                    if (!newPoints.Contains(newPoint))
                    {
                        newPoints.Add(newPoint);
                    }
                }
            }

            points = newPoints;
        }
    }
}