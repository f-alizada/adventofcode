using AdventOfCode.Common;
public class Day6
{
    public static void Main(string[] args)
    {
        var map = Parsers.GetTwoDimensionalCharArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(map));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(map));
    }

    public static int Problem1(char[][] map)
    {
        (int x, int y) = GetStartPosition(map);
        var direction = Direction.TOP;
        var storeOfVisitedPlaces = new HashSet<string>();

        while (1 == 1)
        {
            (int posUpdatesX, int posUpdatesY) = Directions[direction];
            var tmpX = posUpdatesX + x;
            var tmpY = posUpdatesY + y;
            if (tmpX < 0 || tmpX > map.Length - 1 || tmpY < 0 || tmpY > map[x].Length - 1)
            {
                break;
            }

            if (map[tmpX][tmpY] == '#')
            {
                direction = GetUpdatedDirection(direction);
            }
            else
            {
                storeOfVisitedPlaces.Add($"x={x},y={y}");
                x = tmpX;
                y = tmpY;
            }
        }

        return storeOfVisitedPlaces.Count + 1;
    }

    private static (int, int) GetStartPosition(char[][] map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '^')
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    public static int Problem2(char[][] map)
    {
        (int startX, int startY) = GetStartPosition(map);
        var storeOfVisitedPlaces = new HashSet<string>();
        var sum = 0;

        int x = startX;
        int y = startY;

        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                if (map[i][k] != '#' && (i != startX || k != startY))
                {
                    map[i][k] = '#';

                    if (!CanGoOut(map, startX, startY, Direction.TOP))
                    {
                        sum++;
                    }
                    map[i][k] = '.';
                }
            }
        }

        return sum;
    }

    private static bool CanGoOut(char[][] map, int x, int y, Direction direction)
    {
        var visitedPlaces = new HashSet<string>();

        while (1 == 1)
        {
            (int posUpdatesX, int posUpdatesY) = Directions[direction];
            var tmpX = posUpdatesX + x;
            var tmpY = posUpdatesY + y;

            if (tmpX < 0 || tmpX > map.Length - 1 || tmpY < 0 || tmpY > map[x].Length - 1)
            {
                return true;
            }

            if (visitedPlaces.Contains($"x={x},y={y},{direction}"))
            {
                return false;
            }

            visitedPlaces.Add($"x={x},y={y},{direction}");

            if (map[tmpX][tmpY] == '#')
            {
                direction = GetUpdatedDirection(direction);
            }
            else
            {
                x = tmpX;
                y = tmpY;
            }
        }
    }

    private static void PrintWithObst(char[][] map, int x, int y)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                if (x == i && k == y)
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(map[i][k]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("----");
    }

    private static Direction GetUpdatedDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.TOP:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.LEFT;
            case Direction.LEFT:
                return Direction.TOP;
        }
        return direction;
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
}