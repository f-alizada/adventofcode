
using AdventOfCode.Common;

public class Day7
{
    public static void Main(string[] args)
    {
        var input = string.Empty;

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(Parsers.GetTwoDimensionalCharArray()));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(Parsers.GetTwoDimensionalCharArray()));
    }

    public static long Problem1(char[][] map)
    {
        var totalSum = 0;

        for (var i = 0; i < map.Length - 1; i++) 
        {
            for(var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 'S' || map[i][j] == '|')
                {
                    if(map[i+1][j] == '.')
                    {
                        map[i + 1][j] = '|';
                    }
                    else if(map[i + 1][j] == '^')
                    {
                        totalSum++;
                        if (j - 1 >= 0)
                        {
                            map[i + 1][j - 1] = '|';
                        }
                        if (j + 1 < map[i+1].Length)
                        {
                            map[i + 1][j + 1] = '|';
                        }
                    }
                }
            }
        }

        return totalSum;
    }

    public static long Problem2(char[][] map)
    {
        var totalSum = 0L;

        var timeLineMap = new MapEntry[map.Length][];
        timeLineMap = map.Select(x => x.Select(y => new MapEntry() { timeline = y == 'S' ? 1 : 0, value = y }).ToArray()).ToArray(); 

        for (var i = 0; i < timeLineMap.Length - 1; i++)
        {
            for (var j = 0; j < timeLineMap[i].Length; j++)
            {
                if (timeLineMap[i][j].value == 'S' || timeLineMap[i][j].value == '|')
                {
                    if (timeLineMap[i + 1][j].value == '.' || timeLineMap[i + 1][j].value == '|')
                    {
                        timeLineMap[i + 1][j].value = '|';
                        timeLineMap[i + 1][j].timeline = timeLineMap[i + 1][j].timeline + timeLineMap[i][j].timeline;
                    }
                    else if (timeLineMap[i + 1][j].value == '^')
                    {
                        if (j - 1 >= 0)
                        {
                            timeLineMap[i + 1][j - 1].value = '|';
                            timeLineMap[i + 1][j - 1].timeline = timeLineMap[i + 1][j - 1].timeline + timeLineMap[i][j].timeline;
                        }
                        if (j + 1 < timeLineMap[i + 1].Length)
                        {
                            timeLineMap[i + 1][j + 1].value = '|';
                            timeLineMap[i + 1][j + 1].timeline = timeLineMap[i + 1][j + 1].timeline + timeLineMap[i][j].timeline;
                        }
                    }
                }
            }
        }

        for (var i = 0; i < timeLineMap.Length - 1; i++)
        {
            for (var j = 0; j < timeLineMap[i].Length; j++)
            {
                if (timeLineMap[i][j].value == '|')
                {
                    Console.Write($"{timeLineMap[i][j].timeline} ");
                }
                else
                {
                    Console.Write($"{timeLineMap[i][j].value} ");
                }
                
            }
            Console.WriteLine();
        }


        for (var j = 0; j < timeLineMap[timeLineMap.Length - 1].Length; j++)
        {
            if (timeLineMap[timeLineMap.Length - 1][j].value == '|')
            {
                totalSum += timeLineMap[timeLineMap.Length - 1][j].timeline;
            }
        }

       return totalSum;
    }
}

public class MapEntry
{
    public char value { get; set;  }

    public long timeline { get; set; } = 0;
}