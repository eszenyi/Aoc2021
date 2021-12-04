using AocRunner.Bingo;

namespace AocRunner
{
    internal class Aoc04
    {
        const int boardSize = 5;
        const int separator = 1;
        readonly int[] numbers;
        readonly List<Board> boards;

        /*
         * --- Day 4: Giant Squid ---
         * https://adventofcode.com/2021/day/4
         *
         */

        public Aoc04()
        {
            var lines = File.ReadAllLines("inputs\\day04.txt");
            //var lines = File.ReadAllLines("inputs\\sample04-1.txt");
            numbers = lines[0].Split(',').Select(n => int.Parse(n)).ToArray();

            boards = new List<Board>();

            var boardLines = new string[boardSize];
            for (int i = 2; i < lines.Length; i += boardSize + separator)
            {
                Array.Copy(lines, i, boardLines, 0, boardSize);
                boards.Add(new Board(boardLines));
            }
        }

        public long RunPuzzle1()
        {
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.DrawNumber(number))
                    {
                        if (board.IsWinner)
                        {
                            return number * board.Sum;
                        }
                    }
                }
            }

            return -1;
        }

        public long RunPuzzle2()
        {
            Stack<int> winningBoards = new();
            List<Board> toRemove = new();

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.DrawNumber(number))
                    {
                        if (board.IsWinner)
                        {
                            winningBoards.Push(number * board.Sum);
                            toRemove.Add(board);
                        }
                    }
                }

                if (toRemove != null)
                {
                    boards.RemoveAll(b => toRemove.Contains(b));
                    toRemove.Clear();
                }
            }

            return winningBoards.Pop();
        }
    }
}