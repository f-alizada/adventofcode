using AdventOfCode.Common;

public class Day2
{
    public static void Main(string[] args)
    {
        var levels = Parsers.GetTwoDimensionalSplittedIntegerArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(levels));

        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(levels));
    }

    private static int Problem2(int[][] levels)
    {
        var result = 0;
        foreach (var level in levels)
        {
            if (IsSafe(level))
            {
                result++;
                continue;
            }

            for (int i = 0; i < level.Length; i++)
            {
                if (IsSafe(level, i))
                {
                    result++;
                    break;
                }
            }
        }
        return result;
    }

    private static int Problem1(int[][] levels)
    {
        var result = 0;
        foreach (var level in levels)
        {
            if (IsSafe(level))
            {
                result++;
            }
        }
        return result;
    }

    private static bool IsSafe(int[] level, int? skipIndex = null)
    {
        var startIndex = skipIndex.HasValue && skipIndex == 0 ? 1 : 0;
        var prev = level[startIndex];
        bool? isIncreasing = null;
        for (var i = startIndex + 1; i < level.Length; i++)
        {
            if (skipIndex == i)
            {
                continue;
            }
            var difference = Math.Abs(level[i] - prev);
            if (difference < 1 || difference > 3)
            {
                return false;
            }

            var localIncreasing = prev > level[i];
            if (!isIncreasing.HasValue)
            {
                isIncreasing = localIncreasing;
            }
            else
            {
                if (localIncreasing != isIncreasing.Value)
                {
                    return false;
                }
            }

            prev = level[i];
        }

        return true;
    }
}