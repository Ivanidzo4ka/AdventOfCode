
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
    foreach (var x in data.ing)
    {
        for (int i = 0; i < data.ranges.Count; i++)
        {
            var (l, r) = data.ranges[i];
            if (x >= l && x <= r)
            {
                ans++;
                break;
            }
        }
    }
    Console.WriteLine(ans);
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var sort = data.ranges.OrderBy(x => x.x).ThenBy(x => x.y).ToArray();
    var i = 0;
    var merged = new List<(long x, long y)>();
    while (i < sort.Length)
    {
        var (curL, curR) = sort[i];
        i++;
        while (i < sort.Length && sort[i].x <= curR)
        {
            curR = Math.Max(curR, sort[i].y);
            i++;
        }
        merged.Add((curL, curR));
    }
    foreach (var (x, y) in merged)
    {
        ans += y - x + 1;
    }

    Console.WriteLine(ans);
}
(List<(long x, long y)> ranges, List<long> ing) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var i = 0;
    var ranges = new List<(long x, long y)>();
    var ing = new List<long>();
    while (lines[i] != "")
    {
        ranges.Add((long.Parse(lines[i].Split('-')[0]), long.Parse(lines[i].Split('-')[1])));
        i++;
    }
    i++;
    while (i < lines.Length)
    {
        ing.Add(long.Parse(lines[i]));
        i++;
    }
    return (ranges, ing);
}