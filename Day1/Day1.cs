public class Day1
{
  public static void Main(string[] args)
  {
    var leftList = new List<int>();
    var rightList = new List<int>();

    var fileName = "input.test.txt";
    string? line = string.Empty;
    using (var fs = File.OpenText(fileName))
    {
      while((line  = fs.ReadLine()) != null){
        var splittedVals = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        // sorted insertion? 
        leftList.Add(int.Parse(splittedVals[0].Trim()));
        rightList.Add(int.Parse(splittedVals[1].Trim()));
      }
    }
    leftList.Sort();
    rightList.Sort();
    Console.WriteLine("Problem 1:");
    Console.WriteLine(Problem1(leftList, rightList));
    Console.WriteLine("----");

    Console.WriteLine("Problem 2:");
    Console.WriteLine(Problem2(leftList, rightList));

  }

  public static int Problem1(List<int> leftList, List<int> rightList){
    var result = 0;
    for(var i = 0 ; i < leftList.Count; i++){
      result += Math.Abs(leftList[i] - rightList[i]);
    }

    return result;
  }

  public static int Problem2(List<int> leftList, List<int> rightList){
    var result = 0;
    
    foreach(var leftVal in leftList){
      result += leftVal * rightList.Count(x => x == leftVal);
    }

    return result;
  }
}