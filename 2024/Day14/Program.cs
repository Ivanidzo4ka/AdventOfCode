using System.Text;

PartOne(true);
PartOne(false);
PartTwo(false);

void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    var n = 103;
    var m = 101;
    if (test)
    {
        Console.Write("Test:");
        n = 7;
        m = 11;
    }
    var q = new int[4];
    var sec = 100;
    foreach (var robot in data)
    {
        var x = robot.y + sec * robot.vy;
        var y = robot.x + sec * robot.vx;
        x %= n;
        x = (x + n) % n;
        y %= m;
        y = (y + m) % m;
        if (x < n / 2 && y < m / 2) q[0]++;
        else if (x > n / 2 && y < m / 2) q[1]++;
        else if (x < n / 2 && y > m / 2) q[2]++;
        else if (x > n / 2 && y > m / 2) q[3]++;
    }
    Console.WriteLine(q[0] * q[1] * q[2] * q[3]);
}

void PartTwo(bool test = false)
{
    var ans = 0L;
    var n = 103;
    var m = 101;
    var data = ReadData(test);
    if (test)
    {
        Console.Write("Test:");
        n = 7;
        m = 11;
    }
    var q = new int[4];
    for (int i = 0; i < 10000; i++)
    {
        var set = new HashSet<(int x, int y)>();
        foreach (var robot in data)
        {
            var x = robot.y + i * robot.vy;
            var y = robot.x + i * robot.vx;
            x %= n;
            x = (x + n) % n;
            y %= m;
            y = (y + m) % m;
            set.Add((x, y));
        }
        var found = false;
        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < m; y++)
                if (set.Contains((x, y))
                    && set.Contains((x, y + 1))
                    && set.Contains((x, y + 2))
                    && set.Contains((x, y + 3))
                    && set.Contains((x, y + 4))
                    && set.Contains((x, y + 4))
                    && set.Contains((x, y + 5))
                    && set.Contains((x, y + 6))
                    && set.Contains((x, y + 7))
                    && set.Contains((x, y + 8))
                    )
                    found = true;
        }
        if (found)
        {
            Console.WriteLine(i);
            Print(set, n, m);
            break;
        }
    }
}
void Print(HashSet<(int x, int y)> set, int n, int m)
{
    Console.WriteLine("");
    for (int i = 0; i < n; i++)
    {
        var sb = new StringBuilder();
        for (int j = 0; j < m; j++)
        {
            if (set.Contains((i, j))) sb.Append("#");
            else sb.Append(".");
        }
        Console.WriteLine(sb.ToString());
    }
}

List<(int x, int y, int vx, int vy)> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var result = new List<(int x, int y, int vx, int vy)>();
    foreach (var line in lines)
    {
        var split = line.Split(" v=");
        var tp = split[0].Split(",");
        var tv = split[1].Split(",");
        var x = int.Parse(tp[0].Substring(2));
        var y = int.Parse(tp[1]);
        var vx = int.Parse(tv[0]);
        var vy = int.Parse(tv[1]);
        result.Add((x, y, vx, vy));
    }
    return result;
    //return lines.Select(x => x.ToCharArray()).ToArray();
}