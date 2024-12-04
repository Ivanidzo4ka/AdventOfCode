PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var (a, b) = ReadData(test);
    var ans = 0;
    a.Sort();
    b.Sort();
    for (int i = 0; i < a.Count; i++)
    {
        ans += Math.Abs(a[i] - b[i]);
    }

    if (test)
        Console.Write("Test:");
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var (a, b) = ReadData(test);
    var dic = b.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
    var ans = 0L;
    foreach (var x in a)
    {
        dic.TryGetValue(x, out var mul);
        ans += (x * mul);
    }
    if (test)
        Console.Write("Test:");
    Console.WriteLine(ans);
}
(List<int> a, List<int> b) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var a = new List<int>();
    var b = new List<int>();
    foreach (var line in lines)
    {
        var sp = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        a.Add(int.Parse(sp[0]));
        b.Add(int.Parse(sp[1]));
    }
    return (a, b);
}