int MAX = int.MinValue;
int first = 0;
StepOne();
StepTwo();
void StepOne()
{

    Dictionary<string, int> valveMap = new Dictionary<string, int>();
    List<int> valveRate = new List<int>();
    Dictionary<string, HashSet<string>> paths = new Dictionary<string, HashSet<string>>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(";");
        var valve = split[0].Substring("Valve ".Length, 2);
        var rate = int.Parse(split[0].Substring("Valve AA has flow rate=".Length));
        valveMap.Add(valve, valveMap.Count);
        valveRate.Add(rate);
        var check = " tunnels lead to valves ";
        if (split[1].StartsWith(" tunnel leads"))
            check = " tunnel leads to valve";
        var others = split[1].Substring(check.Length);

        paths[valve] = new HashSet<string>();
        foreach (var other in others.Split(","))
        {
            paths[valve].Add(other.Trim());
        }
    }
    var N = valveMap.Count;
    var map = new int[N, N];
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            map[i, j] = int.MaxValue / 2;
        }
        map[i, i] = 0;
    }

    foreach (var kvp in paths)
    {
        var x = valveMap[kvp.Key];
        foreach (var lead in kvp.Value)
        {
            var y = valveMap[lead];
            map[x, y] = 1;
        }
    }

    for (int z = 0; z < N; z++)
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                if (map[i, z] + map[z, j] < map[i, j])
                    map[i, j] = map[i, z] + map[z, j];

    first = valveMap.Where(x => x.Key == "AA").First().Value;
    Walk(valveRate, first, map, 0, 30, false);
    Console.WriteLine(MAX);
}

void StepTwo()
{

    Dictionary<string, int> valveMap = new Dictionary<string, int>();
    List<int> valveRate = new List<int>();
    Dictionary<string, HashSet<string>> paths = new Dictionary<string, HashSet<string>>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(";");
        var valve = split[0].Substring("Valve ".Length, 2);
        var rate = int.Parse(split[0].Substring("Valve AA has flow rate=".Length));
        valveMap.Add(valve, valveMap.Count);
        valveRate.Add(rate);
        var check = " tunnels lead to valves ";
        if (split[1].StartsWith(" tunnel leads"))
            check = " tunnel leads to valve";
        var others = split[1].Substring(check.Length);

        paths[valve] = new HashSet<string>();
        foreach (var other in others.Split(","))
        {
            paths[valve].Add(other.Trim());
        }
    }
    var N = valveMap.Count;
    var map = new int[N, N];
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            map[i, j] = int.MaxValue / 2;
        }
        map[i, i] = 0;
    }

    foreach (var kvp in paths)
    {
        var x = valveMap[kvp.Key];
        foreach (var lead in kvp.Value)
        {
            var y = valveMap[lead];
            map[x, y] = 1;
        }
    }

    for (int z = 0; z < N; z++)
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                if (map[i, z] + map[z, j] < map[i, j])
                    map[i, j] = map[i, z] + map[z, j];

    first = valveMap.Where(x => x.Key == "AA").First().Value;
    Walk(valveRate, first, map, 0, 26, true);
    Console.WriteLine(MAX);
}

void Walk(List<int> valveRate, int current, int[,] map, int score, int time, bool human)
{
    for (int i = 0; i < valveRate.Count; i++)
    {
        if (valveRate[i] != 0 && time > map[current, i] + 1)
        {
            var rate = valveRate[i];
            valveRate[i] = 0;

            Walk(valveRate, i, map, score + rate * (time - map[current, i] - 1), time - map[current, i] - 1, human);

            valveRate[i] = rate;
        }
    }
    if (human)
    {
        Walk(valveRate, first, map, score, 26, false);
    }
    else
    {
        if (score > MAX)
            MAX = score;
    }
}