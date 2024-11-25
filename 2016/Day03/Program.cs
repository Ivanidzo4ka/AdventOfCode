PartOne();
PartTwo();
void PartOne()
{
    var lines = File.ReadAllLines("input.txt");
    var ans = 0;
    foreach (var line in lines)
    {
        var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        if (IsTriangle(split))
            ans++;
    }
    Console.WriteLine(ans);
}
bool IsTriangle(IList<int> points)
{
    if (points[0] + points[1] <= points[2]) { return false; }
    if (points[0] + points[2] <= points[1]) { return false; }
    if (points[1] + points[2] <= points[0]) { return false; }
    return true;
}
void PartTwo()
{
    var lines = File.ReadAllLines("input.txt");
    var ans = 0;
    var a = new List<int>();
    var b = new List<int>();
    var c = new List<int>();
    foreach (var line in lines)
    {
        var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        a.Add(split[0]);
        b.Add(split[1]);
        c.Add(split[2]);
        if (a.Count == 3)
        {
            if (IsTriangle(a))
                ans++;
            if (IsTriangle(b))
                ans++;
            if (IsTriangle(c))
                ans++;
            a.Clear();
            b.Clear();
            c.Clear();
        }
    }
    Console.WriteLine(ans);
}