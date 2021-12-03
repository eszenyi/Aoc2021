namespace Helpers
{
    public class Binary
    {
        public static long BinaryToLong(string binaryString)
        {
            return Convert.ToInt64(binaryString, 2);
        }
    }
}