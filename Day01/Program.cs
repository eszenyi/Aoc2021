using Helpers;

var loader = new Loader();
var numbers = loader.LoadInts("input1.txt");
//var numbers = loader.LoadInts("sample1.txt");

int prev = int.MinValue;
int result = 0;
for (int i = 0; i < numbers.Length; i++)
{
    if (i + 2 < numbers.Length)
    {
        var sum = numbers[i] + numbers[i + 1] + numbers[i + 2];
        if (i > 0)
        {
            if (sum > prev)
                result++;
        }
        prev = sum;
    }
}

Console.WriteLine($"{result}");
