namespace AdventOfCode.Common;

public class Parsers
{
    public static int[][] GetTwoDimensionalIntegerArray()
    {
        return File.ReadAllLines(Constants.InputFilePath)
            .Select(x => x.Select(y => y - 48).ToArray())
            .ToArray();
    }

    public static int[][] GetTwoDimensionalSplittedIntegerArray()
    {
        return File.ReadAllLines(Constants.InputFilePath).
            Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)).
            Select(x => x.Select(y => int.Parse(y)).ToArray()).ToArray();
    }
    public static char[][] GetTwoDimensionalCharArray()
    {
        return File.ReadAllLines(Constants.InputFilePath).
            Select(x => x.ToCharArray()).ToArray();
    }


}
