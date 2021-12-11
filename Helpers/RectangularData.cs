namespace Helpers
{
    public class RectangularData
    {
        public static (int, int) GetRowAndColumnForIndexAndWidth(int index, int width)
        {
            return (index / width, index % width);
        }

        public static int GetIndexForRowAndColumn(int row, int column, int width)
        {
            return row * width + column;
        }
    }
}