using System.Security.Cryptography.X509Certificates;

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
    var n = data.Length;
    var m = data[0].Length;
    var dir = new (int, int)[]
    {
        (-1,0),(1,0),(0,-1),(0,1),(-1,-1),(1,1),(1,-1),(-1,1)
    };
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            var c = data[i][j];
            if (c == '@')
            {
                var count = 0;
                foreach (var (dx, dy) in dir)
                {
                    var ni = i + dx;
                    var nj = j + dy;
                    if (ni < 0 || ni >= n || nj < 0 || nj >= m)
                        continue;
                    if (data[ni][nj] == '@')
                    {
                        count++;
                    }
                }

                if (count < 4)
                    ans++;
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
    var n = data.Length;
    var m = data[0].Length;
    var dir = new (int, int)[]
    {
        (-1,0),(1,0),(0,-1),(0,1),(-1,-1),(1,1),(1,-1),(-1,1)
    };
    while (true)
    {
        var l = new List<(int x, int y)>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                var c = data[i][j];
                if (c == '@')
                {
                    var count = 0;
                    foreach (var (dx, dy) in dir)
                    {
                        var ni = i + dx;
                        var nj = j + dy;
                        if (ni < 0 || ni >= n || nj < 0 || nj >= m)
                            continue;
                        if (data[ni][nj] == '@')
                        {
                            count++;
                        }
                    }

                    if (count < 4)
                    {
                        ans++;
                        l.Add((i, j));
                    }
                }
            }
        }
        if (l.Count == 0)
            break;
        else
        {
            foreach (var remove in l)
            {
                data[remove.x][remove.y] = '.';
            }
        }

    }
    Console.WriteLine(ans);
}
char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.ToCharArray()).ToArray();
}