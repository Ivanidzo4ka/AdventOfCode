Dictionary<(int pos, int level), long> _dp = new();

PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var pos = data[0].IndexOf('S');
    var set = new HashSet<int>();
    set.Add(pos);
    var line = 1;
    while (line < data.Count)
    {
        var newSet = new HashSet<int>();
        foreach (var beam in set)
        {
            if (data[line][beam] == '^')
            {
                if (beam - 1 >= 0)
                    newSet.Add(beam - 1);
                if (beam + 1 < data[0].Length)
                    newSet.Add(beam + 1);
                ans++;
            }
            else newSet.Add(beam);
        }
        set = newSet;
        line++;
    }

    Console.WriteLine(ans);
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var pos = data[0].IndexOf('S');
    ans = CalcBeams(pos, 1, data);
    Console.WriteLine(ans);
}
long CalcBeams(int pos, int level, IList<string> data)
{
    if (level >= data.Count)
        return 1;
    if (_dp.ContainsKey((pos, level)))
        return _dp[(pos, level)];
    long ans = 0;
    if (data[level][pos] == '^')
    {
        if (pos - 1 >= 0)
            ans += CalcBeams(pos - 1, level + 1, data);
        if (pos + 1 < data[0].Length)
            ans += CalcBeams(pos + 1, level + 1, data);
    }
    else
        ans += CalcBeams(pos, level + 1, data);
    _dp[(pos, level)] = ans;
    return ans;
}
IList<string> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines;
}