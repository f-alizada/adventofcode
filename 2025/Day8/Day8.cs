
using System;

using AdventOfCode.Common;

public class Day8
{
    public static void Main(string[] args)
    {
        var coordinates = Parsers.GetTwoDimensionalSplittedLongArray(',');

        Console.WriteLine("Problem 1:");
        Console.WriteLine(Problem1(coordinates));
        Console.WriteLine("----");

        Console.WriteLine("Problem 2:");
        Console.WriteLine(Problem2(coordinates));
    }

    public static long Problem1(long[][] coordinates)
    {
        var JBoxList = new JBox[coordinates.Length];
        for (var c = 0; c < coordinates.Length; c++) 
        {
            JBoxList[c] = new JBox { positionIndex = c, positionx = coordinates[c][0], positiony = coordinates[c][1], positionz = coordinates[c][2] };
        }

        var closestList = new List<ClosestJbox>();
        
        for(var i = 0; i < JBoxList.Length; i++)
        {
            for(var j = i+1;  j < JBoxList.Length; j++)
            {
                closestList.Add(new ClosestJbox()
                {
                    position0 = i, 
                    position1 = j,
                    distance = JBoxList[i].Distance(JBoxList[j])
                });
            }
        }

        closestList.Sort((x, y) => x.distance.CompareTo(y.distance));

        for (var i = 0; i < 10; i++)
        {
            if (JBoxList[closestList[i].position0].c is null && JBoxList[closestList[i].position1].c is null)
            {
                var circuit = new Circuit()
                {
                    ConnectedJboxes = new HashSet<JBox>()
                    {
                        JBoxList[closestList[i].position0],
                        JBoxList[closestList[i].position1]
                    }
                };
                
                JBoxList[closestList[i].position0].c = circuit;
                JBoxList[closestList[i].position1].c = circuit;
            } 
            else if (JBoxList[closestList[i].position0].c is not null && JBoxList[closestList[i].position1].c is not null)
            {
                foreach(var connectedJBox in JBoxList[closestList[i].position1].c.ConnectedJboxes)
                {
                    connectedJBox.c = JBoxList[closestList[i].position0].c;
                    JBoxList[closestList[i].position0].c.ConnectedJboxes.Add(connectedJBox);
                }
            }
            else if (JBoxList[closestList[i].position0].c is not null && JBoxList[closestList[i].position1].c is null)
            {
                JBoxList[closestList[i].position0].c.ConnectedJboxes.Add(JBoxList[closestList[i].position1]);
                JBoxList[closestList[i].position1].c = JBoxList[closestList[i].position0].c;
            }
            else if (JBoxList[closestList[i].position1].c is not null && JBoxList[closestList[i].position0].c is null)
            {
                JBoxList[closestList[i].position1].c.ConnectedJboxes.Add(JBoxList[closestList[i].position0]);
                JBoxList[closestList[i].position0].c = JBoxList[closestList[i].position1].c;
            }
        }

        var cList = new HashSet<Circuit?>(JBoxList.Select(x => x.c).Where(x=>x is not null).ToList()).ToList();

        cList.Sort((x, y) => x.count.CompareTo(y.count));
        cList.Reverse();

        return cList[0].count * cList[1].count * cList[2].count;
    }

    public static long Problem2(long[][] coordinates)
    {
        var JBoxList = new JBox[coordinates.Length];
        for (var c = 0; c < coordinates.Length; c++)
        {
            JBoxList[c] = new JBox { positionIndex = c, positionx = coordinates[c][0], positiony = coordinates[c][1], positionz = coordinates[c][2] };
        }

        var closestList = new List<ClosestJbox>();

        for (var i = 0; i < JBoxList.Length; i++)
        {
            for (var j = i + 1; j < JBoxList.Length; j++)
            {
                closestList.Add(new ClosestJbox()
                {
                    position0 = i,
                    position1 = j,
                    distance = JBoxList[i].Distance(JBoxList[j])
                });
            }
        }

        closestList.Sort((x, y) => x.distance.CompareTo(y.distance));

        (JBox, JBox) lastConnected = (null, null);
        for (var i = 0; i < closestList.Count; i++)
        {
            if (JBoxList[closestList[i].position0].c is null && JBoxList[closestList[i].position1].c is null)
            {
                var circuit = new Circuit()
                {
                    ConnectedJboxes = new HashSet<JBox>()
                    {
                        JBoxList[closestList[i].position0],
                        JBoxList[closestList[i].position1]
                    }
                };

                JBoxList[closestList[i].position0].c = circuit;
                JBoxList[closestList[i].position1].c = circuit;

                lastConnected = (JBoxList[closestList[i].position0], JBoxList[closestList[i].position1]);
            }
            else if (JBoxList[closestList[i].position0].c is not null && JBoxList[closestList[i].position1].c is not null && JBoxList[closestList[i].position0].c != JBoxList[closestList[i].position1].c)
            {
                foreach (var connectedJBox in JBoxList[closestList[i].position1].c.ConnectedJboxes)
                {
                    connectedJBox.c = JBoxList[closestList[i].position0].c;
                    JBoxList[closestList[i].position0].c.ConnectedJboxes.Add(connectedJBox);
                }
                lastConnected = (JBoxList[closestList[i].position0], JBoxList[closestList[i].position1]);

            }
            else if (JBoxList[closestList[i].position0].c is not null && JBoxList[closestList[i].position1].c is null)
            {
                JBoxList[closestList[i].position0].c.ConnectedJboxes.Add(JBoxList[closestList[i].position1]);
                JBoxList[closestList[i].position1].c = JBoxList[closestList[i].position0].c;
                lastConnected = (JBoxList[closestList[i].position0], JBoxList[closestList[i].position1]);

            }
            else if (JBoxList[closestList[i].position1].c is not null && JBoxList[closestList[i].position0].c is null)
            {
                JBoxList[closestList[i].position1].c.ConnectedJboxes.Add(JBoxList[closestList[i].position0]);
                JBoxList[closestList[i].position0].c = JBoxList[closestList[i].position1].c;
                lastConnected = (JBoxList[closestList[i].position0], JBoxList[closestList[i].position1]);
            }
        }

        return lastConnected.Item1.positionx * lastConnected.Item2.positionx;

    }
}

public class ClosestJbox
{
    public int position0 { get; set; }

    public int position1 { get; set; }

    public double distance {  get; set; }
}

public class JBox
{
    public string ID { get; set; } = Guid.NewGuid().ToString();

    public int positionIndex { get; set; }

    public long positionx {  get; set; }
    
    public long positiony { get; set; }
    
    public long positionz { get; set; }

    public Circuit? c { get; set; } 
    
    public int CCount => c is null ? 1 : c.count;
    public double Distance(JBox jBox)
    {
        var px = this.positionx - jBox.positionx;
        var py = this.positiony - jBox.positiony;
        var pz = this.positionz - jBox.positionz;
        return Math.Sqrt(px * px + py * py + pz * pz);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ID);
    }


    public override bool Equals(object obj)
    {
        if (obj is JBox other)
        {
            return other.ID.Equals(this.ID);
        }
        return false;
    }
}

public class Circuit
{
    public string  ID { get; set; } = Guid.NewGuid().ToString();

    public int count => ConnectedJboxes is null ? 1 : ConnectedJboxes.Count;
    
    public HashSet<JBox> ConnectedJboxes { get; set; } = new HashSet<JBox>();

    public int index {  get; set; }

    public int CompareTo(Circuit c)
    {
        if (this.ID.Equals(c.ID)) return 0;

        return this.count.CompareTo(c.count);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ID);
    }

    public override bool Equals(object obj)
    {
        if (obj is Circuit other)
        {
            return other.ID.Equals(this.ID);
        }
        return false;
    }
}