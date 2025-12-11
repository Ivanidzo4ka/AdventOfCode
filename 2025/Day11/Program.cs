Dictionary<string, long> _dp = new Dictionary<string, long>();


PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
    {
        Console.Write("Test:");
    }
    _dp.Clear();
    _dp["out"] = 1;

    ans = Walk("you", new HashSet<string>(), data);
    Console.WriteLine(ans);
}

long Walk(string cur, HashSet<string> visited, Dictionary<string, List<string>> edges)
{
    if (cur == "out") return 1;
    var ans = 0L;
    if (_dp.ContainsKey(cur)) return _dp[cur];
    foreach (var edge in edges[cur])
    {
        if (!visited.Contains(edge))
        {
            visited.Add(edge);
            ans += Walk(edge, visited, edges);
            visited.Remove(edge);
        }
    }
    _dp[cur] = ans;
    return ans;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test, "test2.txt");
    var ans = 0L;
    if (test)
    {
        Console.Write("Test:");
    }
    _dp.Clear();
    _dp["out"] = 1;

    ans = Walk2("svr", new HashSet<string>(), data,0);
    Console.WriteLine(ans);
}

long Walk2(string cur, HashSet<string> visited, Dictionary<string, List<string>> edges, int count)
{
    if (cur == "out") return count == 3 ? 1 : 0;
    var ans = 0L;
    if (_dp.ContainsKey(cur + count)) return _dp[cur + count];
    var add = 0;
    if (cur == "dac") add = 1;
    if (cur == "fft") add = 2;
    foreach (var edge in edges[cur])
    {
        if (!visited.Contains(edge))
        {
            visited.Add(edge);
            ans += Walk2(edge, visited, edges, count + add);
            visited.Remove(edge);
        }
    }
    _dp[cur + count] = ans;
    return ans;
}
Dictionary<string, List<string>> ReadData(bool test = false, string testName = null)
{
    var lines = File.ReadAllLines(test ? testName ?? "test.txt" : "input.txt");
    Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
    foreach (var line in lines)
    {
        var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        dic.Add(split[0], split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());

    }
    return dic;
}