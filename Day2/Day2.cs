public class Day1
{
  public static void Main(string[] args)
  {
    var levels = new List<int[]>();
    var levelRow = new List<int>();

    var fileName = "input.test.txt";
    string? line = string.Empty;
    using (var fs = File.OpenText(fileName))
    {
      while((line  = fs.ReadLine()) != null){
        var splittedVals = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach(var val in splittedVals)
        {
          levelRow.Add(int.Parse(val));
        }
        levels.Add(levelRow.ToArray());
        levelRow.Clear();
      }
    }

    Console.WriteLine("Problem 1:");
    Console.WriteLine(Problem1(levels));
    Console.WriteLine("----");

    Console.WriteLine("Problem 2:");
    Console.WriteLine(Problem2(levels));
  }

  private static int Problem2(List<int[]> levelsArray){
    var result = 0;
    foreach(var level in levelsArray){
      if (IsSafe(level)){
        result++;
        continue;
      }

      for(int i = 0 ; i < level.Length; i++){
        if (IsSafe(level, i)){
          result++;
          break;
        }
      }
    }
    return result;
  }

  private static int Problem1(List<int[]> levelsArray){
    var result = 0;
    foreach(var level in levelsArray){
      if (IsSafe(level)){
        result++;
      }
    }
    return result;
  }

  private static bool IsSafe(int[] levels, int? skipIndex = null)
  {
    var startIndex = skipIndex.HasValue && skipIndex == 0 ? 1 : 0;
    var prev = levels[startIndex];
    bool? isIncreasing = null;
    for(var i = startIndex + 1; i < levels.Length; i++)
      {
        if (skipIndex == i){
          continue;
        }
        var difference = Math.Abs(levels[i] - prev);
        if (difference < 1 || difference > 3)
        {
          return false;
        }
        
        var localIncreasing = prev > levels[i];
        if (!isIncreasing.HasValue)
        {
          isIncreasing = localIncreasing;
        }
        else
        {
          if (localIncreasing != isIncreasing.Value){
            return false;
          }
        }

        prev = levels[i];
      }

      return true;
  }
}