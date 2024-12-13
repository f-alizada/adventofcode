using System.Text.RegularExpressions;

using AdventOfCode.Common;

public class Day13
{
    public static void Main(string[] args)
    {
        var machines = Parsers.ReadAllLines();

        int xA, xB;
        int yA, yB;
        int resultX, resultY;

        var buttonRegEx = new Regex("X\\+(?<xValue>\\d{1,}), Y\\+(?<yValue>\\d{1,})");
        var resultRegEx = new Regex("X=(?<xResult>\\d{1,}), Y=(?<yResult>\\d{1,})");

        var machinesSetup = new List<(int, int, int, int, int, int)>();

        for (var i = 0; i < machines.Length; i += 4)
        {
            // read A
            var matchesA = buttonRegEx.Match(machines[i]);

            xA = int.Parse(matchesA.Groups["xValue"].ToString());
            yA = int.Parse(matchesA.Groups["yValue"].ToString());

            var matchesB = buttonRegEx.Match(machines[i + 1]);

            xB = int.Parse(matchesB.Groups["xValue"].ToString());
            yB = int.Parse(matchesB.Groups["yValue"].ToString());

            var results = resultRegEx.Match(machines[i + 2]);

            resultX = int.Parse(results.Groups["xResult"].ToString());
            resultY = int.Parse(results.Groups["yResult"].ToString());

            machinesSetup.Add((xA, yA, xB, yB, resultX, resultY));
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(machinesSetup));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(machinesSetup));
    }

    public static long Problem1(List<(int, int, int, int, int, int)> machineSetup)
    {
        long totalTokens = 0;
        foreach (var machine in machineSetup)
        {
            var tokens = CalculateMinimumTokens(machine.Item1, machine.Item2, machine.Item3, machine.Item4, machine.Item5, machine.Item6, 100);
            if (tokens == -1)
            {
                continue;
            }
            totalTokens += tokens;
        }

        return totalTokens;
    }

    public static long Problem2(List<(int, int, int, int, int, int)> machineSetup)
    {
        long totalTokens = 0;
        
        foreach (var machine in machineSetup)
        {
            var tokens = CalculateMinimumTokensFormula(machine.Item1, machine.Item2, machine.Item3, machine.Item4, 10000000000000 + machine.Item5, 10000000000000 + machine.Item6);
            if (tokens == -1)
            {
                continue;
            }
            totalTokens+= tokens;
        }

        return totalTokens;
    }

    public static long CalculateMinimumTokens(long xA, long yA, long xB, long yB, long resultX, long resultY, long limit)
    {
        // validate possibility
        if ((xA * limit) + (xB * limit) < resultX ||
            (yA * limit) + (yB * limit) < resultY)
        {
            return -1;
        }
        

        // b Cost = 1; 
        // a Cost = 3;

        long totalCost = 0;
        long totalAClicked = 0;
        long totalBClicked = 0;
        while (resultX > 0 && resultY > 0)
        {
            if (resultX % xB == 0 &&
                resultY % yB == 0 &&
                (resultY / yB) == (resultX / xB) &&
                   (
                       
                       (resultX / xB) <= limit &&
                       (resultY / yB) <= limit)
               )
            {
                totalBClicked = (resultX / xB);
                totalCost = totalCost + totalBClicked;
                resultX = 0;
                resultY = 0;
            }
            else
            {
                if (totalAClicked == limit)
                {
                    return -1;
                }

                resultX -= xA;
                resultY -= yA;
                totalCost = totalCost + 3;
                totalAClicked++;
            }
        }

        return (resultX == 0 && resultY == 0) ? totalCost : -1 ;
    }

    public static long CalculateMinimumTokensFormula(long xA, long yA, long xB, long yB, long resultX, long resultY)
    {
        var resultA = (resultX * yB - resultY * xB) / (xA * yB - yA * xB);
        var resultB = (resultY * xA - resultX * yA) / (yB*xA - xB*yA);

        if (resultA * xA + resultB * xB == resultX && resultA * yA + resultB * yB == resultY)
        {
            return resultA * 3 + resultB;
        }
        return -1;
    }
}