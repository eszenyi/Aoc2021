using Helpers;

namespace AocRunner
{
    internal class Aoc15
    {
        /*
         * --- Day 15: Chiton ---
         * https://adventofcode.com/2021/day/15
         *
         */

        // index 0 = row
        // index 1 = column
        readonly int[,] input;

        public Aoc15()
        {
            input = Loader.LoadIntsAsBlock("inputs\\sample15-1.txt");
            //input = Loader.LoadIntsAsBlock("inputs\\day15.txt");
        }

        public long RunPuzzle1()
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);

            // pre-populate array
            var risks = new int[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    risks[row, col] = int.MaxValue;
                }
            }
            risks[0, 0] = 0;

            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        if (row == 0 && col == 0)
                            continue;

                        int lowest = int.MaxValue;

                        // at every point, check neighbouring points and add current risk
                        //check up
                        if (row > 0)
                        {
                            if (risks[row - 1, col] < lowest)
                            {
                                lowest = risks[row - 1, col];
                            }
                        }
                        //check down
                        if (row < height - 1)
                        {
                            if (risks[row + 1, col] < lowest)
                            {
                                lowest = risks[row + 1, col];
                            }
                        }
                        //check left
                        if (col > 0)
                        {
                            if (risks[row, col - 1] < lowest)
                            {
                                lowest = risks[row, col - 1];
                            }
                        }
                        //check right
                        if (col < width - 1)
                        {
                            if (risks[row, col + 1] < lowest)
                            {
                                lowest = risks[row, col + 1];
                            }
                        }
                        lowest += input[row, col];

                        if (lowest < risks[row, col])
                        {
                            risks[row, col] = lowest;
                            hasChanged = true;
                        }
                    }
                }
            }

            return risks[height - 1, width - 1];
        }

        public long RunPuzzle2()
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);

            // pre-populate arrays
            var newInput = new int[width * 5, height * 5];
            for (int row = 0; row < newInput.GetLength(0); row++)
            {
                for (int col = 0; col < newInput.GetLength(1); col++)
                {
                    var risk = input[row % height, col % width] + row / height + col / width;
                    if (risk > 9)
                    {
                        risk -= 9;
                    }
                    newInput[row, col] = risk;
                }
            }

            var risks = new int[width * 5, height * 5];
            for (int row = 0; row < risks.GetLength(0); row++)
            {
                for (int col = 0; col < risks.GetLength(1); col++)
                {
                    risks[row, col] = int.MaxValue;
                }
            }
            risks[0, 0] = 0;

            height = risks.GetLength(0);
            width = risks.GetLength(1);

            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        if (row == 0 && col == 0)
                            continue;

                        int lowest = int.MaxValue;

                        // at every point, check neighbouring points and add current risk
                        //check up
                        if (row > 0)
                        {
                            if (risks[row - 1, col] < lowest)
                            {
                                lowest = risks[row - 1, col];
                            }
                        }
                        //check down
                        if (row < height - 1)
                        {
                            if (risks[row + 1, col] < lowest)
                            {
                                lowest = risks[row + 1, col];
                            }
                        }
                        //check left
                        if (col > 0)
                        {
                            if (risks[row, col - 1] < lowest)
                            {
                                lowest = risks[row, col - 1];
                            }
                        }
                        //check right
                        if (col < width - 1)
                        {
                            if (risks[row, col + 1] < lowest)
                            {
                                lowest = risks[row, col + 1];
                            }
                        }
                        lowest += newInput[row, col];

                        if (lowest < risks[row, col])
                        {
                            risks[row, col] = lowest;
                            hasChanged = true;
                        }
                    }
                }
            }

            return risks[height - 1, width - 1];
        }
    }
}