public class Day7
{
  public static void Main(string[] args)
  { 
    var fileName = "input.test.txt";
    string? line = string.Empty;
    var equations = new List<long[]>();
    var results = new List<long>();
    
    using (var fs = File.OpenText(fileName))
    {
          while((line  = fs.ReadLine()) != null){
                    var splitted = line.Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    results.Add(long.Parse(splitted[0]));
                    equations.Add(splitted[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray());
          }
    }

    Console.WriteLine("Problem 1:");
    Console.WriteLine(Problem1(results, equations));
    Console.WriteLine("----");

    Console.WriteLine("Problem 2:");
    Console.WriteLine(Problem2(results, equations));
  }

  public static long Problem1(List<long> result, List<long[]> equations)
  {
        long sum = 0;
        for (var i = 0; i < result.Count; i++)
        { 
            if (IsEquationValid(result[i], null, equations[i]))
            {
                sum += result[i];
            }
        }
        return sum;
  }

    public static long Problem2(List<long> result, List<long[]> equations)
    {
        long sum = 0;
        for (var i = 0; i < result.Count; i++)
        {
            if (IsEquationValidThreeOp(result[i], null, equations[i]))
            {
                sum += result[i];
            }
        }
        return sum;
    }

    private static bool IsEquationValidThreeOp(long result, long? localRes, long[] values, long index = 0)
    {
        if (!localRes.HasValue)
        {
            localRes = values[0];
            return IsEquationValidThreeOp(result, localRes, values, index + 1);
        }

        if (index == values.Length)
        {
            return localRes == result;
        }

        
        var valueToAdd = values[index];
        long multiplier = 1;
        while (valueToAdd > 0)
        {
            multiplier *= 10;
            valueToAdd /= 10;
        }
        var concatenationValue = localRes * multiplier + values[index];

        return IsEquationValidThreeOp(result, localRes * values[index], values, index + 1) ||
               IsEquationValidThreeOp(result, localRes + values[index], values, index + 1) ||
               IsEquationValidThreeOp(result, concatenationValue, values, index + 1);
    }

    private static bool IsEquationValid(long result, long? localRes, long[] values, long index = 0)
    {
        if (!localRes.HasValue)
        {
            localRes = values[0];
            return IsEquationValid(result, localRes, values, index + 1);
        }

        if (index == values.Length)
        {
            return localRes == result;
        }

        return IsEquationValid(result,  localRes * values[index] , values, index + 1) || IsEquationValid(result, localRes + values[index], values, index + 1);
    }
}