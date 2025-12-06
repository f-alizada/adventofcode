
using AdventOfCode.Common;

public class Day6
{
    public static void Main(string[] args)
    {
        var input = string.Empty;


        // read the data
        var lines = Parsers.ReadAllLines();
        var operations = lines[lines.Length - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x[0]).ToList();

        var numbers = new List<long[]>();
        for ( var i = 0; i < lines.Length - 1; i++)
        {
            numbers.Add(lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray());
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(numbers, operations));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2());
    }

    public static long Problem1(List<long[]> numbers, List<char> operations)
    {
        var totalSum = 0L;
        for (var i = 0; i < operations.Count; i++)
        {
            var initialVal = 0L;
            
            if (operations[i] == '*')
            {
                initialVal = 1L;
            }

            foreach(var n in numbers)
            {
                if (operations[i] == '*')
                {
                    initialVal *= n[i];
                }
                else
                {
                    initialVal += n[i];
                }
            }
            totalSum+= initialVal;
        }

        return totalSum;
    }

    public static long Problem2()
    {
        var charArr = Parsers.GetTwoDimensionalCharArray();

        var indexStart = 0;
        var indexEnd = 0;

        var totalSum = 0L;
        
        for ( var i = 1; i < charArr[charArr.Length - 1].Length; i++)
        {
            if (charArr[charArr.Length - 1][i] != ' ' || i == charArr[charArr.Length - 1].Length - 1)
            {
                indexEnd = i;
                
                var initialVal = 0L;
                var operation = charArr[charArr.Length - 1][indexStart];
                if (operation == '*')
                {
                    initialVal = 1L;
                }

                for ( var j = indexStart; j <= indexEnd; j++)
                {
                    var value = 0;
                    var found = false;
                    for (var k = 0; k < charArr.Length - 1; k++)
                    {
                        if (charArr[k][j] != ' ')
                        {
                            found = true;
                            value = value * 10 +  (charArr[k][j] - 48) ;
                        }
                    }

                    if (!found)
                    {
                        break ;
                    }

                    if (operation == '*')
                    {
                        initialVal *= value;
                    }
                    else
                    {
                        initialVal += value;
                    }
                }

                totalSum += initialVal;

                indexStart = i;
            }
        }

        return totalSum;
    }
}