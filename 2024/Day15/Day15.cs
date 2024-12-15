using AdventOfCode.Common;

public class Day15
{
    public static void Main(string[] args)
    {
        var inputData = Parsers.ReadAllLines();

        var mapData = new List<string>();
        var navigationData = new List<char>();

        var readingMap = true;

        foreach (var input in inputData)
        {
            if (string.IsNullOrEmpty(input))
            {
                readingMap = false;
                continue;
            }

            if (readingMap)
            {
                mapData.Add(input);
            }
            else
            {
                navigationData.AddRange(input.ToCharArray());
            }
        }

        var mapP1 = mapData.Select(x => x.ToCharArray()).ToArray();
        var mapP2 = mapData.Select(x => x.ToCharArray()).ToArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(mapP1, navigationData.ToArray()));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(mapP2, navigationData.ToArray()));

    }

    public static long Problem1(char[][] map, char[] navigation)
    {
        (int startX, int startY) = GetStartPosition(map);
        PrintMap(map);
        foreach (char nav in navigation)
        {
            Directions? direction = null;

            switch (nav)
            {
                case '<':
                    direction = Directions.LEFT;
                    break;
                case '^':
                    direction = Directions.TOP;
                    break;
                case '>':
                    direction = Directions.RIGHT;
                    break;
                case 'v':
                    direction = Directions.DOWN;
                    break;
            }

            (int xUpdate, int yUpdate) = direction!.Value.GetDirectionCoordinates();

            var nextX = startX + xUpdate;
            var nextY = startY + yUpdate;

            var canMove = true;

            while (map[nextX][nextY] != '.')
            {
                if (map[nextX][nextY] == '#')
                {
                    canMove = false;
                    break;
                }

                nextX += xUpdate;
                nextY += yUpdate;
            }

            if (canMove)
            {
                map[startX][startY] = '.';
                map[startX + xUpdate][startY + yUpdate] = '@';
                if (nextX != startX + xUpdate || nextY != startY + yUpdate)
                {
                    map[nextX][nextY] = 'O';
                }
                startX += xUpdate;
                startY += yUpdate;
            }
        }

        return CalculateSumGPSCoordinates(map, 'O');
    }

    public static long Problem2(char[][] map, char[] navigation)
    {
        // resize first
        map = ResizeMap(map);
        PrintMap(map);
        (int startX, int startY) = GetStartPosition(map);
        
        foreach (char nav in navigation)
        {
            Directions? direction = null;

            switch (nav)
            {
                case '<':
                    direction = Directions.LEFT;
                    break;
                case '^':
                    direction = Directions.TOP;
                    break;
                case '>':
                    direction = Directions.RIGHT;
                    break;
                case 'v':
                    direction = Directions.DOWN;
                    break;
            }


            (int xUpdate, int yUpdate) = direction!.Value.GetDirectionCoordinates();

            if (direction!.Value == Directions.RIGHT || direction!.Value == Directions.LEFT)
            {
                var nextX = startX + xUpdate;
                var nextY = startY + yUpdate;
                var canMove = true;
                var moveCounts = 1;
                while (map[nextX][nextY] != '.')
                {
                    if (map[nextX][nextY] == '#')
                    {
                        canMove = false;
                        break;
                    }
                    moveCounts++;
                    nextX += xUpdate;
                    nextY += yUpdate;
                }

                if (canMove)
                {
                    for (var i = 1; i <= moveCounts; i++)
                    {
                        map[nextX][nextY] = map[nextX - xUpdate][nextY - yUpdate];
                        nextX -= xUpdate;
                        nextY -= yUpdate;
                    }
                    map[startX][startY] = '.';
                    startX = startX + xUpdate;
                    startY = startY + yUpdate;
                }
            }
            else
            {
                var indexesToMove = new List<List<(int, int)>>();
                var currentIndexes = new HashSet<(int, int)>();
                currentIndexes.Add((startX, startY));
                indexesToMove.Add(currentIndexes.ToList());

                var canMove = true;
                while (1 == 1)
                {
                    if (currentIndexes.Count == 0)
                    {
                        break;
                    }

                    var nextIndexes = new HashSet<(int, int)>();

                    foreach ((int indexX, int indexY) in currentIndexes)
                    {
                        var nextIndexX = indexX + xUpdate;
                        var nextIndexY = indexY + yUpdate;
                        if (map[nextIndexX][nextIndexY] == '#')
                        {
                            canMove = false;
                            break;
                        }
                        else if (map[nextIndexX][nextIndexY] == '[')
                        {
                            nextIndexes.Add((nextIndexX, nextIndexY));
                            nextIndexes.Add((nextIndexX, nextIndexY+1));
                        }
                        else if (map[nextIndexX][nextIndexY] == ']')
                        {
                            nextIndexes.Add((nextIndexX, nextIndexY));
                            nextIndexes.Add((nextIndexX, nextIndexY - 1));
                        }
                    }
                    if (nextIndexes.Count > 0)
                    {
                        indexesToMove.Add(nextIndexes.ToList());
                    }

                    currentIndexes = nextIndexes;
                }

                if (canMove)
                {
                    for (var i = indexesToMove.Count - 1; i >= 0; i--)
                    {
                        foreach (var (indexX, indexY) in indexesToMove[i])
                        {
                            map[indexX + xUpdate][indexY + yUpdate] = map[indexX][indexY];
                            map[indexX][indexY] = '.';
                        }
                    }
                    
                    map[startX][startY] = '.';
                    startX = startX + xUpdate;
                    startY = startY + yUpdate;
                }
            }
        }

        PrintMap(map);

        return CalculateSumGPSCoordinates(map, '[');
    }

    private static char[][] ResizeMap(char[][] map)
    {
        var newMap = new List<char[]>();

        for (int i = 0; i < map.Length; i++)
        {
            var row = new List<char>();
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '#')
                {
                    row.Add('#');
                    row.Add('#');
                }
                else if (map[i][j] == '.')
                {
                    row.Add('.');
                    row.Add('.');
                }
                else if (map[i][j] == 'O')
                {
                    row.Add('[');
                    row.Add(']');
                }
                else if (map[i][j] == '@')
                {
                    row.Add('@');
                    row.Add('.');
                }
            }
            newMap.Add(row.ToArray());
        }
        return newMap.ToArray();
    }

    private static long CalculateSumGPSCoordinates(char[][] map, char boxSymbol)
    {
        long sum = 0;
        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                if (map[i][k] == boxSymbol)
                {
                    sum = sum + (i * 100) + k;
                }
            }
        }
        return sum;

    }

    private static void PrintMap(char[][] map)
    {
        Console.WriteLine();
        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                Console.Write(map[i][k]);
            }
            Console.WriteLine();
        }
    }

    private static (int, int) GetStartPosition(char[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '@')
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}