namespace AocRunner.Bingo
{
    internal class Board
    {
        const int BoardSize = 5;
        readonly IDictionary<int, BoardItem> _board = new Dictionary<int, BoardItem>();
        int _howManyDrawn = 0;

        public int Sum { get; private set; }

        public Board(string[] input)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                var numbers = input[row].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                for (int col = 0; col < BoardSize; col++)
                {
                    Sum += numbers[col];
                    _board.Add(row * BoardSize + col, new BoardItem { Number = numbers[col] });
                }
            }
        }

        public bool DrawNumber(int drawn)
        {
            bool found = false;

            foreach (var pair in _board)
            {
                if (pair.Value.Number == drawn)
                {
                    found = true;
                    _board[pair.Key].IsDrawn = true;
                    _howManyDrawn++;
                    Sum -= drawn;
                    break;
                }
            }

            return found;
        }

        public bool IsWinner
        {
            get
            {
                if (_howManyDrawn < 5)
                    return false;

                // check rows
                for (int row = 0; row < BoardSize; row++)
                {
                    bool completeRow = true;
                    var currentCol = 0;
                    for (int col = 0; col < BoardSize; col++)
                    {
                        currentCol = col;
                        if (!_board[row * BoardSize + col].IsDrawn)
                        {
                            completeRow = false;
                            break;
                        }
                    }
                    if (completeRow)
                    {
                        return true;
                    }
                }

                // check columns
                for (int col = 0; col < BoardSize; col++)
                {
                    bool completeRow = true;
                    var currentRow = 0;
                    for (int row = 0; row < BoardSize; row++)
                    {
                        currentRow = row;
                        if (!_board[row * BoardSize + col].IsDrawn)
                        {
                            completeRow = false;
                            break;
                        }
                    }
                    if (completeRow)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private class BoardItem
        {
            public int Number { get; set; }
            public bool IsDrawn { get; set; }

            public override string ToString()
            {
                return $"{Number}: {IsDrawn}";
            }
        }
    }
}