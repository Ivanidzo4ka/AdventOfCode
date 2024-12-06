PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var n = data.Length;
    var dir = new (int x, int y)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
    var curD = 0;
    var ans = 0L;
    var startx = 0;
    var starty = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] == '^')
            {
                startx = i;
                starty = j;
                break;
            }
        }
    if (test)
        Console.Write("Test:");
    var set = new HashSet<(int x, int y)>();
    do
    {
        set.Add((startx, starty));
        var nextx = startx + dir[curD].x;
        var nexty = starty + dir[curD].y;

        if (nextx < 0 || nextx == n || nexty < 0 || nexty == n) break;
        if (data[nextx][nexty] == '#') { curD++; curD %= 4; }
        else { startx = nextx; starty = nexty; }
    } while (true);

    Console.WriteLine(set.Count);
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    var n = data.Length;
    var startx = 0;
    var starty = 0;
    if (test)
        Console.Write("Test:");
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] == '^')
            {
                startx = i;
                starty = j;
                break;
            }
        }
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] == '.')
            {
                data[i][j] = '#';
                if (IsLoop(data, startx, starty))
                {
                    ans++;
                }
                data[i][j] = '.';
            }
        }
    Console.WriteLine(ans);
}

bool IsLoop(char[][] data, int startx, int starty)
{
    var n = data.Length;
    var dir = new (int x, int y)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
    var curD = 0;
    var set = new HashSet<(int x, int y, int d)>();
    do
    {
        if (!set.Add((startx, starty, curD)))
        { return true; }
        var nextx = startx + dir[curD].x;
        var nexty = starty + dir[curD].y;

        if (nextx < 0 || nextx == n || nexty < 0 || nexty == n) return false;;
        if (data[nextx][nexty] == '#') { curD++; curD %= 4; }
        else { startx = nextx; starty = nexty; }
    } while (true);
}

char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.ToCharArray()).ToArray();
}