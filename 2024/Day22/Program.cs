using System.IO.Compression;
using System.Text;
var dir = new (int x, int y)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };

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

    foreach (var item in data)
    {
        var d = item;
        for (int i = 0; i < 2000; i++)
        {
            d = Evolve(d);
        }
        ans += d;

    }
    Console.WriteLine(ans);
}
long Evolve(long cur)
{
    cur = Prune(Mix(cur, cur * 64));
    cur = Prune(Mix(cur, cur / 32));
    cur = Prune(Mix(cur, cur * 2048));
    return cur;
}
long Mix(long secret, long value)
{
    return secret ^ value;
}
long Prune(long secret)
{
    return secret % 16777216;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    if (test)
        Console.Write("Test:");
    var diffs = new Dictionary<(int a, int b, int c, int d), int>();
    foreach (var item in data)
    {
        var cur = item;
        var a = 0;
        var b = 0;
        var c = 0;
        var d = 0;
        var set = new HashSet<(int a, int b, int c, int d)>();
        for (int i = 0; i < 2000; i++)
        {
            var next = Evolve(cur);
            var diff = next % 10 - cur % 10;
            a = b;
            b = c;
            c = d;
            d = (int)diff;
            cur = next;
            if (i >= 3)
            {
                if (set.Add((a, b, c, d)))
                {
                    diffs.TryGetValue((a, b, c, d), out var acc);
                    diffs[(a, b, c, d)] = acc + (int)cur % 10;
                }
            }
        }
    }
    Console.WriteLine(diffs.Values.Max());
}

List<long> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => long.Parse(x)).ToList();

}