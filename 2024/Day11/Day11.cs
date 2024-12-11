using System.Diagnostics.Metrics;

public class Day11
{
    public static void Main(string[] args)
    { 
        var fileName = "input.test.txt";
        var stones = new List<long>();
    
        using (var fs = File.OpenText(fileName))
        {
            var stonesInput = fs.ReadToEnd().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            stones = stonesInput.Select(long.Parse).ToList();
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(stones.ToArray()));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(stones.ToArray()));
    }

    public static long Problem1(long[] stones)
    {
        long count = 0;

        foreach(var stone in stones)
        {
            count = count + CalculateStoneCount(stone, 25);
        }

        return count;
    }


    public static long Problem2(long[] stones)
    {
        long count = 0;

        foreach (var stone in stones)
        {
            count = count + CalculateStoneCount(stone, 75);
        }

        return count;
    }

    private static long CalculateStoneCount(long initialValue, int blinks = 25)
    {
        var newStones = new Dictionary<long, long>();
        newStones[initialValue] = 1;
        var tmpMem = new Dictionary<long, long>();

        for (var i = 0; i < blinks; i++)
        {
            tmpMem = new Dictionary<long, long>(newStones);
            newStones.Clear();

            foreach (var stone in tmpMem.Keys)
            {
                if (stone == 0)
                {
                    newStones.TryGetValue(1, out var count);
                    newStones[1] = count + tmpMem[stone];
                }
                else if (IsEvenDigits(stone))
                {
                    (long number1, long number2) = SplitNumber(stone);
                    newStones.TryGetValue(number1, out var count);
                    newStones[number1] = count + tmpMem[stone];

                    newStones.TryGetValue(number2, out count);
                    newStones[number2] = count + tmpMem[stone];
                }
                else
                {
                    newStones.TryGetValue(stone * 2024, out var count);
                    newStones[stone * 2024] = tmpMem[stone] + count;
                }
            }
        }

        long totalSum = 0;
        foreach (var val in newStones.Values)
        {
            totalSum += val;
        }
        return totalSum;
    }


    private static (long, long) SplitNumber(long number)
    {
        var storeNumber = new List<long>();
        while (number > 0)
        {
            storeNumber.Add(number%10);
            number /= 10;
        }

        var count = storeNumber.Count;
        long number1 = 0;
        long number2 = 0;

        for(var i = 0; i < count / 2; i++) 
        {
            number1 *= 10;
            number2 *= 10;
            number1 = number1 + storeNumber[count - 1 - i];
            number2 = number2 + storeNumber[count- 1 - i - count / 2];
        }

        return (number1, number2);
    }

    private static bool IsEvenDigits(long number)
    {
        var count = 0;
        
        while(number > 0)
        {
            count++;
            number /= 10;
        }

        return count % 2 == 0;
    }
}