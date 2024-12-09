public class Day9
{
    public static void Main(string[] args)
    {
        var fileName = "input.test.txt";
        string? input = string.Empty;

        using (var fs = File.OpenText(fileName))
        {
            input = fs.ReadToEnd();
        }

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(input));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(input));
    }

    public static long Problem1(string disk)
    {
        var elementCount = 0;
        foreach (var c in disk)
        {
            elementCount = elementCount + c - 48;
        }

        var diskMap = new int[elementCount];

        var freeSpace = false;
        var currentId = 0;
        var currentIndex = 0;
        var totalFileSpace = 0;
        foreach (var c in disk)
        {
            var amount = c - 48;
            var element = currentId;
            if (freeSpace)
            {
                element = -1;
            }

            for(var i = 0; i < amount; i++)
            {
                diskMap[currentIndex] = element;
                currentIndex++;
            }
            
            if (!freeSpace)
            {
                currentId++;
                totalFileSpace += amount;
            }

            freeSpace = !freeSpace;
        }

        PrintDiskMap(diskMap);

        var leftIndex = 0;
        var rightIndex = diskMap.Length - 1;
        Console.WriteLine($"Total fileSpace = {totalFileSpace}");
        while(leftIndex < totalFileSpace)
        {
            if (diskMap[leftIndex] == -1)
            {
                while(diskMap[rightIndex] == -1)
                {
                    rightIndex--;
                }
                diskMap[leftIndex] = diskMap[rightIndex];
                diskMap[rightIndex] = -1;
            }
            leftIndex++;
        }

        PrintDiskMap(diskMap);

        long sum = 0;
        for(var i = 0; i < totalFileSpace;i++)
        {
            sum = sum + (i * diskMap[i]);
        }

        return sum;
    }

    public static long Problem2(string disk)
    {
        var diskMap = new List<DiskSpace>();
        var freeSpace = false;
        var currentId = 0;
        foreach (var c in disk)
        {
            var amount = c - 48;

            diskMap.Add(new DiskSpace { count = amount, free = freeSpace, id = currentId });

            if (!freeSpace)
            {
                currentId++;
            }

            freeSpace = !freeSpace;
        }

        PrintDiskMap(diskMap);

        var rightIndex = diskMap.Count - 1;

        while (rightIndex >= 0)
        {
            if (!diskMap[rightIndex].free)
            {
                var requiredCount = diskMap[rightIndex].count;
                var fileId = diskMap[rightIndex].id;
                // try find place left

                var freeSpaceIndex = -1;
                for (var i = 0; i < rightIndex; i++)
                {
                    if (diskMap[i].free && diskMap[i].count >= requiredCount)
                    {
                        freeSpaceIndex = i;
                        break;
                    }
                }

                if (freeSpaceIndex >= 0)
                {
                    diskMap[freeSpaceIndex].count = diskMap[freeSpaceIndex].count - requiredCount;
                    diskMap[rightIndex].free = true;
                    diskMap.Insert(freeSpaceIndex, new DiskSpace { id = fileId, count = requiredCount, free = false });
                }
            }
            rightIndex--;
            
        }

        PrintDiskMap(diskMap);

        long sum = 0;
        var index = 0;
        for (var i = 0; i < diskMap.Count; i++)
        {
            if (diskMap[i].count == 0)
            {
                continue;
            }
            
            if (diskMap[i].free)
            {
                index+=diskMap[i].count;
            }
            else
            {
                for(var j = 0; j < diskMap[i].count; j++)
                {
                    sum = sum + diskMap[i].id * index;
                    index++;
                }
            }
        }

        return sum;
        
    }

    private static void PrintDiskMap(List<DiskSpace> diskMap)
    {
        for (int i = 0; i < diskMap.Count ; i++)
        {
            if (diskMap[i].free)
            {
                for(int j = 0; j < diskMap[i].count; j++)
                {
                    Console.Write('.');
                }
            }
            else
            {
                for (int j = 0; j < diskMap[i].count; j++)
                {
                    Console.Write(diskMap[i].id);
                }
                
            }

        }
        Console.WriteLine();
    }

    private static void PrintDiskMap(int[] diskMap)
    {
        for (int i = 0; i < diskMap.Length; i++)
        {
            if(diskMap[i] == -1)
            {
                Console.Write('.');
            }
            else
            {
                Console.Write(diskMap[i]);
            }
            
        }
        Console.WriteLine();
    }
}

class DiskSpace
{
    public int count { get; set; }
    public int id { get; set; }
    public bool free { get; set; }
}