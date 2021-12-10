namespace Helpers
{
    public class Loader
    {
        public static int[] LoadInts(string filename)
        {
            var lines = File.ReadAllLines(filename);

            return lines.Select(line => Convert.ToInt32(line)).ToArray();
        }

        public static long[] LoadInt64s(string filename)
        {
            var lines = File.ReadAllLines(filename);

            return lines.Select(line => Convert.ToInt64(line)).ToArray();
        }

        public static int[] LoadIntsSingleLine(string filename, char separator)
        {
            var text = File.ReadAllText(filename);

            return text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => Convert.ToInt32(line))
                .ToArray();
        }

        public static long[] LoadInt64sSingleLine(string filename, char separator)
        {
            var text = File.ReadAllText(filename);

            return text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => Convert.ToInt64(line))
                .ToArray();
        }

        public static int[,] LoadIntsAsBlock(string filename)
        {
            var lines = File.ReadAllLines(filename);
            int[,] numbers = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    numbers[i, j] = Convert.ToInt32(lines[i][j].ToString());
                }
            }

            return numbers;
        }
    }
}