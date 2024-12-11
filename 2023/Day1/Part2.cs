using Day1;
using System.Text;

public class Part2
{
    private static readonly Dictionary<string, char> s_numberData = new Dictionary<string, char>()
    {
        { "one", '1' },
        { "two", '2' },
        { "three", '3' },
        { "four", '4' },
        { "five", '5' },
        { "six", '6' },
        { "seven", '7' },
        { "eight", '8' },
        { "nine", '9' },
    };

    public static int GetCalibrationValue(string line)
    {
        var consoleHelper = new ConsoleHelper(line, 100);
        int firstDigitIndex = -1, secondDigitIndex = -1;
        int firstDigitLength = -1, secondDigitLength = -1;

        consoleHelper.DrawInputLine(firstDigitIndex, firstDigitLength, secondDigitIndex, secondDigitLength);

        for (var i = 0; i < line.Length; i++)
        {
            if (line[i] >= 48 && line[i] <= 57)
            {
                if (firstDigitIndex < 0)
                {
                    firstDigitIndex = i;
                    firstDigitLength = 1;
                }
                secondDigitIndex = i;
                secondDigitLength = 1;
            }
            else
            {
                foreach (var wordData in s_numberData.Keys)
                {
                    if (line.Substring(i).StartsWith(wordData))
                    {
                        if (firstDigitIndex < 0)
                        {
                            firstDigitIndex = i;
                            firstDigitLength = wordData.Length;
                        }
                        secondDigitIndex = i;
                        secondDigitLength = wordData.Length;
                        break;
                    }
                }
            }
            consoleHelper.DrawCurrentCursor(i);
            consoleHelper.DrawInputLine(firstDigitIndex, firstDigitLength, secondDigitIndex, secondDigitLength);
            consoleHelper.DrawCurrentAnswer(firstDigitIndex, firstDigitLength, secondDigitIndex, secondDigitLength);
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