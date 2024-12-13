public class Day5
{
    public static void Main(string[] args)
    {
        var fileName = "input.test.txt";
        string? line = string.Empty;

        var orderNotAllowedRules = new Dictionary<int, HashSet<int>>();
        var updates = new List<int[]>();

        var update = new List<int>();
        using (var fs = File.OpenText(fileName))
        {
            while ((line = fs.ReadLine()) != null)
            {
                if (line.Contains("|"))
                {
                    // parse rule
                    var splitted = line.Split(['|']);
                    if (orderNotAllowedRules.TryGetValue(int.Parse(splitted[1]), out HashSet<int>? rules))
                    {
                        rules.Add(int.Parse(splitted[0]));
                    }
                    else
                    {
                        orderNotAllowedRules.Add(int.Parse(splitted[1]), new HashSet<int>() { int.Parse(splitted[0]) });
                    };
                }
                else if (line.Contains(","))
                {
                    update.Clear();
                    foreach (var up in line.Split(','))
                    {
                        update.Add(int.Parse(up.Trim()));
                    }
                    updates.Add(update.ToArray());
                }
            }
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(orderNotAllowedRules, updates.ToArray()));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(orderNotAllowedRules, updates.ToArray()));
    }

    public static int Problem1(Dictionary<int, HashSet<int>> rules, int[][] updates)
    {
        var sum = 0;
        foreach (var update in updates)
        {
            if (IsValidUpdate(rules, update))
            {
                sum += update[update.Length / 2];
            }
        }

        return sum;
    }
    public static int Problem2(Dictionary<int, HashSet<int>> rules, int[][] updates)
    {
        var sum = 0;
        foreach (var update in updates)
        {
            if (!IsValidUpdate(rules, update))
            {
                FixTheOrder(rules, update);
                sum += update[update.Length / 2];
            }
        }
        return sum;
    }


    private static void FixTheOrder(Dictionary<int, HashSet<int>> rules, int[] update)
    {
        HashSet<int>[] localRuleStore = new HashSet<int>[update.Length];
        for (int i = 0; i < update.Length; i++)
        {
            if (rules.TryGetValue(update[i], out HashSet<int>? value))
            {
                localRuleStore[i] = value;
            }
        }

        for (int i = 0; i < update.Length; i++)
        {
            for (var k = 0; k < localRuleStore.Length; k++)
            {
                if (localRuleStore[k] is not null && localRuleStore[k].Contains(update[i]))
                {
                    (update[k], update[i]) = (update[i], update[k]);
                    (localRuleStore[k], localRuleStore[i]) = (localRuleStore[i], localRuleStore[k]);
                }
            }
        }
    }

    private static bool IsValidUpdate(Dictionary<int, HashSet<int>> rules, int[] update)
    {
        var localRuleStore = new HashSet<int>();
        for (int i = 0; i < update.Length; i++)
        {
            if (localRuleStore.Contains(update[i]))
            {
                return false;
            }
            if (rules.TryGetValue(update[i], out HashSet<int>? r))
            {
                foreach (var rule in r)
                {
                    localRuleStore.Add(rule);
                }
            }
        }
        return true;
    }
}