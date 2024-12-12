using AdventOfCode.Common;

public class Day12
{
    private static HashSet<(int, int)> s_visited;
    public static void Main(string[] args)
    {
        s_visited = new HashSet<(int, int)>();
        var map = Parsers.GetTwoDimensionalCharArray();

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(map));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(map));
    }

    public static long Problem1(char[][] map)
    {
        long result = 0;
        for(var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (!s_visited.Contains((i, j)))
                {
                    // group
                    (int perimeter , int area) = GetPerimeterAndArea(map, i, j, map[i][j]);
                    result = result + perimeter * area;
                }
            }
        }
        return result;
    }


    public static long Problem2(char[][] map)
    {
        s_visited.Clear();
        long result = 0;
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (!s_visited.Contains((i, j)))
                {
                    var sidesStore = new HashSet<(int, int, Directions)>();
                    
                    int area = GetSidesAndArea(map, i, j, map[i][j], null, sidesStore);
                    result = result + area * CalculateSidesFromVisitedPoints(sidesStore);
                }
            }
        }
        return result;
    }

    private static int CalculateSidesFromVisitedPoints(HashSet<(int, int, Directions)> store)
    {
        var currentSide = 0;
        
        var visited = new HashSet<int>();
        var storeList = store.ToList();
        for(var i = 0; i < storeList.Count; i++)
        {
            var current = storeList[i];
            if (visited.Contains(i))
            {
                continue;
            }

            visited.Add(i);
            currentSide++;

            var toCheck = new Stack<(int, int, Directions)>();
            toCheck.Push(current);

            while(toCheck.Count > 0)
            {
                var currentPoint = toCheck.Pop();
                for (var j = 0; j < storeList.Count; j++)
                {
                    if (visited.Contains(j))
                    {
                        continue;
                    }

                    var point = storeList[j];
                    if (point.Item3 == currentPoint.Item3)
                    {
                        if (Math.Abs(point.Item1 - currentPoint.Item1) + Math.Abs(point.Item2 - currentPoint.Item2) == 1)
                        {
                            visited.Add(j);
                            toCheck.Push(point);
                        }
                    }
                }
            }
        }

        return currentSide;
    }

    private static (int, int) GetPerimeterAndArea(char[][] map, int startX, int startY, char value)
    {
        if (startX < 0 || startY < 0 || startX >= map.Length || startY >= map[startX].Length)
        {
            return (1, 0);
        }

        if(map[startX][startY] != value)
        {
            return (1, 0);
        }

        if (s_visited.Contains((startX, startY)))
        {
            return (0, 0);
        }

        s_visited.Add((startX, startY));

        var (perimeterD, areaD) = GetPerimeterAndArea(map, startX + 1, startY, value);
        var (perimeterT, areaT) = GetPerimeterAndArea(map, startX - 1, startY, value);
        var (perimeterL, areaL) = GetPerimeterAndArea(map, startX, startY - 1, value);
        var (perimeterR, areaR) = GetPerimeterAndArea(map, startX, startY + 1, value);

        return ((perimeterD + perimeterL + perimeterR + perimeterT), (areaD + areaL + areaR + areaT + 1));
    }

    private static int GetSidesAndArea(char[][] map, int startX, int startY, char value, Directions? direction, HashSet<(int, int, Directions)> store)
    {
        if (startX < 0 || startY < 0 || startX >= map.Length || startY >= map[startX].Length || map[startX][startY] != value)
        {
            store.Add((startX, startY, direction!.Value));
            
            return 0;
        }

        if (s_visited.Contains((startX, startY)))
        {
            return 0;
        }

        s_visited.Add((startX, startY));

        var areaD = GetSidesAndArea(map, startX + 1, startY, value, Directions.DOWN, store);
        var areaT = GetSidesAndArea(map, startX - 1, startY, value, Directions.TOP, store);
        var areaL = GetSidesAndArea(map, startX, startY - 1, value, Directions.LEFT, store);
        var areaR = GetSidesAndArea(map, startX, startY + 1, value, Directions.RIGHT, store);

        return areaD + areaL + areaR + areaT + 1;
    }
}