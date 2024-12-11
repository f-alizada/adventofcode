using AdventOfCode.Common;

public class Day10
{
    public static void Main(string[] args)
    {
        var map = Parsers.GetTwoDimensionalIntegerArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(map));

        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(map));
    }

    public static int Problem1(int[][] map)
    {
        var count = 0;
        HashSet<(int, int)> store = new HashSet<(int, int)>();
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0)
                {
                    store.Clear();
                    CollectTrailheadCount(map, 0, i, j, store);
                    count += store.Count;
                }
            }
        }

        return count;
    }

    public static int Problem2(int[][] map)
    {
        var rating = 0;
        HashSet<(int, int)> store = new HashSet<(int, int)>();

        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0)
                {
                    rating += CollectTrailheadCount(map, 0, i, j, store);
                }
            }
        }

        return rating;
    }

    private static int CollectTrailheadCount(int[][] map, int currentValue, int positionX, int positionY, HashSet<(int, int)> store)
    {
        if (positionX < 0 || positionX >= map.Length || positionY < 0 || positionY >= map[positionX].Length)
        {
            return 0;
        }

        if (map[positionX][positionY] != currentValue)
        {
            return 0;
        }

        if (currentValue == 9)
        {
            store.Add((positionX, positionY));
            return 1;
        }

        return CollectTrailheadCount(map, currentValue + 1, positionX + 1, positionY, store) +
        CollectTrailheadCount(map, currentValue + 1, positionX - 1, positionY, store) +
        CollectTrailheadCount(map, currentValue + 1, positionX, positionY + 1, store) +
        CollectTrailheadCount(map, currentValue + 1, positionX, positionY - 1, store);
    }
}