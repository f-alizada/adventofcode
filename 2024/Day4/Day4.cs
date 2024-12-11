using AdventOfCode.Common;

public class Day4
{
    public static void Main(string[] args)
    {
        var xmas = Parsers.GetTwoDimensionalCharArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(xmas));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(xmas));
    }

    private static readonly Dictionary<char, char> Steps = new Dictionary<char, char>(){
    { 'X', 'M' },
    { 'M', 'A' },
    { 'A', 'S' },
  };

    public static int Problem1(char[][] xmas)
    {
        var sum = 0;
        for (var i = 0; i < xmas.Length; i++)
        {
            for (var k = 0; k < xmas[i].Length; k++)
            {
                if (xmas[i][k] == 'X')
                {
                    foreach (var direction in Directions.Keys)
                    {
                        sum += XmasFinder(xmas, i, k, 'X', direction);
                    }
                }
            }
        }

        return sum;
    }


    private static int XmasFinder(char[][] xmas, int row, int column, char findChar, Direction direction)
    {
        if (row < 0 || row > xmas.Length - 1)
        {
            return 0;
        }

        if (column < 0 || column > xmas[row].Length - 1)
        {
            return 0;
        }

        if (xmas[row][column] != findChar)
        {
            return 0;
        }

        if (!Steps.ContainsKey(findChar))
        {
            return 1;
        }

        var nextChar = Steps[findChar];
        (int x, int y) = Directions[direction];
        return XmasFinder(xmas, row + x, column + y, nextChar, direction);
    }

    private readonly static Dictionary<Direction, (int, int)> Directions = new Dictionary<Direction, (int, int)>(){
    { Direction.TOP, (-1, 0)},
    { Direction.DOWN, (1, 0)},
    { Direction.LEFT, (0, -1)},
    { Direction.RIGHT, (0, 1)},
    { Direction.TOPRIGHT, (-1, 1)},
    { Direction.TOPLEFT, (-1, -1)},
    { Direction.DOWNLEFT, (1, -1)},
    { Direction.DOWNRIGHT, (1, 1)}
  };


    public static int Problem2(char[][] xmas)
    {
        var sum = 0;
        for (var i = 0; i < xmas.Length; i++)
        {
            for (var k = 0; k < xmas[i].Length; k++)
            {
                if (xmas[i][k] == 'A')
                {
                    var dr = XmasFinder(xmas, i - 1, k - 1, 'M', Direction.DOWNRIGHT);
                    var dl = XmasFinder(xmas, i - 1, k + 1, 'M', Direction.DOWNLEFT);
                    var tr = XmasFinder(xmas, i + 1, k - 1, 'M', Direction.TOPRIGHT);
                    var tl = XmasFinder(xmas, i + 1, k + 1, 'M', Direction.TOPLEFT);
                    if (dr + dl + tr + tl == 2)
                    {
                        sum++;
                    }
                }
            }
        }

        return sum;
    }
}

enum Direction
{
    TOP,
    DOWN,
    RIGHT,
    LEFT,
    TOPLEFT,
    TOPRIGHT,
    DOWNLEFT,
    DOWNRIGHT
}