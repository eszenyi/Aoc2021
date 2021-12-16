namespace Helpers
{
    public class Binary
    {
        public static int BinaryToInt(string binaryString)
        {
            return Convert.ToInt32(binaryString, 2);
        }

        public static long BinaryToLong(string binaryString)
        {
            return Convert.ToInt64(binaryString, 2);
        }
    }
}