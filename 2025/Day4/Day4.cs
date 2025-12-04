
using AdventOfCode.Common;

public class Day4
{
    public static void Main(string[] args)
    {
        var input = string.Empty;

        char[][] rolls = Parsers.GetTwoDimensionalCharArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(rolls));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(rolls));
    }

    public static long Problem1(char[][] rolls)
    {
        return GetReachableRolls(rolls).Count;
    }

    public static long Problem2(char[][] rolls)
    {
        var result = 0;
        var rollsToRemove = GetReachableRolls(rolls);
        while (rollsToRemove.Count > 0) 
        {
            result += rollsToRemove.Count;
            foreach (var rollsIndex in rollsToRemove) 
            {
                rolls[rollsIndex.x][rollsIndex.y] = '.';
            }
            rollsToRemove = GetReachableRolls(rolls);
        }
        return result;
    }

    public static List<(int x, int y)> GetReachableRolls(char[][] rolls)
    {
        var listOfReachableRolls = new List<(int x, int y)>();

        for (var i = 0; i < rolls.Length; i++)
        {
            for (var j = 0; j < rolls[i].Length; j++)
            {
                if (rolls[i][j] != '@')
                {
                    continue;
                }

                var neighbourRollCount = 0;
                foreach (Directions direction in Enum.GetValues(typeof(Directions)))
                {
                    var neighbourIndexX = i + direction.GetDirectionCoordinates().x;
                    var neighbourIndexY = j + direction.GetDirectionCoordinates().y;

                    if (neighbourIndexX >= 0 && neighbourIndexX < rolls.Length && neighbourIndexY >= 0 && neighbourIndexY < rolls[i].Length)
                    {
                        if (rolls[neighbourIndexX][neighbourIndexY] == '@')
                        {
                            neighbourRollCount++;
                        }
                    }
                }
                if (neighbourRollCount < 4)
                {
                    listOfReachableRolls.Add((i, j));
                }
            }
        }

        return listOfReachableRolls;
    }

   
}