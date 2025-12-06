
using AdventOfCode.Common;

public class Day5
{
    public static void Main(string[] args)
    {
        var input = string.Empty;

        var freshRange = new List<long[]>();
        var existingIn = new List<long>();
        string? line = string.Empty;

        using (var fs = File.OpenText(Constants.InputFilePath))
        {
            while ((line = fs.ReadLine()) != string.Empty)
            {
                freshRange.Add(line.Split('-').Select(x => long.Parse(x)).ToArray());
            }

            while ((line = fs.ReadLine()) != null)
            {
                existingIn.Add(long.Parse(line));
            }
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(freshRange, existingIn));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(freshRange));
    }

    public static long Problem1(List<long[]> fresh, List<long> ingredients)
    {
        var result = 0;
        foreach (var id in ingredients)
        {
            if (IsFresh(fresh, id))
            {
                result++;
            }
        }
        return result;
    }


    private static bool IsFresh(List<long[]> fresh, long id)
    {
        for (var i = 0; i < fresh.Count; i++)
        {
            if (id >= fresh[i][0] && id <= fresh[i][1])
            {
                return true;
            }
        }
        return false;
    }

    public static long Problem2(List<long[]> fresh)
    {
        var combined = new List<long[]>();

        fresh.Sort(delegate(long[] x, long[] y)
        {
            return x[0].CompareTo(y[0]);
        });

        foreach (var f in fresh)
        {
            var collided = false;
            foreach(var c in combined)
            {
                if (isColliding(c[0], c[1], f[0], f[1]))
                {
                    collided = true;
                    c[0] = Math.Min(c[0], f[0]);
                    c[1] = Math.Max(c[1], f[1]);
                    break; 
                }
            }
            if (!collided)
            {
                combined.Add(f);
            }
        }

        var totalFresh = 0L;

        foreach (var f in combined)
        {
            totalFresh = totalFresh + f[1] - f[0] + 1; 
        }

        return totalFresh;
    }


    private static bool isColliding(long x1, long y1, long x2, long y2) 
    { 
        if ((x1 >= x2  && x1 <= y2) || (y1 >= x2 && y1 <= y2))
        {
            return true;
        }

        if ((x2 >= x1 && x2 <= y1) || (y2 >= x1 && y2 <= y1))
        {
            return true;
        }

        return false;
    }
}