using Helpers;

namespace AocRunner
{
    internal class Aoc11
    {
        /*
         * --- Day 11: Dumbo Octopus ---
         * https://adventofcode.com/2021/day/11
         *
         */

        // index 0 = row
        // index 1 = column
        readonly int[,] input;
        readonly int width;
        readonly int height;

        public Aoc11()
        {
            //input = Loader.LoadIntsAsBlock("inputs\\sample11-1.txt");
            //input = Loader.LoadIntsAsBlock("inputs\\sample11-2.txt");
            input = Loader.LoadIntsAsBlock("inputs\\day11.txt");
            height = input.GetLength(0);
            width = input.GetLength(1);
        }

        public long RunPuzzle1()
        {
            const int numberOfSteps = 100;

            int numberOfFlashes = 0;
            for (int i = 0; i < numberOfSteps; i++)
            {
                numberOfFlashes += ExecuteStep();
            }

            return numberOfFlashes;
        }

        public long RunPuzzle2()
        {
            bool inSync = false;
            int stepCount = 0;
            while (!inSync)
            {
                var numberOfFlashes = ExecuteStep();
                stepCount++;
                if (numberOfFlashes == input.Length)
                {
                    inSync = true;
                }
            }

            return stepCount;
        }

        private int ExecuteStep()
        {
            IncreaseEnergyLevels();

            var flashes = new List<int>();
            DoFlash(flashes);
            var flashCount = flashes.Count;

            ResetEnergyLevels(flashes);

            return flashCount;
        }

        private void IncreaseEnergyLevels()
        {
            for (int row = 0; row < input.GetLength(0); row++)
            {
                for (int col = 0; col < input.GetLength(1); col++)
                {
                    input[row, col]++;
                }
            }
        }

        private void DoFlash(List<int> flashes)
        {
            for (int row = 0; row < input.GetLength(0); row++)
            {
                for (int col = 0; col < input.GetLength(1); col++)
                {
                    int currentIndex = RectangularData.GetIndexForRowAndColumn(row, col, width);
                    if (input[row, col] > 9 && !flashes.Contains(currentIndex))
                    {
                        flashes.Add(currentIndex);
                        CheckNeighbours(row, col, flashes);
                    }
                }
            }
        }

        private void CheckNeighbours(int row, int col, List<int> flashes)
        {
            CheckOctopus(row - 1, col - 1, flashes); //top left
            CheckOctopus(row - 1, col, flashes); // top
            CheckOctopus(row - 1, col + 1, flashes); // top right
            CheckOctopus(row, col + 1, flashes); // right
            CheckOctopus(row + 1, col + 1, flashes); // bottom right 
            CheckOctopus(row + 1, col, flashes); // bottom
            CheckOctopus(row + 1, col - 1, flashes); // bottom left
            CheckOctopus(row, col - 1, flashes); // left
        }

        private void CheckOctopus(int row, int col, List<int> flashes)
        {
            if (row < 0 || col < 0 || row >= height || col >= width)
            {
                return;
            }

            int currentIndex = RectangularData.GetIndexForRowAndColumn(row, col, width);
            if (!flashes.Contains(currentIndex))
            {
                input[row, col]++;
                if (input[row, col] > 9)
                {
                    flashes.Add(currentIndex);
                    CheckNeighbours(row, col, flashes);
                }
            }
        }

        private void ResetEnergyLevels(List<int> flashes)
        {
            foreach (int flashIndex in flashes)
            {
                var (row, col) = RectangularData.GetRowAndColumnForIndexAndWidth(flashIndex, width);
                input[row, col] = 0;
            }
        }
    }
}