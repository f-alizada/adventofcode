public class Day1
{
  public static void Main(string[] args)
  {
    var actionsNavigation = new List<char>();
    var actionsSteps = new List<int>();

    var fileName = "input.test.txt";
    string? line = string.Empty;
    
    using (var fs = File.OpenText(fileName))
    {
      while((line  = fs.ReadLine()) != null){
                actionsNavigation.Add(line[0]);
                actionsSteps.Add(int.Parse(line.Substring(1)));
            }
    }

    Console.WriteLine("Problem 1:");
    Console.WriteLine(Problem1(actionsNavigation, actionsSteps));
    Console.WriteLine("----");

    Console.WriteLine("Problem 2:");
    Console.WriteLine(Problem2(actionsNavigation, actionsSteps));

  }

    public static int Problem1(List<char> actionsNavigation, List<int> actionsSteps){
        var currentPoint = 50;
        var result = 0;

        for (var i = 0 ;  i < actionsNavigation.Count; i++) {
            var steps = actionsSteps[i];
            if (actionsSteps[i] > 99)
            {
                steps %= 100;
            }

            if (actionsNavigation[i] == 'L')
            {
                steps = steps * -1;
            }
            
            currentPoint += steps;

            if (currentPoint == 0 || currentPoint == 100)
            {
                result++;
            }
            else if (currentPoint < 0)
            {
                currentPoint = 100 + currentPoint; 
            }
            else if (currentPoint > 99)
            {
                currentPoint = currentPoint - 100;
            }
            Console.WriteLine($"CurrentPoint {actionsNavigation[i]}{actionsSteps[i]}  {currentPoint}");
        }
    
        return result;
    }

    public static int Problem2(List<char> actionsNavigation, List<int> actionsSteps)
    {
        var currentPoint = 50;
        var result = 0;

        for (var i = 0; i < actionsNavigation.Count; i++)
        {
            var steps = actionsSteps[i];
            if (steps > 99)
            {
                result += steps / 100;
                steps %= 100;
            }

            if (actionsNavigation[i] == 'L')
            {
                steps = steps * -1;
            }

            var originalPointWasAt0 = (currentPoint == 0 || currentPoint == 100);
            currentPoint += steps;

            if (currentPoint == 0 || currentPoint == 100)
            {
                result++;
            }
            else if (currentPoint < 0)
            {
                if (!originalPointWasAt0)
                {
                    result++;
                }
                currentPoint = 100 + currentPoint;
            }
            else if (currentPoint > 99)
            {
                if (!originalPointWasAt0)
                {
                    result++;
                }
                currentPoint = currentPoint - 100;
            }
            Console.WriteLine($"CurrentPoint {actionsNavigation[i]}{actionsSteps[i]}  {currentPoint} with 0 hits {result}");
        }

        return result;
    }
}