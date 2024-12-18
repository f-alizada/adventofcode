using System.Text.RegularExpressions;

using AdventOfCode.Common;

public class Day17
{
    public static void Main(string[] args)
    {
        var inputData = Parsers.ReadAllLines();

        var registersRegex = new Regex("Register [A|B|C]: (?<registerValue>\\d{1,})");

        registersRegex.Match(inputData[0]).Groups.TryGetValue("registerValue", out var registerA);
        registersRegex.Match(inputData[1]).Groups.TryGetValue("registerValue", out var registerB);
        registersRegex.Match(inputData[2]).Groups.TryGetValue("registerValue", out var registerC);

        var inputProgram = inputData[4].Split("Program: ")[1].Split(",").Select(s => s.Trim()).Select(int.Parse).ToList();
        var registers = new Registers {
            RegisterA = int.Parse(registerA!.Value.ToString()),
            RegisterB = int.Parse(registerB!.Value.ToString()),
            RegisterC = int.Parse(registerC!.Value.ToString())
        };

        Console.WriteLine("Problem 1:");
        Console.WriteLine(
            Problem1(inputProgram.ToArray(), registers));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        //Console.WriteLine(Problem2(mapP2));
    }

    public static long? Problem1(int[] inputProgram, Registers registers)
    {
        long result = 0;
        var currentIndex = 0;
        var outputValues = new List<int>();
        while (true)
        {
            if (currentIndex >= inputProgram.Length)
            {
                break;
            }
            var comboOperand = GetOperandComboValue(inputProgram[currentIndex + 1], registers);
            var literalOperand = inputProgram[currentIndex + 1];
            var jump = true;

            switch (inputProgram[currentIndex])
            {
                case 0:
                    registers.RegisterA = (registers.RegisterA / (int)Math.Pow(2, comboOperand));
                    break;
                case 1:
                    registers.RegisterB = registers.RegisterB ^ inputProgram[currentIndex + 1];
                    break;
                case 2:
                    registers.RegisterB = comboOperand % 8;
                    break;
                case 3:
                    if (registers.RegisterA == 0)
                    {
                        break;
                    }
                    currentIndex = literalOperand;
                    jump = false;
                    break;
                case 4:
                    registers.RegisterB = registers.RegisterB ^ registers.RegisterC;
                    break;
                case 5:
                    outputValues.Add(comboOperand % 8);
                    break;
                case 6:
                    registers.RegisterB = (registers.RegisterA / (int)Math.Pow(2, comboOperand));
                    break;
                case 7:
                    registers.RegisterC = (registers.RegisterA / (int)Math.Pow(2, comboOperand));
                    break;
            }

            if (jump)
            {
                currentIndex += 2;
            }
        }

        Console.WriteLine(string.Join(",", outputValues));
        
        return result;
    }

    public static long Problem2(char[][] map)
    {
        return 0;
    }

    public static int GetOperandComboValue(int operand, Registers registers)
    {
        if (operand >= 0 && operand <= 3)
        {
            return operand;
        }
        
        return operand switch
        {
            4 => registers.RegisterA,
            5 => registers.RegisterB,
            6 => registers.RegisterC,
            _ => throw new NotImplementedException(),
        };
    }
}

public class Registers
{
    public int RegisterA { get; set; }
    public int RegisterB { get; set; }
    public int RegisterC { get; set; }
}