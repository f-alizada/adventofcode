
using System.Reflection.Metadata;

using AdventOfCode.Common;

public class Day3
{
    public static void Main(string[] args)
    {
        var input = string.Empty;

        int[][] blocks = Parsers.GetTwoDimensionalIntegerArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem(blocks, 2));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem(blocks, 12));
    }

    public static long Problem(int[][] blocks, int limit)
    {
        long result = 0;

        foreach (var block in blocks) 
        {
            long localMax = 0;
            (int max, int maxIndex)? battery = (0,-1);

            for (var i = limit - 1; i >= 0; i--)
            {
                battery = FindMax(battery.Value.maxIndex + 1, block, i);
                localMax = localMax * 10 + battery.Value.max;
            }
            

            Console.WriteLine($"{localMax}");

            result = localMax + result;
        }

        return result;
    }


    private static (int max, int maxIndex) FindMax(int startIndex, int[] block, int limitCount)
    {
        var max = block[startIndex];
        var maxIndex  = startIndex;

        for (var i = startIndex; i < block.Length - limitCount; i++) 
        {
            if (max < block[i])
            {
                max = block[i];
                maxIndex = i;
            }
        }

        return (max, maxIndex);
    }
}