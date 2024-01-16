using System.Threading;
using System.Diagnostics;
//StepOne();
StepTwo();
void StepOne()
{
    var l = new List<Blueprint>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var b = new Blueprint(line, 24);
        l.Add(b);
    }


    var ans = 0;
    Parallel.ForEach(l, (x => x.Start()));
    for (int i = 0; i < l.Count; i++)
    {
        ans += (i + 1) * l[i].MaxGeode;
    }
    Console.WriteLine(ans);
}


void StepTwo()
{
    var l = new List<Blueprint>();
    foreach (var line in File.ReadAllLines("input.txt").Take(3))
    {
        var b = new Blueprint(line, 32);
        l.Add(b);
    }


    var ans = 1;
     Parallel.ForEach(l, (x => x.Start()));
    for (int i = 0; i < l.Count; i++)
    {
        ans *= l[i].MaxGeode;
    }
    Console.WriteLine(ans);
}

public class Blueprint
{
    public int OreCost;
    public int ClayCost;

    public (int ore, int clay) ObsidianCost;
    public (int ore, int obsidian) GeodeCost;


    public int MaxGeode;


    public Blueprint(string str, int time)
    {
        var split = str.Split(".");
        OreCost = split[0][split[0].IndexOf("costs") + 6] - '0';
        ClayCost = split[1][split[1].IndexOf("costs") + 6] - '0';
        var start = split[2].IndexOf("costs ");
        var end = split[2].IndexOf(" ore");
        var t = int.Parse(split[2].Substring(start + 6, end - start - 6));
        start = split[2].IndexOf("and ");
        end = split[2].IndexOf("clay");
        ObsidianCost = (t, int.Parse(split[2].Substring(start + 4, end - start - 4)));

        start = split[3].IndexOf("costs ");
        end = split[3].IndexOf("ore");
        t = int.Parse(split[3].Substring(start + 6, end - start - 6));
        start = split[3].IndexOf("and ");
        end = split[3].IndexOf("obsidian");
        GeodeCost = (t, int.Parse(split[3].Substring(start + 4, end - start - 4)));

        MaxGeode = 0;
        Time = time;
    }
    private int Time;
    private Stopwatch _stopWatch;

    public void Start()
    {
        _stopWatch = new Stopwatch();
        _stopWatch.Start();
        Run((0, 0, 0, 0), (1, 0, 0, 0), Time);
    }

    private void Run((int ore, int clay, int obsidian, int geode) resource, (int ore, int clay, int obsidian, int geode) robots, int time)
    {
        // need to find better way to cut branches.
        if (_stopWatch.Elapsed.TotalSeconds>10)
            return;
        if (time == 1)
        {
            if (MaxGeode < resource.geode + robots.geode)
                MaxGeode = resource.geode + robots.geode;
        }
        else
        {
            // Build OreOne
            if (resource.ore >= OreCost && robots.ore < ClayCost && Time-time < OreCost*ClayCost+2)
            {
                Run((
                    resource.ore - OreCost + robots.ore,
                    resource.clay + robots.clay,
                    resource.obsidian + robots.obsidian,
                    resource.geode + robots.geode),
                    (robots.ore + 1,
                    robots.clay,
                    robots.obsidian,
                    robots.geode), time - 1);
            }
            if (resource.ore >= ClayCost && robots.clay < ObsidianCost.clay)
            {
                Run((
                    resource.ore - ClayCost + robots.ore,
                    resource.clay + robots.clay,
                    resource.obsidian + robots.obsidian,
                    resource.geode + robots.geode),
                     (robots.ore, robots.clay + 1, robots.obsidian, robots.geode), time - 1);
            }

            // Build Geode
            if (resource.ore >= GeodeCost.ore && resource.obsidian >= GeodeCost.obsidian)
            {
                Run((
                    resource.ore - GeodeCost.ore + robots.ore,
                     resource.clay + robots.clay,
                     resource.obsidian + robots.obsidian - GeodeCost.obsidian,
                      resource.geode + robots.geode),
                    (robots.ore, robots.clay, robots.obsidian, robots.geode + 1), time - 1);
            }
            else
            // Build Obsidian
            if (resource.ore >= ObsidianCost.ore && resource.clay >= ObsidianCost.clay && robots.obsidian < GeodeCost.obsidian)
            {
                Run((
                    resource.ore - ObsidianCost.ore + robots.ore,
                    resource.clay - ObsidianCost.clay + robots.clay,
                    resource.obsidian + robots.obsidian,
                    resource.geode + robots.geode),
                 (robots.ore, robots.clay, robots.obsidian + 1, robots.geode), time - 1);
            }
            else
            {


                Run((
                    resource.ore + robots.ore,
                    resource.clay + robots.clay,
                    resource.obsidian + robots.obsidian,
                    resource.geode + robots.geode),
                    (robots.ore, robots.clay, robots.obsidian, robots.geode), time - 1);
            }
        }
    }


}