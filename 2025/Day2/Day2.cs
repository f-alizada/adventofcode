
public class Day2
{
    public static void Main(string[] args)
    {
        var fileName = "input.test.txt";
        string? line = string.Empty;
        var input = string.Empty;

        using (var fs = File.OpenText(fileName))
        {
            while ((line = fs.ReadLine()) != null)
            {
                input = input + line;
            }
        }

        var ranges = new List<long[]>();
        foreach(var range in input.Split(","))
        {
            ranges.Add(range.Split('-').Select(x => long.Parse(x)).ToArray());
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1SlowVersion(ranges));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2SlowVersion(ranges));
    }

    public static long Problem1SlowVersion(List<long[]> ranges)
    {
        long result = 0;
        foreach (var range in ranges)
        {
            var leftInt = range[0];
            var rightInt = range[1];

            Console.WriteLine($"range {leftInt} - {rightInt}");

            if (leftInt.ToString().Length % 2 == 1 && leftInt.ToString().Length == rightInt.ToString().Length)
            {
                continue;
            }

            while(leftInt <= rightInt)
            {
                var leftStr = leftInt.ToString();
                if (leftStr.Length % 2 == 1)
                {
                    leftInt++;
                    continue;
                }

                if (leftStr.Substring(0, leftStr.Length / 2).Equals(leftStr.Substring(leftStr.Length / 2))){
                    result += leftInt;
                }
                leftInt++;
            }
        }
        return result;
    }


    public static long Problem2SlowVersion(List<long[]> ranges)
    {
        long result = 0;
        foreach (var range in ranges)
        {
            var leftInt = range[0];
            var rightInt = range[1];

            Console.WriteLine($"range {leftInt} - {rightInt}");


            while (leftInt <= rightInt)
            {
                var leftStr = leftInt.ToString();
                if (ContainsRepeatedVals(leftStr))
                {
                    result += leftInt;
                }
                


                leftInt++;
            }
        }
        return result;
    }

    private static bool ContainsRepeatedVals(string val)
    {
        for (var i = 0; i < val.Length / 2; i++) 
        { 
            if (val.Length % (i + 1) != 0)
            {
                continue;
            }

            var lengthToCompare = i + 1;
            var compareWith = val.Substring(0, lengthToCompare);
            var reachedEnd = true;
            for (var j = 0; j < val.Length; j+= lengthToCompare)
            {
                if (! (val.Substring(j, lengthToCompare) == compareWith))
                {
                    reachedEnd = false;
                    break;
                }
            }
            if (reachedEnd)
            {
                return true;
            }
        }
        return false;
    }


    /*
     *  Just thought that solving it using arrays and optimized algorithm to find invalid IDS would help the performance. 
     *  Then I saw the input, so leaving it to solve sometime in the future.
     */
    public static int Problem1(List<int[]> ranges)
    {
        var result = 0;

        foreach (var range in ranges) 
        {
            var leftBorder = TransformToArray(range[0]);
            var rightBorder = TransformToArray(range[1]);

            if (leftBorder.Length % 2 == 1 && leftBorder.Length == rightBorder.Length)
            {
                continue;
            }

            var newleftBorder = new int[leftBorder.Length];
            var halfIndex = (leftBorder.Length / 2);
            for (var i = 0; i < halfIndex; i++) 
            {
                newleftBorder[halfIndex + i] = leftBorder[i];
                newleftBorder[i] = leftBorder[i];
            }

            while(Equals(newleftBorder, leftBorder) >= 0 && Equals(newleftBorder.Take(halfIndex).ToArray(), newleftBorder.TakeLast(halfIndex).ToArray()) != 0)
            {
                AddOne(ref newleftBorder);
            }

            while (Equals(newleftBorder , leftBorder) >= 0 && Equals(rightBorder, newleftBorder) >= 0)
            {
                
            }
        }
        return 0;
    }

    private static int GetInteger(int[] a)
    {
        var res = 0;
        for (var i = a.Length - 1; i >= 0; i--)
        {
            res = res * 10;
            res = res + a[i];
        }

        return res;
    }

    private static int Equals(int[] a, int[] b)
    {
        if ( a.Length > b.Length )
        {
            return 1;
        }

        if (a.Length < b.Length)
        {
            return -1;
        }

        for (var i = 0; i < a.Length; i++) 
        {
            if (a[i] < b[i])
            {
                return -1;
            }
            else if (a[i] > b[i]) 
            {
                return 1;
            }
        }

        return 0;
    }

    private static void AddOne(ref int [] a)
    {
        for(var i = a.Length - 1 ;i > 0; i--)
        {
            a[i]++;
            if ( a[i] == 10)
            {
                a[i] = 0;
            }
            else
            {
                break;
            }
        }

        if (a[0] == 10)
        {
            var l = new List<int>(a);
            l[0] = 0;
            l.Prepend(1);
            a = l.ToArray();
        }
    }


    private static int[] TransformToArray(int value)
    {
        var l = new List<int>();

        while (value > 0)
        {
            l.Add(value % 10);
            value /= 10;
        }

        l.Reverse();

        return l.ToArray();
    }

}