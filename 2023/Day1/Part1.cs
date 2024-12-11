using System.Text;
using Day1;

public class Part1
{
    public static int GetCalibrationValue(string line)
    {
        var consoleHelper = new ConsoleHelper(line, 100);
        int firstDigitIndex = -1, secondDigitIndex = -1;

        consoleHelper.DrawInputLine(firstDigitIndex,-1 , secondDigitIndex, -1);

        for (var i = 0; i < line.Length; i++)
        {
            if (line[i] >= 48 && line[i] <= 57)
            {
                if (firstDigitIndex < 0)
                {
                    firstDigitIndex = i;
                }
                secondDigitIndex = i;
            }
            consoleHelper.DrawCurrentCursor(i);
            consoleHelper.DrawInputLine(firstDigitIndex,1, secondDigitIndex,1);
            consoleHelper.DrawCurrentAnswer(firstDigitIndex,1, secondDigitIndex, 1);
        }

        var calibrationValue = new StringBuilder();
        calibrationValue.Append(line[firstDigitIndex]);
        calibrationValue.Append(line[secondDigitIndex]);
        if (int.TryParse(calibrationValue.ToString(), out var result))
        {
            return result;
        }
        return -1;
    }
}

