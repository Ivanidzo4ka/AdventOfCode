(int x, int y)[] dir = [(0, 1), (1, 0), (-1, 0), (0, -1)];
PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    int n = data.Length;
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            if (data[i][j] == '0')
            {
                ans += CountScore(data, i, j, '0');
            }
    Console.WriteLine(ans);
}
int CountScore(char[][] field, int x, int y, char cur)
{
    if (cur == '9') return 1;
    char next = (char)(cur + 1);
    var n = field.Length;
    var ans = 0;
    for (int d = 0; d < 4; d++)
    {
        var nextx = x + dir[d].x;
        var nexty = y + dir[d].y;
        if (nextx == n || nexty == n || nextx < 0 || nexty < 0) continue;
        if (field[nextx][nexty] == next)
        {
            ans += CountScore(field, nextx, nexty, next);
        }
    }
    return ans;
}
void PartOne(bool test = false)
{
    var data = ReadData(test);
    int n = data.Length;
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            if (data[i][j] == '0')
            {
                var set = new HashSet<(int x, int y)>();
                CountRating(data, i, j, '0', set);
                ans += set.Count;
            }
    Console.WriteLine(ans);
}
void CountRating(char[][] field, int x, int y, char cur, HashSet<(int x, int y)> tops)
{
    if (cur == '9') { tops.Add((x, y)); return; }
    char next = (char)(cur + 1);
    var n = field.Length;
    for (int d = 0; d < 4; d++)
    {
        var nextx = x + dir[d].x;
        var nexty = y + dir[d].y;
        if (nextx == n || nexty == n || nextx < 0 || nexty < 0) continue;
        if (field[nextx][nexty] == next)
        {
            CountRating(field, nextx, nexty, next, tops);
        }
    }

}

char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.ToCharArray()).ToArray();
}