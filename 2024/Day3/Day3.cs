using System.Text;
using System.Text.RegularExpressions;

public class Day3
{
    public static void Main(string[] args)
    {
        var fileName = "input.test.txt";
        string? data = string.Empty;
        using (var fs = File.OpenText(fileName))
        {
            data = fs.ReadToEnd();
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(data));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(data));
    }

    public static int Problem1(string data)
    {
        var sum = 0;
        var regEx = new Regex(@"mul\((?<val1>\d{1,}),(?<val2>\d{1,})\)");
        var mathes = regEx.Matches(data);
        foreach (Match match in mathes)
        {
            sum = sum + (int.Parse(match.Groups["val1"].Value) * int.Parse(match.Groups["val2"].Value));
        }
        return sum;
    }

    public static int Problem2(string data)
    {
        // try custom parser

        var todo = true;
        var currentStage = 0;
        var mulStart = "mul(";
        var doWord = "do()";
        var dontWord = "don't()";
        var sum = 0;

        var leftVal = new StringBuilder();
        var rightVal = new StringBuilder();

        for (var i = 0; i < data.Length; i++)
        {
            if (currentStage == 1)
            {
                if (data[i] >= 48 && data[i] <= 57)
                {
                    leftVal.Append(data[i]);
                }
                else if (data[i] == ',')
                {
                    currentStage = 2;
                }
                else
                {
                    currentStage = 0;
                }
            }
            else if (currentStage == 2)
            {
                if (data[i] >= 48 && data[i] <= 57)
                {
                    rightVal.Append(data[i]);
                }
                else if (data[i] == ')')
                {
                    currentStage = 0;
                    sum += int.Parse(leftVal.ToString()) * int.Parse(rightVal.ToString());
                }
                else
                {
                    currentStage = 0;
                }
            }

            if (currentStage == 0)
            {
                if (IsWordMatch(mulStart, data, i))
                {
                    i = i + mulStart.Length - 1;

                    if (todo)
                    {
                        currentStage = 1;
                    }

                    leftVal.Clear();
                    rightVal.Clear();
                    continue;
                }

                if (IsWordMatch(doWord, data, i))
                {
                    todo = true;
                    i = i + doWord.Length - 1;
                    continue;
                }

                if (IsWordMatch(dontWord, data, i))
                {
                    todo = false;
                    i = i + dontWord.Length - 1;
                    continue;
                }

            }
        }
        return sum;
    }

    private static bool IsWordMatch(string searchWord, string inputWord, int startIndex)
    {
        for (var k = 0; k < searchWord.Length; k++)
        {
            if (startIndex + k >= inputWord.Length)
            {
                return false;
            }
            if (inputWord[startIndex + k] != searchWord[k])
            {
                return false;
            }
        }
        return true;
    }

}