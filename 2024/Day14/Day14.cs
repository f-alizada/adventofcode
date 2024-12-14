using System.Text.RegularExpressions;

using AdventOfCode.Common;

public class Day14
{
    public static void Main(string[] args)
    {
        var robots = Parsers.ReadAllLines();

        int vX,vY;
        int pX, pY;

        var lineRegEx = new Regex("p=(?<pyValue>\\d{1,}),(?<pxValue>\\d{1,}) v=(?<vyValue>-{0,1}\\d{1,}),(?<vxValue>-{0,1}\\d{1,})");

        var robotsSetup = new List<(int, int, int, int)>();

        for (var i = 0; i < robots.Length; i++)
        {
            var match = lineRegEx.Match(robots[i]);

            pX = int.Parse(match.Groups["pxValue"].ToString());
            pY = int.Parse(match.Groups["pyValue"].ToString());

            vX = int.Parse(match.Groups["vxValue"].ToString());
            vY = int.Parse(match.Groups["vyValue"].ToString());

            robotsSetup.Add((pX, pY, vX, vY));
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(robotsSetup, 103, 101));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(robotsSetup, 103, 101));
    }

    public static long Problem1(List<(int, int, int, int)> robotsSetup, int tileX, int tileY)
    {
        long qquadrant1 = 0;
        long qquadrant2 = 0;
        long qquadrant3 = 0;
        long qquadrant4 = 0;
        foreach (var robot in robotsSetup)
        {
            var newpositionX = robot.Item1 + 100 * robot.Item3;
            var newpositionY = robot.Item2 + 100 * robot.Item4;

            newpositionX = newpositionX % tileX;
            newpositionY = newpositionY % tileY;

            if(newpositionX < 0)
            {
                newpositionX += tileX;
            }

            if (newpositionY < 0)
            {
                newpositionY += tileY;
            }

            if (newpositionX >= 0 && newpositionY >= 0 && newpositionX < tileX/2 && newpositionY < tileY/2) 
            {
                qquadrant1++;
           }
            else if (newpositionX >= tileX/2 + 1 && newpositionY >= 0 && newpositionX < tileX && newpositionY < tileY/2)
            {
                qquadrant2++;
            }
            else if (newpositionX >= tileX/2 + 1 && newpositionY >= tileY/2 + 1 && newpositionX < tileX && newpositionY < tileY)
            {
                qquadrant3++;
            }
            else if (newpositionX >= 0 && newpositionY >= tileY/2+1 && newpositionX < tileX/2 && newpositionY < tileY )
            {
                qquadrant4++;
            }
            else
            {
                Console.WriteLine("possible case");
            }
        }

        return qquadrant1*qquadrant2*qquadrant3*qquadrant4;
    }

    public static long Problem2(List<(int, int, int, int)> robotsSetup, int tileX, int tileY)
    {
        Console.WriteLine("Monitor the output for the tree");
        var count = 0;
        var tiles = new int[tileX, tileY];
        while(1 == 1)
        {
            count++;
            for (var i = 0; i < robotsSetup.Count; i++)
            {
                if (tiles[robotsSetup[i].Item1, robotsSetup[i].Item2] > 0)
                {
                    tiles[robotsSetup[i].Item1, robotsSetup[i].Item2]--;
                }
                var newpositionX = robotsSetup[i].Item1 + robotsSetup[i].Item3;
                var newpositionY = robotsSetup[i].Item2 +  robotsSetup[i].Item4;

                newpositionX = newpositionX % tileX;
                newpositionY = newpositionY % tileY;

                if (newpositionX < 0)
                {
                    newpositionX += tileX;
                }

                if (newpositionY < 0)
                {
                    newpositionY += tileY;
                }
                tiles[newpositionX, newpositionY]++;
                robotsSetup[i] = (newpositionX, newpositionY, robotsSetup[i].Item3, robotsSetup[i].Item4);
            }
            
            PrintPossibleTree(tiles, count);
        }
    }

    private static void PrintPossibleTree(int[,] tiles, int count)
    {
        
        var triangleFound = false;
        for (var i = 0; i < 50; i++)
        {
            var center = tiles.GetLength(1) / 2;
            if (tiles[i, center] > 0 && tiles[i + 1, center + 1] > 0 && tiles[i + 1, center - 1] > 0)
            {
                triangleFound = true;
            }
        }

        if (!triangleFound)
        {
            return;
        }

        Console.WriteLine(count);

        for(var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j] > 0)
                {
                    Console.Write(tiles[i, j]);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();
        }
    }

}