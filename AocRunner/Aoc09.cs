using Helpers;

namespace AocRunner
{
    internal class Aoc09
    {
        /*
         * --- Day 9: Smoke Basin ---
         * https://adventofcode.com/2021/day/9
         *
         */

        private HeightMap heightMap;

        public Aoc09()
        {
            //var numbers = Loader.LoadIntsAsBlock("inputs\\sample09-1.txt");
            var numbers = Loader.LoadIntsAsBlock("inputs\\day09.txt");

            heightMap = new HeightMap(numbers);
        }

        public long RunPuzzle1()
        {
            return heightMap
                .FindSmallestHeights()
                .Sum(num => num + 1);
        }

        public long RunPuzzle2()
        {
            var basins = heightMap.FindBasinForLowPoint();
            var ordered = basins.OrderByDescending(b => b.Value.Count);
            var list = ordered.Take(3).ToList();

            return list[0].Value.Count * list[1].Value.Count * list[2].Value.Count;
        }

        private class HeightMap
        {
            private readonly IDictionary<int, int> data = new Dictionary<int, int>();

            public int Width { get; }
            public int Height { get; }

            public HeightMap(int[,] numbers)
            {
                Height = numbers.GetLength(0);
                Width = numbers.GetLength(1);
                for (int row = 0; row < Height; row += 1)
                {
                    for (int col = 0; col < Width; col += 1)
                    {
                        data[row * Width + col] = numbers[row, col];
                    }
                }
            }

            public List<int> FindSmallestHeights()
            {
                var smallest = new List<int>();

                foreach (var kvp in data)
                {
                    var row = kvp.Key / Width;
                    var col = kvp.Key % Width;

                    if (!HasSmallerNeighbour(row, col))
                    {
                        smallest.Add(kvp.Value);
                    }
                }

                return smallest;
            }

            public List<int> FindLowPoints()
            {
                var lowPoints = new List<int>();

                foreach (var key in data.Keys)
                {
                    var row = key / Width;
                    var col = key % Width;

                    if (!HasSmallerNeighbour(row, col))
                    {
                        lowPoints.Add(key);
                    }
                }

                return lowPoints;
            }

            public Dictionary<int, List<int>> FindBasinForLowPoint()
            {
                var basins = new Dictionary<int, List<int>>();
                foreach (var point in FindLowPoints())
                {
                    var row = point / Width;
                    var col = point % Width;

                    var basinPoints = new List<int> { point };
                    FindBasinPoints(row, col, basinPoints);
                    basins[point] = basinPoints;
                }

                return basins;
            }

            private List<int> FindBasinPoints(int row, int column, List<int> basinPoints)
            {
                var current = data[row * Width + column];

                if (current == 9)
                    return basinPoints;

                CheckUp(row, column, basinPoints);
                CheckDown(row, column, basinPoints);
                CheckLeft(row, column, basinPoints);
                CheckRight(row, column, basinPoints);

                return basinPoints;
            }

            private void CheckUp(int row, int column, List<int> basinPoints)
            {
                var current = data[row * Width + column];
                if (row > 0)
                {
                    var newRow = row - 1;
                    var newIdx = newRow * Width + column;
                    var pointUp = data[newIdx];
                    if (pointUp < 9 && pointUp > current && !basinPoints.Contains(newIdx))
                    {                        
                        basinPoints.Add(newIdx);
                        FindBasinPoints(newRow, column, basinPoints);
                    }
                }
            }

            private void CheckDown(int row, int column, List<int> basinPoints)
            {
                var current = data[row * Width + column];
                if (row < Height - 1)
                {
                    var newRow = row + 1;
                    var newIdx = newRow * Width + column;
                    var pointDown = data[newIdx];
                    if (pointDown < 9 && pointDown > current && !basinPoints.Contains(newIdx))
                    {
                        basinPoints.Add(newIdx);
                        FindBasinPoints(newRow, column, basinPoints);
                    }
                }
            }

            private void CheckLeft(int row, int column, List<int> basinPoints)
            {
                var current = data[row * Width + column];
                if (column > 0)
                {
                    var newColumn = column - 1;
                    var newIdx = row * Width + newColumn;
                    var pointLeft = data[newIdx];
                    if (pointLeft < 9 && pointLeft > current && !basinPoints.Contains(newIdx))
                    {
                        basinPoints.Add(newIdx);
                        FindBasinPoints(row, newColumn, basinPoints);
                    }
                }
            }

            private void CheckRight(int row, int column, List<int> basinPoints)
            {
                var current = data[row * Width + column];
                if (column < Width - 1)
                {
                    var newColumn = column + 1;
                    var newIdx = row * Width + newColumn;
                    var pointRight = data[newIdx];
                    if (pointRight < 9 && pointRight > current && !basinPoints.Contains(newIdx))
                    {
                        basinPoints.Add(newIdx);
                        FindBasinPoints(row, newColumn, basinPoints);
                    }
                }
            }

            public bool HasSmallerNeighbour(int row, int column)
            {
                var current = data[row * Width + column];

                if (current == 9)
                {
                    return true;
                }

                // check up
                if (row > 0)
                {
                    if (data[(row - 1) * Width + column] < current)
                        return true;
                }

                // check down
                if (row < Height - 1)
                {
                    if (data[(row + 1) * Width + column] < current)
                        return true;
                }

                // check left
                if (column > 0)
                {
                    if (data[row * Width + column - 1] < current)
                        return true;
                }

                // check right
                if (column < Width - 1)
                {
                    if (data[row * Width + column + 1] < current)
                        return true;
                }

                return false;
            }
        }
    }
}