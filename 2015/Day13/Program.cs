PartOne();
PartTwo();
void PartOne()
{
    var ans = 0l;
    var data = Parse("input.txt");
    var set = data.Keys.ToHashSet();
    var sitting = new List<string>(set.Count);
    ans = Maximize(data, set, sitting);
    Console.WriteLine(ans);
}
long Maximize(Dictionary<string, Dictionary<string, int>> data, HashSet<string> set, List<string> sitting)
{
    var ans = long.MinValue;
    if (set.Count == 0)
        return CalcualteCost(sitting, data);
    foreach (var elem in set.ToList())
    {
        set.Remove(elem);
        sitting.Add(elem);
        ans = Math.Max(ans, Maximize(data, set, sitting));
        sitting.RemoveAt(sitting.Count - 1);
        set.Add(elem);
    }
    return ans;
}
long CalcualteCost(List<string> sitting, Dictionary<string, Dictionary<string, int>> data)
{
    var ans = 0l;
    for (int i = 0; i < sitting.Count; i++)
    {
        var left = (i == 0) ? sitting[^1] : sitting[i - 1];
        var right = (i == sitting.Count - 1) ? sitting[0] : sitting[i + 1];
        ans += data[sitting[i]][left];
        ans += data[sitting[i]][right];
    }
    return ans;
}
void PartTwo()
{
    var ans = 0l;
    var data = Parse("input.txt");
    data["ivan"] = new Dictionary<string, int>();
    foreach(var kvp in data)
    {
        kvp.Value["ivan"]=0;
        data["ivan"][kvp.Key] = 0;
    }
    var set = data.Keys.ToHashSet();
    var sitting = new List<string>(set.Count);
    ans = Maximize(data, set, sitting);
    Console.WriteLine(ans);
}

Dictionary<string, Dictionary<string, int>> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var data = new Dictionary<string, Dictionary<string, int>>();
    foreach (var line in lines)
    {
        var split = line.Split(" happiness units by sitting next to ");
        var to = split[1].Substring(0, split[1].Length-1);
        var cur = "";
        var gain = 0;
        if (split[0].Contains("lose "))
        {
            var loseSplit = split[0].Split(" would lose ");
            cur = loseSplit[0];
            gain = -int.Parse(loseSplit[1]);
        }
        else
        {
            var gainSplit = split[0].Split(" would gain ");
            cur = gainSplit[0];
            gain = int.Parse(gainSplit[1]);
        }
        if (!data.TryGetValue(cur, out var toDic))
        {
            toDic = new Dictionary<string, int>();
            data[cur] = toDic;
        }
        toDic[to] = gain;
    }
    return data;
}