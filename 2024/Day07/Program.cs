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
    foreach (var (res, vals) in data)
    {
        if (CanMake(vals, res))
            ans += res;
    }
    Console.WriteLine(ans);
}

bool CanMake(List<long> vals, long res)
{
    var m = (1 << (vals.Count - 1));
    for (int i = 0; i < m; i++)
    {
        var t = vals[0];
        for (int j = 1; j < vals.Count; j++)
        {
            if (0 != (i & (1 << (j - 1))))
            {
                t *= vals[j];
            }
            else t += vals[j];
        }
        if (t == res) return true;

    }
    return false;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    foreach (var (res, vals) in data)
    {
        if (CanMakeTreeOps(vals, 1, res, vals[0]))
            ans += res;
    }
    Console.WriteLine(ans);
}

bool CanMakeTreeOps(List<long> vals, int pos, long res, long cur)
{
    if (pos == vals.Count) return res == cur;
    var sum = CanMakeTreeOps(vals, pos + 1, res, cur + vals[pos]);
    if (sum) return true;
    var mul = CanMakeTreeOps(vals, pos + 1, res, cur * vals[pos]);
    if (mul) return true;

    var shift = CanMakeTreeOps(vals, pos + 1, res, long.Parse(cur.ToString() + vals[pos].ToString()));
    if (shift) return true;
    return false;
}
List<(long res, List<long> val)> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var res = new List<(long res, List<long> val)>();
    foreach (var line in lines)
    {
        var cur = new List<long>();
        var s = line.Split(":");

        res.Add((long.Parse(s[0]), s[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList()));
    }
    return res;
}