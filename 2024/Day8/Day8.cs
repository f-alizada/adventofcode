using AdventOfCode.Common;

public class Day8
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
        // collect pairs
        var pairs = new Dictionary<char, List<(int, int)>>();
        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                if (map[i][k] == '.')
                {
                    continue;
                }

                if (!pairs.ContainsKey(map[i][k]))
                {
                    pairs[map[i][k]] = new List<(int, int)>();
                }

                pairs[map[i][k]].Add((i, k));
            }
        }
        // check antinodes 

        var uniqueVisits = new HashSet<(int, int)>();
        foreach (var pair in pairs.Values)
        {
            for (var i = 0; i < pair.Count; i++)
            {
                for (var j = i + 1; j < pair.Count; j++)
                {
                    var differenceX = pair[i].Item1 - pair[j].Item1;
                    var differenceY = pair[i].Item2 - pair[j].Item2;

                    var antinode1PositionX = pair[i].Item1 - 2 * differenceX;
                    var antinode1Positiony = pair[i].Item2 - 2 * differenceY;

                    if (antinode1PositionX >= 0 &&
                        antinode1PositionX <= map.Length - 1 &&
                        antinode1Positiony >= 0 &&
                        antinode1Positiony <= map[antinode1PositionX].Length - 1)
                    {
                        uniqueVisits.Add((antinode1PositionX, antinode1Positiony));
                    }

                    var antinode2PositionX = pair[j].Item1 + 2 * differenceX;
                    var antinode2Positiony = pair[j].Item2 + 2 * differenceY;

                    if (antinode2PositionX >= 0 &&
                        antinode2PositionX <= map.Length - 1 &&
                        antinode2Positiony >= 0 &&
                        antinode2Positiony <= map[antinode2PositionX].Length - 1)
                    {
                        uniqueVisits.Add((antinode2PositionX, antinode2Positiony));
                    }
                }
            }
        }

        PrintWithAntinodes(map, uniqueVisits);

        return uniqueVisits.Count;
    }

    public static long Problem2(char[][] map)
    {
        // collect pairs
        var pairs = new Dictionary<char, List<(int, int)>>();
        for (var i = 0; i < map.Length; i++)
        {
            for (var k = 0; k < map[i].Length; k++)
            {
                if (map[i][k] == '.')
                {
                    continue;
                }

                if (!pairs.ContainsKey(map[i][k]))
                {
                    pairs[map[i][k]] = new List<(int, int)>();
                }

                pairs[map[i][k]].Add((i, k));
            }
        }
        // check antinodes 

        var uniqueVisits = new HashSet<(int, int)>();
        foreach (var pair in pairs.Values)
        {
            if (pair.Count < 2)
            {
                continue;
            }
            for (var i = 0; i < pair.Count; i++)
            {
                for (var j = i + 1; j < pair.Count; j++)
                {
                    var differenceX = pair[i].Item1 - pair[j].Item1;
                    var differenceY = pair[i].Item2 - pair[j].Item2;

                    var antinode1PositionX = pair[i].Item1 - 2 * differenceX;
                    var antinode1Positiony = pair[i].Item2 - 2 * differenceY;

                    uniqueVisits.Add(pair[i]);
                    uniqueVisits.Add(pair[j]);

                    while (antinode1PositionX >= 0 &&
                        antinode1PositionX <= map.Length - 1 &&
                        antinode1Positiony >= 0 &&
                        antinode1Positiony <= map[antinode1PositionX].Length - 1)
                    {
                        uniqueVisits.Add((antinode1PositionX, antinode1Positiony));
                        antinode1PositionX -= differenceX;
                        antinode1Positiony -= differenceY;
                    }

                    var antinode2PositionX = pair[j].Item1 + 2 * differenceX;
                    var antinode2Positiony = pair[j].Item2 + 2 * differenceY;

                    while (antinode2PositionX >= 0 &&
                        antinode2PositionX <= map.Length - 1 &&
                        antinode2Positiony >= 0 &&
                        antinode2Positiony <= map[antinode2PositionX].Length - 1)
                    {
                        uniqueVisits.Add((antinode2PositionX, antinode2Positiony));
                        antinode2PositionX += differenceX;
                        antinode2Positiony += differenceY;
                    }
                }
            }
        }

        PrintWithAntinodes(map, uniqueVisits);

        return uniqueVisits.Count;
    }

    private static void PrintWithAntinodes(char[][] m, HashSet<(int, int)> uniqueVisits)
    {
        for (int i = 0; i < m.Length; i++)
        {
            for (int j = 0; j < m[i].Length; j++)
            {
                if (m[i][j] == '.' && uniqueVisits.Contains((i, j)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(m[i][j]);
                }
            }
            Console.WriteLine();
        }
    }
}