using AdventOfCode.Common;

public class Day16
{
    public static void Main(string[] args)
    {
        var inputData = Parsers.GetTwoDimensionalCharArray();

        char[][] mapP1 = (char[][])inputData.Clone();
        char[][] mapP2 = (char[][])inputData.Clone();

        Console.WriteLine("Problem 1:");
        //Console.WriteLine(Problem1(mapP1));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(mapP2));

    }

    public static long? Problem1(char[][] map)
    {
        var startPosition = GetStartPosition(map, 'S');
        var endPosition = GetStartPosition(map, 'E');

        map[startPosition.Item1][startPosition.Item2] = '.';
        map[endPosition.Item1][endPosition.Item2] = '.';

        var visited = new Dictionary<(int, int, Directions), long> ();
        var result = CalculateMinimumCostOfPath(map, Directions.RIGHT, startPosition, endPosition);

        return result;
    }

    public static long Problem2(char[][] map)
    {
        var startPosition = GetStartPosition(map, 'S');
        var endPosition = GetStartPosition(map, 'E');

        map[startPosition.Item1][startPosition.Item2] = '.';
        map[endPosition.Item1][endPosition.Item2] = '.';

        var visited = new Dictionary<(int, int, Directions), long>();
        var result = CalculateMinimumCostOfPath(map, Directions.RIGHT, startPosition, endPosition);

        return result!.Value;
    }

    private static long? CalculateMinimumCostOfPath(char[][] map, Directions startDirection , (int x, int y) startPosition, (int endX, int endY) end)
    {
        long? minValue = null;
        Dictionary<(int, int, Directions), long> visited = new();


        List<(int x, int y, Directions direction, int price, (int x, int y)[] From)> nextMoves = new List<(int x, int y, Directions direction, int price, (int x, int y)[] From)>();

        nextMoves.Add((startPosition.x, startPosition.y, startDirection, 0, new (int x, int y)[] { (startPosition.x, startPosition.y) }));
        var visitedNodesForPlaces = new HashSet<(int, int)>();
        var totalBestPlaces = 0;
        while (nextMoves.Count > 0)
        {
            var currentMoves = nextMoves.ToArray();
            nextMoves.Clear();
            foreach (var currentMove in currentMoves)
            {
                if (currentMove.x == end.endX && currentMove.y == end.endY)
                {
                    if (minValue.HasValue)
                    {
                        if (minValue.Value > currentMove.price)
                        {
                            minValue = currentMove.price;
                            visitedNodesForPlaces.Clear();
                            totalBestPlaces = GetBestPlacesToSit(currentMove.From, end, startPosition, visitedNodesForPlaces);
                        }
                        else if (minValue.Value == currentMove.price)
                        {
                            totalBestPlaces += GetBestPlacesToSit(currentMove.From, end, startPosition, visitedNodesForPlaces);
                        }
                    }
                    else
                    {
                        minValue = currentMove.price;
                        visitedNodesForPlaces.Clear();
                        totalBestPlaces = GetBestPlacesToSit(currentMove.From, end, startPosition, visitedNodesForPlaces);
                    }
                    continue;
                }

                if (visited.ContainsKey((currentMove.x, currentMove.y, currentMove.direction)))
                {
                    if (visited[(currentMove.x, currentMove.y, currentMove.direction)] < currentMove.price)
                    {
                        continue;
                    }
                }

                if (map[currentMove.x][currentMove.y] == '#')
                {
                    continue;
                }
                
                visited[(currentMove.x, currentMove.y, currentMove.direction)] = currentMove.price;
                var coordinateUpdates = currentMove.direction.GetDirectionCoordinates();

                var fromUpdated = new (int, int)[currentMove.From.Length + 1];
                for (var i = 0; i < currentMove.From.Length; i++)
                {
                    fromUpdated[i] = currentMove.From[i];
                }
                fromUpdated[currentMove.From.Length-1] = (currentMove.x, currentMove.y);

                nextMoves.Add((currentMove.x + coordinateUpdates.x, currentMove.y + coordinateUpdates.y, currentMove.direction, currentMove.price + 1, fromUpdated));
                nextMoves.Add((currentMove.x, currentMove.y, currentMove.direction.TurnRight(), currentMove.price + 1000, currentMove.From));
                nextMoves.Add((currentMove.x, currentMove.y, currentMove.direction.TurnLeft(), currentMove.price + 1000, currentMove.From));
            }
        }

        Console.WriteLine(totalBestPlaces);

        return minValue!.Value;
    }

    private static int GetBestPlacesToSit((int, int)[] cameFrom, (int x, int y) endPosition, (int x, int y) startPosition, HashSet<(int, int)> visitedNodes)
    {
        var result = 0;  
        foreach(var node in cameFrom)
        {
            if (!visitedNodes.Contains((node.Item1, node.Item2)))
            {
                result++;
            }
            visitedNodes.Add((node.Item1, node.Item2));
        }

        return result;
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


    private static void PrintMap(char[][] map, (int, int, Directions) positionToMark)
    {
        Console.Clear();

        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                if(i == positionToMark.Item1 && k == positionToMark.Item2)
                {
                    switch (positionToMark.Item3)
                    {
                        case Directions.TOP:
                            Console.Write('^');
                            break;
                        case Directions.DOWN:
                            Console.Write('v');
                            break;
                        case Directions.LEFT:
                            Console.Write('<');
                            break;
                        case Directions.RIGHT:
                            Console.Write('>');
                            break;
                    }
                }
                else
                {
                    Console.Write(map[i][k]);
                }
            }
            Console.WriteLine();
        }
    }

    private static (int, int) GetStartPosition(char[][] map, char value)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == value)
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}