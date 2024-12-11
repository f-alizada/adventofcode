
namespace Day1
{
    internal class ConsoleHelper
    {
        private int inputCurosrTop;
        private int stepCursorTop;
        private int answerCursorTop;
        private int sleepTime;
        private string inputLine;

        public ConsoleHelper(string input, int sleepTime = 1000)
        {
            Console.Clear();
            Console.CursorVisible = false;

            inputLine = input;
            inputCurosrTop = Console.CursorTop;
            stepCursorTop = Console.CursorTop + 1;
            answerCursorTop = Console.CursorTop + 2;
            this.sleepTime = sleepTime;
        }

        public void Sleep()
        {
            Thread.Sleep(sleepTime);
        }

        public void DrawInputLine(int firstDigitIndex, int firstDigitLength, int secondDigitIndex, int secondDigitLength)
        {
            Console.SetCursorPosition(0, inputCurosrTop);
            Console.CursorLeft = 0;

            for (var i = 0; i < inputLine.Length; i++)
            {
                var currentForegroundColor = Console.ForegroundColor;
                if (i >= firstDigitIndex && i < firstDigitIndex + firstDigitLength ||
                    i >= secondDigitIndex && i < secondDigitIndex + secondDigitLength)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(inputLine[i]);
                Console.ForegroundColor = currentForegroundColor;
            }
            Sleep();
        }

        public void DrawCurrentCursor(int skip)
        {
            Console.SetCursorPosition(0, stepCursorTop);
            Console.CursorLeft = 0;
            for (var i = 0; i < skip; i++)
            {
                Console.Write(' ');
            }
            Console.Write('^');
            Sleep();
        }

        public void DrawCurrentAnswer(int firstDigitIndex, int firstDigitLength, int secondDigitIndex, int secondDigitLength)
        {
            if (firstDigitIndex < 0 || secondDigitIndex < 0)
            {
                return;
            }
            Console.SetCursorPosition(0, answerCursorTop);
            Console.CursorLeft = 0;

            Console.Write("Answer:");
            Console.Write(inputLine.Substring(firstDigitIndex, firstDigitLength));
            Console.Write(inputLine.Substring(secondDigitIndex, secondDigitLength));
            Sleep();
        }
    }
}